﻿//-----------------------------------------------------------------------
// <copyright file="OperationResponseProcessor.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NJsonSchema.Infrastructure;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Contexts;

namespace NSwag.SwaggerGeneration.WebApi.Processors
{
    /// <summary>Generates the operation's response objects based on reflection and the ResponseTypeAttribute, SwaggerResponseAttribute and ProducesResponseTypeAttribute attributes.</summary>
    public class OperationResponseProcessor : IOperationProcessor
    {
        private readonly WebApiToSwaggerGeneratorSettings _settings;

        /// <summary>Initializes a new instance of the <see cref="OperationParameterProcessor"/> class.</summary>
        /// <param name="settings">The settings.</param>
        public OperationResponseProcessor(WebApiToSwaggerGeneratorSettings settings)
        {
            _settings = settings;
        }

        /// <summary>Processes the specified method information.</summary>
        /// <param name="context"></param>
        /// <returns>true if the operation should be added to the Swagger specification.</returns>
        public async Task<bool> ProcessAsync(OperationProcessorContext context)
        {
            var parameter = context.MethodInfo.ReturnParameter;
            var successXmlDescription = await parameter.GetDescriptionAsync(parameter.GetCustomAttributes()).ConfigureAwait(false) ?? string.Empty;

            var responseTypeAttributes = context.MethodInfo.GetCustomAttributes()
                .Where(a => a.GetType().Name == "ResponseTypeAttribute" ||
                            a.GetType().Name == "SwaggerResponseAttribute")
                .Concat(context.MethodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes()
                    .Where(a => a.GetType().Name == "SwaggerResponseAttribute"))
                .ToList();

            var producesResponseTypeAttributes = context.MethodInfo.GetCustomAttributes()
                .Where(a => a.GetType().Name == "ProducesResponseTypeAttribute")
                .ToList();

            var builder = new SwaggerResponseBuilder(
                context,
                _settings,
                GetVoidResponseStatusCode(),
                successXmlDescription);

            builder.PopulateModelsFromResponseTypeAttributes(responseTypeAttributes);
            builder.PopulateModelsFromResponseTypeAttributes(producesResponseTypeAttributes);

            await builder.BuildSwaggerResponseAsync(parameter);
            return true;
        }

        private string GetVoidResponseStatusCode()
        {
            return _settings.IsAspNetCore ? "200" : "204";
        }
    }
}