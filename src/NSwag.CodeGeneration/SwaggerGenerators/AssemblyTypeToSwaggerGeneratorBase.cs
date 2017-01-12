﻿//-----------------------------------------------------------------------
// <copyright file="AssemblyTypeToSwaggerGeneratorBase.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Threading.Tasks;

namespace NSwag.CodeGeneration.SwaggerGenerators
{
    /// <summary></summary>
    public abstract class AssemblyTypeToSwaggerGeneratorBase
    {
        /// <summary>Initializes a new instance of the <see cref="AssemblyTypeToSwaggerGeneratorBase"/> class.</summary>
        /// <param name="settings">The settings.</param>
        protected AssemblyTypeToSwaggerGeneratorBase(AssemblyTypeToSwaggerGeneratorSettings settings)
        {
            Settings = settings;
        }

        /// <summary>Gets or sets the settings.</summary>
        public AssemblyTypeToSwaggerGeneratorSettings Settings { get; protected set; }

        /// <summary>Generates the swagger document for all public/exported classes.</summary>
        /// <returns>The Swagger document.</returns>
        public abstract Task<SwaggerDocument> GenerateAsync();

        /// <summary>Generates the swagger document for the specified classes.</summary>
        /// <param name="classNames">The class names.</param>
        /// <returns>The Swagger document.</returns>
        public abstract Task<SwaggerDocument> GenerateAsync(string[] classNames);

        /// <summary>Gets the classes.</summary>
        /// <returns>The class names.</returns>
        public abstract string[] GetClasses();
    }
}