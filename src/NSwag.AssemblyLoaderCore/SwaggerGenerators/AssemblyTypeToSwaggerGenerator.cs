﻿//-----------------------------------------------------------------------
// <copyright file="AssemblyTypeToSwaggerGenerator.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Generation;
using NSwag.CodeGeneration.Infrastructure;
using NSwag.CodeGeneration.Utilities;

#if !FullNet
using System.Runtime.Loader;
#endif

namespace NSwag.CodeGeneration.SwaggerGenerators
{
    /// <summary>Generates a <see cref="SwaggerDocument"/> from a Web API controller or type which is located in a .NET assembly.</summary>
    public class AssemblyTypeToSwaggerGenerator : AssemblyTypeToSwaggerGeneratorBase
    {
        /// <summary>Initializes a new instance of the <see cref="AssemblyTypeToSwaggerGenerator"/> class.</summary>
        /// <param name="settings">The settings.</param>
        public AssemblyTypeToSwaggerGenerator(AssemblyTypeToSwaggerGeneratorSettings settings) : base(settings)
        {
        }

        /// <summary>Gets the available controller classes from the given assembly.</summary>
        /// <returns>The controller classes.</returns>
        public override string[] GetClasses()
        {
            if (File.Exists(Settings.AssemblyPath))
            {
#if FullNet
                using (var isolated = new AppDomainIsolation<NetAssemblyLoader>(Path.GetDirectoryName(Path.GetFullPath(Settings.AssemblyPath)), Settings.AssemblyConfig))
                    return isolated.Object.GetClasses(Settings.AssemblyPath, GetAllReferencePaths(Settings));
#else
                var loader = new NetAssemblyLoader();
                return loader.GetClasses(Settings.AssemblyPath, GetAllReferencePaths(Settings));
#endif
            }
            else
                return new string[] { };
        }

        /// <summary>Generates the Swagger definition for the given classes without operations (used for class generation).</summary>
        /// <param name="classNames">The class names.</param>
        /// <returns>The Swagger definition.</returns>
        public override async Task<SwaggerDocument> GenerateAsync(string[] classNames)
        {
#if FullNet
            using (var isolated =new AppDomainIsolation<NetAssemblyLoader>(Path.GetDirectoryName(Path.GetFullPath(Settings.AssemblyPath)), Settings.AssemblyConfig))
            {
                var json = await Task.Run(() => isolated.Object.FromAssemblyType(classNames, JsonConvert.SerializeObject(Settings))).ConfigureAwait(false);
                return await SwaggerDocument.FromJsonAsync(json).ConfigureAwait(false);
            }

#else
            var loader = new NetAssemblyLoader();
            var data = await loader.FromAssemblyTypeAsync(classNames, JsonConvert.SerializeObject(Settings)).ConfigureAwait(false);
            return await SwaggerDocument.FromJsonAsync(data).ConfigureAwait(false);
#endif
        }

        private static string[] GetAllReferencePaths(AssemblyTypeToSwaggerGeneratorSettings settings)
        {
            return new[] { Path.GetDirectoryName(PathUtilities.MakeAbsolutePath(settings.AssemblyPath, Directory.GetCurrentDirectory())) }
                .Concat(settings.ReferencePaths)
                .Distinct()
                .ToArray();
        }

        private class NetAssemblyLoader : AssemblyLoader
        {
            internal string FromAssemblyType(string[] classNames, string settingsData)
            {
                return FromAssemblyTypeAsync(classNames, settingsData).GetAwaiter().GetResult();
            }

            private async Task<string> FromAssemblyTypeAsync(string[] classNames, string settingsData)
            {
                var document = new SwaggerDocument();
                var settings = JsonConvert.DeserializeObject<AssemblyTypeToSwaggerGeneratorSettings>(settingsData);

                RegisterReferencePaths(GetAllReferencePaths(settings));

                var generator = new JsonSchemaGenerator(settings);
                var schemaResolver = new SwaggerSchemaResolver(document, settings);

#if FullNet
                var assembly = Assembly.LoadFrom(settings.AssemblyPath);
#else
                var assembly = Context.LoadFromAssemblyPath(settings.AssemblyPath);
#endif
                foreach (var className in classNames)
                {
                    var type = assembly.GetType(className);
                    var schema = await generator.GenerateAsync(type, schemaResolver).ConfigureAwait(false);
                    document.Definitions[type.Name] = schema;
                }

                return document.ToJson();
            }

            internal string[] GetClasses(string assemblyPath, IEnumerable<string> referencePaths)
            {
                RegisterReferencePaths(referencePaths);

#if FullNet
                var assembly = Assembly.LoadFrom(assemblyPath);
#else
                var assembly = Context.LoadFromAssemblyPath(assemblyPath);
#endif
                return assembly.ExportedTypes
                    .Select(t => t.FullName)
                    .OrderBy(t => t)
                    .ToArray();
            }
        }
    }
}
