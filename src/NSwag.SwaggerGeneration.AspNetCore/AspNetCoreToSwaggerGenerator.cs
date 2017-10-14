﻿//-----------------------------------------------------------------------
// <copyright file="AspNetCoreToSwaggerGenerator.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using NJsonSchema;
using NJsonSchema.Infrastructure;
using NSwag.SwaggerGeneration.Processors.Contexts;

namespace NSwag.SwaggerGeneration.AspNetCore
{
    /// <summary>Generates a <see cref="SwaggerDocument"/> object for the given Web API class type. </summary>
    public class AspNetCoreToSwaggerGenerator
    {
        private readonly SwaggerJsonSchemaGenerator _schemaGenerator;

        /// <summary>Initializes a new instance of the <see cref="AspNetCoreToSwaggerGenerator" /> class.</summary>
        /// <param name="settings">The settings.</param>
        public AspNetCoreToSwaggerGenerator(AspNetCoreToSwaggerGeneratorSettings settings)
            : this(settings, new SwaggerJsonSchemaGenerator(settings))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="AspNetCoreToSwaggerGenerator" /> class.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="schemaGenerator">The schema generator.</param>
        public AspNetCoreToSwaggerGenerator(AspNetCoreToSwaggerGeneratorSettings settings, SwaggerJsonSchemaGenerator schemaGenerator)
        {
            Settings = settings;
            _schemaGenerator = schemaGenerator;
        }

        /// <summary>Gets or sets the generator settings.</summary>
        public AspNetCoreToSwaggerGeneratorSettings Settings { get; }

        /// <summary>Generates a Swagger specification for the given <see cref="ApiDescriptionGroupCollection"/>.</summary>
        /// <param name="apiDescriptionGroups"><see cref="ApiDescriptionGroup"/>.</param>
        /// <returns>The <see cref="SwaggerDocument" />.</returns>
        /// <exception cref="InvalidOperationException">The operation has more than one body parameter.</exception>
        public Task<SwaggerDocument> GenerateAsync(ApiDescriptionGroupCollection apiDescriptionGroups)
        {
            var apiDescriptions = apiDescriptionGroups.Items.SelectMany(g => g.Items);
            return GenerateAsync(apiDescriptions);
        }

        /// <summary>
        /// Generates a Swagger specification for the given <see cref="ApiDescription"/> sequence.
        /// </summary>
        /// <param name="apiDescriptions"><see cref="ApiDescription"/></param>
        /// <returns>The <see cref="SwaggerDocument" />.</returns>
        public async Task<SwaggerDocument> GenerateAsync(IEnumerable<ApiDescription> apiDescriptions)
        {
            var document = await CreateDocumentAsync(Settings).ConfigureAwait(false);
            var schemaResolver = new SwaggerSchemaResolver(document, Settings);

            var apiGroups = apiDescriptions.Where(apiDescription => apiDescription.ActionDescriptor is ControllerActionDescriptor)
                .Select(apiDescription => new Tuple<ApiDescription, MethodInfo>(apiDescription, ((ControllerActionDescriptor)apiDescription.ActionDescriptor).MethodInfo))
                .GroupBy(item => item.Item2.DeclaringType);

            foreach (var controllerApiDescriptionGroup in apiGroups)
                await GenerateForControllerAsync(document, controllerApiDescriptionGroup.Key, controllerApiDescriptionGroup, new SwaggerGenerator(_schemaGenerator, Settings, schemaResolver), schemaResolver).ConfigureAwait(false);

            document.GenerateOperationIds();

            var controllerTypes = apiGroups.Select(k => k.Key);
            foreach (var processor in Settings.DocumentProcessors)
                await processor.ProcessAsync(new DocumentProcessorContext(document, controllerTypes, schemaResolver, _schemaGenerator));

            return document;
        }

        private async Task GenerateForControllerAsync(SwaggerDocument document, Type controllerType, IEnumerable<Tuple<ApiDescription, MethodInfo>> controllerApiDescriptionGroup, SwaggerGenerator swaggerGenerator, SwaggerSchemaResolver schemaResolver)
        {
            var hasIgnoreAttribute = controllerType.GetTypeInfo()
                .GetCustomAttributes()
                .Any(a => a.GetType().Name == "SwaggerIgnoreAttribute");

            if (hasIgnoreAttribute)
            {
                return;
            }

            var operations = new List<Tuple<SwaggerOperationDescription, ApiDescription, MethodInfo>>();

            var descriptions = new List<SwaggerOperationDescription>();
            foreach (var item in controllerApiDescriptionGroup)
            {
                var apiDescription = item.Item1;
                var method = item.Item2;

                var path = apiDescription.RelativePath;
                if (!path.StartsWith("/"))
                    path = "/" + path;

                var operationDescription = new SwaggerOperationDescription
                {
                    Path = path,
                    Method = GetSwaggerOperationMethod(apiDescription.HttpMethod),
                    Operation = new SwaggerOperation
                    {
                        IsDeprecated = method.GetCustomAttribute<ObsoleteAttribute>() != null,
                        OperationId = GetOperationId(document, controllerType.Name, method)
                    }
                };

                operations.Add(new Tuple<SwaggerOperationDescription, ApiDescription, MethodInfo>(operationDescription, apiDescription, method));
            }

            await AddOperationDescriptionsToDocumentAsync(document, controllerType, operations, swaggerGenerator, schemaResolver).ConfigureAwait(false);
        }

        private async Task AddOperationDescriptionsToDocumentAsync(SwaggerDocument document, Type controllerType, List<Tuple<SwaggerOperationDescription, ApiDescription, MethodInfo>> operations, SwaggerGenerator swaggerGenerator, SwaggerSchemaResolver schemaResolver)
        {
            var allOperation = operations.Select(t => t.Item1).ToList();
            foreach (var tuple in operations)
            {
                var operation = tuple.Item1;
                var apiDescription = tuple.Item2;
                var method = tuple.Item3;

                var addOperation = await RunOperationProcessorsAsync(document, apiDescription, controllerType, method, operation, allOperation, swaggerGenerator, schemaResolver).ConfigureAwait(false);
                if (addOperation)
                {
                    var path = operation.Path.Replace("//", "/");

                    if (!document.Paths.ContainsKey(path))
                        document.Paths[path] = new SwaggerOperations();

                    if (document.Paths[path].ContainsKey(operation.Method))
                    {
                        throw new InvalidOperationException("The method '" + operation.Method + "' on path '" + path + "' is registered multiple times " +
                            "(check the DefaultUrlTemplate setting [default for Web API: 'api/{controller}/{id}'; for MVC projects: '{controller}/{action}/{id?}']).");
                    }

                    document.Paths[path][operation.Method] = operation.Operation;
                }
            }
        }

        private async Task<SwaggerDocument> CreateDocumentAsync(AspNetCoreToSwaggerGeneratorSettings settings)
        {
            var document = !string.IsNullOrEmpty(settings.DocumentTemplate) ? await SwaggerDocument.FromJsonAsync(settings.DocumentTemplate).ConfigureAwait(false) : new SwaggerDocument();

            document.Generator = "NSwag v" + SwaggerDocument.ToolchainVersion + " (NJsonSchema v" + JsonSchema4.ToolchainVersion + ")";
            document.Consumes = new List<string> { "application/json" };
            document.Produces = new List<string> { "application/json" };
            document.Info = new SwaggerInfo
            {
                Title = settings.Title,
                Description = settings.Description,
                Version = settings.Version
            };

            return document;
        }

        private async Task<bool> RunOperationProcessorsAsync(SwaggerDocument document, ApiDescription apiDescription, Type controllerType, MethodInfo methodInfo, SwaggerOperationDescription operationDescription, List<SwaggerOperationDescription> allOperations, SwaggerGenerator swaggerGenerator, SwaggerSchemaResolver schemaResolver)
        {
            // 1. Run from settings
            var operationProcessorContext = new AspNetCoreOperationProcessorContext(document, operationDescription, controllerType, methodInfo, swaggerGenerator, _schemaGenerator, schemaResolver, allOperations)
            {
                ApiDescription = apiDescription,
            };
            foreach (var operationProcessor in Settings.OperationProcessors)
            {
                if (await operationProcessor.ProcessAsync(operationProcessorContext).ConfigureAwait(false) == false)
                    return false;
            }

            // 2. Run from class attributes
            var operationProcessorAttribute = methodInfo.DeclaringType.GetTypeInfo()
                .GetCustomAttributes()
            // 3. Run from method attributes
                .Concat(methodInfo.GetCustomAttributes())
                .Where(a => a.GetType().Name == "SwaggerOperationProcessorAttribute");

            foreach (dynamic attribute in operationProcessorAttribute)
            {
                var operationProcessor = ReflectionExtensions.HasProperty(attribute, "Parameters") ?
                    Activator.CreateInstance(attribute.Type, attribute.Parameters) :
                    Activator.CreateInstance(attribute.Type);

                if (operationProcessor.Process(methodInfo, operationDescription, swaggerGenerator, allOperations) == false)
                    return false;
            }

            return true;
        }

        private string GetOperationId(SwaggerDocument document, string controllerName, MethodInfo method)
        {
            string operationId;

            dynamic swaggerOperationAttribute = method.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name == "SwaggerOperationAttribute");
            if (swaggerOperationAttribute != null && !string.IsNullOrEmpty(swaggerOperationAttribute.OperationId))
                operationId = swaggerOperationAttribute.OperationId;
            else
            {
                if (controllerName.EndsWith("Controller"))
                    controllerName = controllerName.Substring(0, controllerName.Length - 10);

                operationId = controllerName + "_" + GetActionName(method);
            }

            var number = 1;
            while (document.Operations.Any(o => o.Operation.OperationId == operationId + (number > 1 ? "_" + number : string.Empty)))
                number++;

            return operationId + (number > 1 ? number.ToString() : string.Empty);
        }

        private string GetActionName(MethodInfo method)
        {
            var methodName = method.Name;
            if (methodName.EndsWith("Async"))
                methodName = methodName.Substring(0, methodName.Length - 5);

            return methodName;
        }

        private SwaggerOperationMethod GetSwaggerOperationMethod(string httpMethod)
        {
            if (string.Equals(httpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return SwaggerOperationMethod.Get;

            if (string.Equals(httpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                return SwaggerOperationMethod.Post;

            if (string.Equals(httpMethod, "PUT", StringComparison.OrdinalIgnoreCase))
                return SwaggerOperationMethod.Put;

            if (string.Equals(httpMethod, "DELETE", StringComparison.OrdinalIgnoreCase))
                return SwaggerOperationMethod.Delete;

            if (string.Equals(httpMethod, "HEAD", StringComparison.OrdinalIgnoreCase))
                return SwaggerOperationMethod.Head;

            if (string.Equals(httpMethod, "OPTIONS", StringComparison.OrdinalIgnoreCase))
                return SwaggerOperationMethod.Options;

            if (string.Equals(httpMethod, "PATCH", StringComparison.OrdinalIgnoreCase))
                return SwaggerOperationMethod.Patch;

            return SwaggerOperationMethod.Undefined;
        }
    }
}
