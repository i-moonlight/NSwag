﻿//-----------------------------------------------------------------------
// <copyright file="SecurityDefinitionAppender.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace NSwag.CodeGeneration.SwaggerGenerators.WebApi.Processors.Security
{
    /// <summary>Appends the OAuth2 security scheme to the document's security definitions.</summary>
    public class SecurityDefinitionAppender : IDocumentProcessor
    {
        private readonly string _name;
        private readonly SwaggerSecurityScheme _swaggerSecurityScheme;

        /// <summary>Initializes a new instance of the <see cref="SecurityDefinitionAppender" /> class.</summary>
        /// <param name="name">The name/key of the security scheme/definition.</param>
        /// <param name="swaggerSecurityScheme">The Swagger security scheme.</param>
        public SecurityDefinitionAppender(string name, SwaggerSecurityScheme swaggerSecurityScheme)
        {
            _name = name; 
            _swaggerSecurityScheme = swaggerSecurityScheme;
        }

        /// <summary>Processes the specified Swagger document.</summary>
        /// <param name="document">The Swagger document.</param>
        /// <param name="controllerTypes"></param>
        public void Process(SwaggerDocument document, IEnumerable<Type> controllerTypes)
        {
            document.SecurityDefinitions[_name] = _swaggerSecurityScheme;
        }
    }
}
