﻿//-----------------------------------------------------------------------
// <copyright file="WebApiToSwaggerGeneratorSettings.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NJsonSchema;
using NJsonSchema.Generation;
using NJsonSchema.Infrastructure;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Contexts;

namespace NSwag.SwaggerGeneration.AspNetCore
{
    internal class OperationParameterProcessor : IOperationProcessor
    {
        private readonly AspNetCoreToSwaggerGeneratorSettings _settings;

        public OperationParameterProcessor(AspNetCoreToSwaggerGeneratorSettings settings)
        {
            _settings = settings;
        }

        /// <summary>Processes the specified method information.</summary>
        /// <param name="operationProcessorContext"></param>
        /// <returns>true if the operation should be added to the Swagger specification.</returns>
        public async Task<bool> ProcessAsync(OperationProcessorContext operationProcessorContext)
        {
            if (!(operationProcessorContext is AspNetCoreOperationProcessorContext context))
            {
                return false;
            }

            var httpPath = context.OperationDescription.Path;
            var parameters = context.ApiDescription.ParameterDescriptions;

            var methodParameters = context.MethodInfo.GetParameters();

            foreach (var apiParameter in parameters.Where(p => p.Source != null))
            {
                var parameter = methodParameters.FirstOrDefault(m => m.Name == apiParameter.Name);
                if (parameter == null || parameter.GetCustomAttributes().Any(a => a.GetType().Name == "SwaggerIgnoreAttribute"))
                {
                    continue;
                }

                var parameterName = apiParameter.Name;
                var attributes = parameter.GetCustomAttributes();

                if (apiParameter.Source == BindingSource.Path)
                {
                    var operationParameter = await context.SwaggerGenerator.CreatePrimitiveParameterAsync(apiParameter.Name, parameter).ConfigureAwait(false);
                    operationParameter.Kind = SwaggerParameterKind.Path;
                    operationParameter.IsRequired = true; // Path is always required => property not needed

                    if (_settings.SchemaType == SchemaType.Swagger2)
                        operationParameter.IsNullableRaw = false;

                    context.OperationDescription.Operation.Parameters.Add(operationParameter);
                }
                else
                {
                    var parameterInfo = _settings.ReflectionService.GetDescription(apiParameter.Type, parameter.GetCustomAttributes(), _settings);
                    if (await TryAddFileParameterAsync(context, parameterInfo, parameter).ConfigureAwait(false) == false)
                    {
                        if (apiParameter.Source == BindingSource.Header)
                        {
                            var operationParameter = await context.SwaggerGenerator.CreatePrimitiveParameterAsync(parameterName, parameter).ConfigureAwait(false);
                            operationParameter.Kind = SwaggerParameterKind.Header;

                            context.OperationDescription.Operation.Parameters.Add(operationParameter);
                        }
                        else if (apiParameter.Source == BindingSource.Query)
                        {
                            var operationParameter = await context.SwaggerGenerator.CreatePrimitiveParameterAsync(parameterName, parameter).ConfigureAwait(false);
                            operationParameter.Kind = SwaggerParameterKind.Query;

                            context.OperationDescription.Operation.Parameters.Add(operationParameter);
                        }
                        else
                        {
                            if (apiParameter.Source == BindingSource.Body)
                                await AddBodyParameterAsync(context, parameterName, parameter).ConfigureAwait(false);

                            else if (parameterInfo.IsComplexType)
                            {
                                // Try to find a [WillReadBody] attribute on either the action parameter or the bindingAttribute's class
                                var willReadBodyAttribute = attributes.TryGetIfAssignableTo("WillReadBodyAttribute", TypeNameStyle.Name);

                                if (willReadBodyAttribute == null)
                                    await AddBodyParameterAsync(context, parameterName, parameter).ConfigureAwait(false);
                                else
                                {
                                    // Try to get a boolean property value from the attribute which explicity tells us whether to read from the body
                                    // If no such property exists, then default to false since WebAPI's HttpParameterBinding.WillReadBody defaults to false
                                    var willReadBody = willReadBodyAttribute.TryGetPropertyValue("WillReadBody", true);

                                    if (willReadBody)
                                        await AddBodyParameterAsync(context, parameterName, parameter).ConfigureAwait(false);
                                    else
                                    {
                                        // If we are not reading from the body, then treat this as a primitive.
                                        // This may seem odd, but it allows for primitive -> custom complex-type bindings which are very common
                                        // In this case, the API author should use a TypeMapper to define the parameter
                                        await AddPrimitiveParametersFromUriAsync(
                                            context, apiParameter, httpPath, parameterName, parameter, parameterInfo).ConfigureAwait(false);
                                    }
                                }
                            }
                            else
                                await AddPrimitiveParameterAsync(parameterName, context.OperationDescription.Operation, parameter, context.SwaggerGenerator).ConfigureAwait(false);
                        }
                    }
                }
            }

            if (_settings.AddMissingPathParameters)
            {
                foreach (Match match in Regex.Matches(httpPath, "{(.*?)(:(([^/]*)?))?}"))
                {
                    var parameterName = match.Groups[1].Value;
                    if (context.OperationDescription.Operation.Parameters.All(p => !string.Equals(p.Name, parameterName, StringComparison.OrdinalIgnoreCase)))
                    {
                        var parameterType = match.Groups.Count == 5 ? match.Groups[3].Value : "string";
                        var operationParameter = context.SwaggerGenerator.CreateUntypedPathParameter(parameterName, parameterType);
                        context.OperationDescription.Operation.Parameters.Add(operationParameter);
                    }
                }
            }

            RemoveUnusedPathParameters(context.OperationDescription, httpPath);
            UpdateConsumedTypes(context.OperationDescription);

            EnsureSingleBodyParameter(context.OperationDescription);

            return true;
        }

        private void EnsureSingleBodyParameter(SwaggerOperationDescription operationDescription)
        {
            if (operationDescription.Operation.ActualParameters.Count(p => p.Kind == SwaggerParameterKind.Body) > 1)
                throw new InvalidOperationException("The operation '" + operationDescription.Operation.OperationId + "' has more than one body parameter.");
        }

        private void UpdateConsumedTypes(SwaggerOperationDescription operationDescription)
        {
            if (operationDescription.Operation.ActualParameters.Any(p => p.Type == JsonObjectType.File))
                operationDescription.Operation.Consumes = new List<string> { "multipart/form-data" };
        }

        private void RemoveUnusedPathParameters(SwaggerOperationDescription operationDescription, string httpPath)
        {
            operationDescription.Path = Regex.Replace(httpPath, "{(.*?)(:(([^/]*)?))?}", match =>
            {
                var parameterName = match.Groups[1].Value.TrimEnd('?');
                if (operationDescription.Operation.ActualParameters.Any(p => p.Kind == SwaggerParameterKind.Path && string.Equals(p.Name, parameterName, StringComparison.OrdinalIgnoreCase)))
                    return "{" + parameterName + "}";
                return string.Empty;
            }).TrimEnd('/');
        }

        private async Task<bool> TryAddFileParameterAsync(
            OperationProcessorContext context, JsonTypeDescription info, ParameterInfo parameter)
        {
            var isFileArray = IsFileArray(parameter.ParameterType, info);

            var attributes = parameter.GetCustomAttributes()
                .Union(parameter.ParameterType.GetTypeInfo().GetCustomAttributes());

            var hasSwaggerFileAttribute = attributes.Any(a =>
                a.GetType().IsAssignableTo("SwaggerFileAttribute", TypeNameStyle.Name));

            if (info.Type == JsonObjectType.File || hasSwaggerFileAttribute || isFileArray)
            {
                await AddFileParameterAsync(context, parameter, isFileArray).ConfigureAwait(false);
                return true;
            }

            return false;
        }

        private async Task AddFileParameterAsync(OperationProcessorContext context, ParameterInfo parameter, bool isFileArray)
        {
            var attributes = parameter.GetCustomAttributes().ToList();

            // TODO: Check if there is a way to control the property name
            var parameterDocumentation = await parameter.GetDescriptionAsync(parameter.GetCustomAttributes()).ConfigureAwait(false);
            var operationParameter = await context.SwaggerGenerator.CreatePrimitiveParameterAsync(
                parameter.Name, parameterDocumentation, parameter.ParameterType, attributes).ConfigureAwait(false);

            InitializeFileParameter(operationParameter, isFileArray);
            context.OperationDescription.Operation.Parameters.Add(operationParameter);
        }

        private bool IsFileArray(Type type, JsonTypeDescription typeInfo)
        {
            var isFormFileCollection = type.Name == "IFormFileCollection";
            var isFileArray = typeInfo.Type == JsonObjectType.Array && type.GenericTypeArguments.Any() &&
                _settings.ReflectionService.GetDescription(type.GenericTypeArguments[0], null, _settings).Type == JsonObjectType.File;
            return isFormFileCollection || isFileArray;
        }

        private async Task AddBodyParameterAsync(OperationProcessorContext context, string name, ParameterInfo parameter)
        {
            var operation = context.OperationDescription.Operation;
            if (parameter.ParameterType.Name == "XmlDocument" || parameter.ParameterType.InheritsFrom("XmlDocument", TypeNameStyle.Name))
            {
                operation.Consumes = new List<string> { "application/xml" };
                operation.Parameters.Add(new SwaggerParameter
                {
                    Name = name,
                    Kind = SwaggerParameterKind.Body,
                    Schema = new JsonSchema4 { Type = JsonObjectType.String },
                    IsNullableRaw = true,
                    IsRequired = parameter.HasDefaultValue == false,
                    Description = await parameter.GetDescriptionAsync(parameter.GetCustomAttributes()).ConfigureAwait(false)
                });
            }
            else if (parameter.ParameterType.IsAssignableTo("System.IO.Stream", TypeNameStyle.FullName))
            {
                operation.Consumes = new List<string> { "application/octet-stream" };
                operation.Parameters.Add(new SwaggerParameter
                {
                    Name = name,
                    Kind = SwaggerParameterKind.Body,
                    Schema = new JsonSchema4 { Type = JsonObjectType.String, Format = JsonFormatStrings.Byte },
                    IsNullableRaw = true,
                    IsRequired = parameter.HasDefaultValue == false,
                    Description = await parameter.GetDescriptionAsync(parameter.GetCustomAttributes()).ConfigureAwait(false)
                });
            }
            else
            {
                var operationParameter = await context.SwaggerGenerator
                    .CreateBodyParameterAsync(name, parameter).ConfigureAwait(false);
                operation.Parameters.Add(operationParameter);
            }
        }

        private async Task AddPrimitiveParametersFromUriAsync(OperationProcessorContext context, ApiParameterDescription apiParameterDescription, string httpPath, string name, ParameterInfo parameter, JsonTypeDescription typeDescription)
        {
            var operation = context.OperationDescription.Operation;
            if (typeDescription.Type.HasFlag(JsonObjectType.Array))
            {
                var attributes = parameter.GetCustomAttributes();

                var parameterDocumentation = await parameter.GetDescriptionAsync(attributes).ConfigureAwait(false);
                var operationParameter = await context.SwaggerGenerator.CreatePrimitiveParameterAsync(
                    name, parameterDocumentation, parameter.ParameterType, attributes).ConfigureAwait(false);

                operationParameter.Kind = SwaggerParameterKind.Query;
                operation.Parameters.Add(operationParameter);
            }
            else
            {
                foreach (var property in parameter.ParameterType.GetRuntimeProperties())
                {
                    var attributes = property.GetCustomAttributes().ToList();
                    if (attributes.All(a => a.GetType().Name != "SwaggerIgnoreAttribute" && a.GetType().Name != "JsonIgnoreAttribute"))
                    {
                        var fromQueryAttribute = attributes.SingleOrDefault(a => a.GetType().Name == "FromQueryAttribute");
                        var propertyName = fromQueryAttribute.TryGetPropertyValue<string>("Name") ??
                            context.SchemaGenerator.GetPropertyName(null, property);

                        dynamic fromRouteAttribute = attributes.SingleOrDefault(a => a.GetType().FullName == "Microsoft.AspNetCore.Mvc.FromRouteAttribute");
                        if (fromRouteAttribute != null && !string.IsNullOrEmpty(fromRouteAttribute?.Name))
                            propertyName = fromRouteAttribute?.Name;

                        dynamic fromHeaderAttribute = attributes.SingleOrDefault(a => a.GetType().FullName == "Microsoft.AspNetCore.Mvc.FromHeaderAttribute");
                        if (fromHeaderAttribute != null && !string.IsNullOrEmpty(fromHeaderAttribute?.Name))
                            propertyName = fromHeaderAttribute?.Name;

                        var propertySummary = await property.GetXmlSummaryAsync().ConfigureAwait(false);
                        var operationParameter = await context.SwaggerGenerator.CreatePrimitiveParameterAsync(
                            propertyName, propertySummary, property.PropertyType, attributes).ConfigureAwait(false);

                        // TODO: Check if required can be controlled with mechanisms other than RequiredAttribute

                        var parameterInfo = _settings.ReflectionService.GetDescription(property.PropertyType, attributes, _settings);
                        var isFileArray = IsFileArray(property.PropertyType, parameterInfo);
                        if (parameterInfo.Type == JsonObjectType.File || isFileArray)
                            InitializeFileParameter(operationParameter, isFileArray);
                        else if (fromRouteAttribute != null
                            || httpPath.ToLowerInvariant().Contains("{" + propertyName.ToLower() + "}")
                            || httpPath.ToLowerInvariant().Contains("{" + propertyName.ToLower() + ":"))
                        {
                            operationParameter.Kind = SwaggerParameterKind.Path;
                            operationParameter.IsNullableRaw = false;
                            operationParameter.IsRequired = true; // Path is always required => property not needed
                        }
                        else if (fromHeaderAttribute != null)
                            operationParameter.Kind = SwaggerParameterKind.Header;
                        else
                            operationParameter.Kind = SwaggerParameterKind.Query;

                        operation.Parameters.Add(operationParameter);
                    }
                }
            }
        }

        private async Task AddPrimitiveParameterAsync(
            string name, SwaggerOperation operation, ParameterInfo parameter, SwaggerGenerator swaggerGenerator)
        {
            var operationParameter = await swaggerGenerator.CreatePrimitiveParameterAsync(name, parameter).ConfigureAwait(false);
            operationParameter.Kind = SwaggerParameterKind.Query;
            operationParameter.IsRequired = operationParameter.IsRequired || parameter.HasDefaultValue == false;

            if (parameter.HasDefaultValue)
                operationParameter.Default = parameter.DefaultValue;

            operation.Parameters.Add(operationParameter);
        }

        private void InitializeFileParameter(SwaggerParameter operationParameter, bool isFileArray)
        {
            operationParameter.Type = JsonObjectType.File;
            operationParameter.Kind = SwaggerParameterKind.FormData;

            if (isFileArray)
                operationParameter.CollectionFormat = SwaggerParameterCollectionFormat.Multi;
        }
    }
}