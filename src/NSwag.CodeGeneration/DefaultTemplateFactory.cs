﻿//-----------------------------------------------------------------------
// <copyright file="DefaultTemplateFactory.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Reflection;
using NJsonSchema.CodeGeneration;
using System.IO;

namespace NSwag.CodeGeneration
{
    /// <summary>The default template factory which loads templates from embedded resources.</summary>
    public class DefaultTemplateFactory : NJsonSchema.CodeGeneration.DefaultTemplateFactory
    {
        /// <summary>Initializes a new instance of the <see cref="DefaultTemplateFactory" /> class.</summary>
        /// <param name="settings">The settings.</param>
        public DefaultTemplateFactory(CodeGeneratorSettingsBase settings) : base(settings)
        {
        }

        /// <summary>Gets the current toolchain version.</summary>
        /// <returns>The toolchain version.</returns>
        protected override string GetToolchainVersion()
        {
            return SwaggerDocument.ToolchainVersion + " (NJsonSchema v" + base.GetToolchainVersion() + ")";
        }

        /// <summary>Tries to load an embedded Liquid template.</summary>
        /// <param name="language">The language.</param>
        /// <param name="template">The template name.</param>
        /// <returns>The template.</returns>
        protected override string GetEmbeddedLiquidTemplate(string language, string template)
        {
            var assembly = Assembly.Load(new AssemblyName("NSwag.CodeGeneration." + language));
            var resourceName = "NSwag.CodeGeneration." + language + ".Templates." + template + ".liquid";

            var resource = assembly.GetManifestResourceStream(resourceName);
            if (resource != null)
            {
                using (var reader = new StreamReader(resource))
                    return reader.ReadToEnd();
            }

            return base.GetEmbeddedLiquidTemplate(language, template);
        }
    }
}
