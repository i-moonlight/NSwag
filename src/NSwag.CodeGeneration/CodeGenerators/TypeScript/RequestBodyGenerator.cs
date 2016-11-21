//-----------------------------------------------------------------------
// <copyright file="RequestBodyGenerator.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using NJsonSchema;
using NSwag.CodeGeneration.CodeGenerators.Models;
using NSwag.CodeGeneration.CodeGenerators.TypeScript.Templates;

namespace NSwag.CodeGeneration.CodeGenerators.TypeScript
{
    /// <summary>It generates request body using <see cref="RequestBodyTemplate"/></summary>
    public class RequestBodyGenerator
    {
        /// <summary>Renders the request body generation code.</summary>
        /// <param name="operation">The operation.</param>
        /// <param name="tabCount">The tab count.</param>
        /// <returns>Rendered request body</returns>
        public static string Render(OperationModel operation, int tabCount = 0)
        {
            var tpl = new RequestBodyTemplate(operation);
            return ConversionUtilities.Tab(tpl.Render(), tabCount);
        }
    }
}