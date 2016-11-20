﻿using NJsonSchema.CodeGeneration;
using NSwag.CodeGeneration.CodeGenerators.Models;

namespace NSwag.CodeGeneration.CodeGenerators.TypeScript.Templates
{
    internal partial class RequestUrlTemplate : ITemplate
    {
        public RequestUrlTemplate(OperationModel model)
        {
            Model = model;
        }

        public OperationModel Model { get; }

        public string Render()
        {
            return ConversionUtilities.TrimWhiteSpaces(TransformText());
        }
    }
}
