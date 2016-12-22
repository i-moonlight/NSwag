//-----------------------------------------------------------------------
// <copyright file="SwaggerToTypeScriptClientCommand.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Threading.Tasks;
using NConsole;
using Newtonsoft.Json;
using NJsonSchema.CodeGeneration.TypeScript;
using NJsonSchema.Infrastructure;
using NSwag.CodeGeneration.CodeGenerators;
using NSwag.CodeGeneration.CodeGenerators.TypeScript;
using NSwag.Commands.Base;

#pragma warning disable 1591

namespace NSwag.Commands
{
    [Command(Name = "swagger2tsclient", Description = "Generates TypeScript client code from a Swagger specification.")]
    public class SwaggerToTypeScriptClientCommand : InputOutputCommandBase
    {
        public SwaggerToTypeScriptClientCommand()
        {
            Settings = new SwaggerToTypeScriptClientGeneratorSettings();
        }

        [JsonIgnore]
        public SwaggerToTypeScriptClientGeneratorSettings Settings { get; set; }

        [Argument(Name = "ClassName", IsRequired = false, Description = "The class name of the generated client.")]
        public string ClassName
        {
            get { return Settings.ClassName; }
            set { Settings.ClassName = value; }
        }

        [Argument(Name = "ModuleName", IsRequired = false, Description = "The TypeScript module name (default: '', no module).")]
        public string ModuleName
        {
            get { return Settings.TypeScriptGeneratorSettings.ModuleName; }
            set { Settings.TypeScriptGeneratorSettings.ModuleName = value; }
        }

        [Argument(Name = "Namespace", IsRequired = false, Description = "The TypeScript namespace (default: '', no namespace).")]
        public string Namespace
        {
            get { return Settings.TypeScriptGeneratorSettings.Namespace; }
            set { Settings.TypeScriptGeneratorSettings.Namespace = value; }
        }

        [Argument(Name = "TypeScriptVersion", IsRequired = false, Description = "The target TypeScript version (default: 1.8).")]
        public decimal TypeScriptVersion
        {
            get { return Settings.TypeScriptGeneratorSettings.TypeScriptVersion; }
            set { Settings.TypeScriptGeneratorSettings.TypeScriptVersion = value; }
        }

        [Argument(Name = "Template", IsRequired = false, Description = "The type of the asynchronism handling " +
                                                                       "('JQueryCallbacks', 'JQueryPromises', 'AngularJS', 'Angular2', 'Fetch', 'Aurelia').")]
        public TypeScriptTemplate Template
        {
            get { return Settings.Template; }
            set { Settings.Template = value; }
        }

        [Argument(Name = "PromiseType", IsRequired = false, Description = "The promise type ('Promise' or 'QPromise').")]
        public PromiseType PromiseType
        {
            get { return Settings.PromiseType; }
            set { Settings.PromiseType = value; }
        }

        [Argument(Name = "DateTimeType", IsRequired = false, Description = "The date time type ('Date', 'MomentJS', 'string').")]
        public TypeScriptDateTimeType DateTimeType
        {
            get { return Settings.TypeScriptGeneratorSettings.DateTimeType; }
            set { Settings.TypeScriptGeneratorSettings.DateTimeType = value; }
        }

        [Argument(Name = "GenerateClientClasses", IsRequired = false, Description = "Specifies whether generate client classes.")]
        public bool GenerateClientClasses
        {
            get { return Settings.GenerateClientClasses; }
            set { Settings.GenerateClientClasses = value; }
        }

        [Argument(Name = "GenerateClientInterfaces", IsRequired = false, Description = "Specifies whether generate interfaces for the client classes (default: false).")]
        public bool GenerateClientInterfaces
        {
            get { return Settings.GenerateClientInterfaces; }
            set { Settings.GenerateClientInterfaces = value; }
        }

        [Argument(Name = "WrapDtoExceptions", IsRequired = false, Description = "Specifies whether DTO exceptions are wrapped in a SwaggerException instance (default: false).")]
        public bool WrapDtoExceptions
        {
            get { return Settings.WrapDtoExceptions; }
            set { Settings.WrapDtoExceptions = value; }
        }

        [Argument(Name = "ClientBaseClass", IsRequired = false, Description = "The base class of the generated client classes (optional, must be imported or implemented in the extension code).")]
        public string ClientBaseClass
        {
            get { return Settings.ClientBaseClass; }
            set { Settings.ClientBaseClass = value; }
        }

        [Argument(Name = "UseTransformOptionsMethod", IsRequired = false, Description = "Call 'transformOptions' on the base class or extension class (default: false).")]
        public bool UseTransformOptionsMethod
        {
            get { return Settings.UseTransformOptionsMethod; }
            set { Settings.UseTransformOptionsMethod = value; }
        }

        [Argument(Name = "UseTransformResultMethod", IsRequired = false, Description = "Call 'transformResult' on the base class or extension class (default: false).")]
        public bool UseTransformResultMethod
        {
            get { return Settings.UseTransformResultMethod; }
            set { Settings.UseTransformResultMethod = value; }
        }

        [Argument(Name = "GenerateDtoTypes", IsRequired = false, Description = "Specifies whether to generate DTO classes.")]
        public bool GenerateDtoTypes
        {
            get { return Settings.GenerateDtoTypes; }
            set { Settings.GenerateDtoTypes = value; }
        }

        [Argument(Name = "OperationGenerationMode", IsRequired = false, Description = "The operation generation mode ('SingleClientFromOperationId' or 'MultipleClientsFromPathSegments').")]
        public OperationGenerationMode OperationGenerationMode
        {
            get { return Settings.OperationGenerationMode; }
            set { Settings.OperationGenerationMode = value; }
        }

        [Argument(Name = "MarkOptionalProperties", IsRequired = false, Description = "Specifies whether to mark optional properties with ? (default: false).")]
        public bool MarkOptionalProperties
        {
            get { return Settings.TypeScriptGeneratorSettings.MarkOptionalProperties; }
            set { Settings.TypeScriptGeneratorSettings.MarkOptionalProperties = value; }
        }

        [Argument(Name = "TypeStyle", IsRequired = false, Description = "The type style (default: Class).")]
        public TypeScriptTypeStyle TypeStyle
        {
            get { return Settings.TypeScriptGeneratorSettings.TypeStyle; }
            set { Settings.TypeScriptGeneratorSettings.TypeStyle = value; }
        }

        [Argument(Name = "ClassTypes", IsRequired = false, Description = "The type names which always generate plain TypeScript classes.")]
        public string[] ClassTypes
        {
            get { return Settings.TypeScriptGeneratorSettings.ClassTypes; }
            set { Settings.TypeScriptGeneratorSettings.ClassTypes = value; }
        }

        [Argument(Name = "ExtendedClasses", IsRequired = false, Description = "The list of extended classes.")]
        public string[] ExtendedClasses
        {
            get { return Settings.TypeScriptGeneratorSettings.ExtendedClasses; }
            set { Settings.TypeScriptGeneratorSettings.ExtendedClasses = value; }
        }

        [Argument(Name = "ExtensionCode", IsRequired = false, Description = "The extension code (string or file path).")]
        public string ExtensionCode { get; set; }

        [Argument(Name = "GenerateDefaultValues", IsRequired = false, Description = "Specifies whether to generate default values for properties (default: true).")]
        public bool GenerateDefaultValues
        {
            get { return Settings.TypeScriptGeneratorSettings.GenerateDefaultValues; }
            set { Settings.TypeScriptGeneratorSettings.GenerateDefaultValues = value; }
        }

        [Argument(Name = "ExcludedTypeNames", IsRequired = false, Description = "The excluded DTO type names (must be defined in an import or other namespace).")]
        public string[] ExcludedTypeNames
        {
            get { return Settings.TypeScriptGeneratorSettings.ExcludedTypeNames; }
            set { Settings.TypeScriptGeneratorSettings.ExcludedTypeNames = value; }
        }

        public override async Task<object> RunAsync(CommandLineProcessor processor, IConsoleHost host)
        {
            var code = await RunAsync();
            if (await TryWriteFileOutputAsync(host, () => Task.FromResult(code)).ConfigureAwait(false) == false)
                return code;
            return null; 
        }

        public async Task<string> RunAsync()
        {
            return await Task.Run(async () =>
            {
                var additionalCode = ExtensionCode ?? string.Empty;
                if (await DynamicApis.FileExistsAsync(additionalCode).ConfigureAwait(false))
                    additionalCode = await DynamicApis.FileReadAllTextAsync(additionalCode).ConfigureAwait(false);
                Settings.TypeScriptGeneratorSettings.ExtensionCode = additionalCode;

                var document = await GetInputSwaggerDocument().ConfigureAwait(false);
                var clientGenerator = new SwaggerToTypeScriptClientGenerator(document, Settings);
                return clientGenerator.GenerateFile();
            });
        }
    }
}