﻿using NJsonSchema;
using NJsonSchema.CodeGeneration;
using NSwag.CodeGeneration.CodeGenerators.CSharp.Models;

namespace NSwag.CodeGeneration.CodeGenerators.CSharp.Templates
{
    internal partial class ClientTemplate : ITemplate
    {
        public ClientTemplate(CSharpClientTemplateModel model)
        {
            Model = model;
        }

        public CSharpClientTemplateModel Model { get; }

        public string Render()
        {
            return ConversionUtilities.TrimWhiteSpaces(TransformText());
        }
    }
}
