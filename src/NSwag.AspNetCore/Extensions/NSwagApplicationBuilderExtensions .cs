﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NSwag.AspNetCore;
using NSwag.AspNetCore.Middlewares;
using NSwag.SwaggerGeneration.WebApi;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>NSwag extensions for <see cref="IApplicationBuilder"/>.</summary>
    public static class NSwagApplicationBuilderExtensions
    {
        /// <summary>Adds the OpenAPI/Swagger generator that uses Api Description to perform Swagger generation.</summary>
        /// <remarks>Registers multiple routes/documents if the settings.Path contains a '{documentName}' placeholder.</remarks>
        /// <param name="app">The app.</param>
        /// <param name="configure">Configure additional settings.</param>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, Action<SwaggerDocumentMiddlewareSettings> configure = null)
        {
            return UseSwaggerWithApiExplorerCore(app, configure);
        }

        private static IApplicationBuilder UseSwaggerWithApiExplorerCore(IApplicationBuilder app, Action<SwaggerDocumentMiddlewareSettings> configure)
        {
            // TODO(v12): Add IOptions support when SwaggerUi3Settings<> T has been removed
            //var settings = configure == null && app.ApplicationServices.GetService<IOptions<SwaggerMiddlewareSettings>>()?.Value ?? new SwaggerMiddlewareSettings();

            var settings = new SwaggerDocumentMiddlewareSettings();
            configure?.Invoke(settings);

            if (settings.Path.Contains("{documentName}"))
            {
                var documents = app.ApplicationServices.GetRequiredService<IEnumerable<SwaggerDocumentRegistration>>();
                foreach (var document in documents)
                {
                    app = app.UseMiddleware<SwaggerDocumentMiddleware>(document.DocumentName, settings.Path.Replace("{documentName}", document.DocumentName), settings);
                }

                return app;
            }
            else
            {
                return app.UseMiddleware<SwaggerDocumentMiddleware>(settings.DocumentName, settings.Path, settings);
            }
        }

        /// <summary>Adds the Swagger UI (only) to the pipeline.</summary>
        /// <remarks>The settings.GeneratorSettings property does not have any effect.</remarks>
        /// <param name="app">The app.</param>
        /// <param name="configure">Configure the Swagger settings.</param>
        /// <returns>The app builder.</returns>
        public static IApplicationBuilder UseSwaggerUi3(
            this IApplicationBuilder app,
            Action<SwaggerUi3Settings<WebApiToSwaggerGeneratorSettings>> configure = null)
        {
            // TODO(v12): Add IOptions support when SwaggerUi3Settings<> T has been removed
            //var settings = configure == null && app.ApplicationServices.GetService<IOptions<SwaggerUi3Settings<WebApiToSwaggerGeneratorSettings>>>()?.Value ?? 
            //    new SwaggerUi3Settings<WebApiToSwaggerGeneratorSettings>();

            var settings = new SwaggerUi3Settings<WebApiToSwaggerGeneratorSettings>();
            configure?.Invoke(settings);

            UseSwaggerUiWithDocumentNamePlaceholderExpanding(app, settings, (swaggerRoute, swaggerUiRoute) =>
            {
                app.UseMiddleware<RedirectToIndexMiddleware>(swaggerUiRoute, swaggerRoute, settings.TransformToExternalPath);
                app.UseMiddleware<SwaggerUiIndexMiddleware<WebApiToSwaggerGeneratorSettings>>(swaggerUiRoute + "/index.html", settings, "NSwag.AspNetCore.SwaggerUi3.index.html");
                app.UseFileServer(new FileServerOptions
                {
                    RequestPath = new PathString(swaggerUiRoute),
                    FileProvider = new EmbeddedFileProvider(typeof(SwaggerExtensions).GetTypeInfo().Assembly, "NSwag.AspNetCore.SwaggerUi3")
                });
            },
            (documents) =>
            {
                var swaggerRouteWithPlaceholder = settings.ActualSwaggerDocumentPath;

                settings.SwaggerRoutes.Clear();
                foreach (var document in documents)
                {
                    var swaggerRoute = swaggerRouteWithPlaceholder.Replace("{documentName}", document.DocumentName);
                    settings.SwaggerRoutes.Add(new SwaggerUi3Route(document.DocumentName, swaggerRoute));
                }
            });

            return app;
        }

        /// <summary>Adds the ReDoc UI (only) to the pipeline.</summary>
        /// <remarks>The settings.GeneratorSettings property does not have any effect.</remarks>
        /// <param name="app">The app.</param>
        /// <param name="configure">Configure the Swagger settings.</param>
        /// <returns>The app builder.</returns>
        public static IApplicationBuilder UseReDoc(
            this IApplicationBuilder app,
            Action<SwaggerReDocSettings<WebApiToSwaggerGeneratorSettings>> configure = null)
        {
            var settings = new SwaggerReDocSettings<WebApiToSwaggerGeneratorSettings>();
            configure?.Invoke(settings);

            UseSwaggerUiWithDocumentNamePlaceholderExpanding(app, settings, (swaggerRoute, swaggerUiRoute) =>
            {
                app.UseMiddleware<RedirectToIndexMiddleware>(swaggerUiRoute, swaggerRoute, settings.TransformToExternalPath);
                app.UseMiddleware<SwaggerUiIndexMiddleware<WebApiToSwaggerGeneratorSettings>>(swaggerUiRoute + "/index.html", settings, "NSwag.AspNetCore.ReDoc.index.html");
                app.UseFileServer(new FileServerOptions
                {
                    RequestPath = new PathString(swaggerUiRoute),
                    FileProvider = new EmbeddedFileProvider(typeof(SwaggerExtensions).GetTypeInfo().Assembly, "NSwag.AspNetCore.ReDoc")
                });
            }, (documents) => throw new NotSupportedException("ReDoc does not support multiple documents per UI: " +
                "Do not use '{documentName}' placeholder only in SwaggerRoute but also in SwaggerUiRoute to register multiple UIs."));

            return app;
        }

        private static void UseSwaggerUiWithDocumentNamePlaceholderExpanding(IApplicationBuilder app,
            SwaggerUiSettingsBase<WebApiToSwaggerGeneratorSettings> settings,
            Action<string, string> register,
            Action<IEnumerable<SwaggerDocumentRegistration>> registerMultiple)
        {
            if (settings.ActualSwaggerDocumentPath.Contains("{documentName}"))
            {
                var documents = app.ApplicationServices.GetRequiredService<IEnumerable<SwaggerDocumentRegistration>>();
                if (settings.ActualSwaggerUiPath.Contains("{documentName}"))
                {
                    // Register multiple uis for each document
                    foreach (var document in documents)
                    {
                        register(
                            settings.ActualSwaggerDocumentPath.Replace("{documentName}", document.DocumentName),
                            settings.ActualSwaggerUiPath.Replace("{documentName}", document.DocumentName));
                    }
                }
                else
                {
                    // Register single ui with multiple documents
                    registerMultiple(documents);
                    register(settings.ActualSwaggerDocumentPath, settings.ActualSwaggerUiPath);
                }
            }
            else
            {
                if (settings.ActualSwaggerUiPath.Contains("{documentName}"))
                {
                    throw new ArgumentException("The SwaggerUiRoute cannot contain '{documentName}' placeholder when SwaggerRoute is missing the placeholder.");
                }

                // Register single ui with one document
                register(settings.ActualSwaggerDocumentPath, settings.ActualSwaggerUiPath);
            }
        }
    }
}
