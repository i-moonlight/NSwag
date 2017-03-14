//-----------------------------------------------------------------------
// <copyright file="SwaggerToCSharpClientGeneratorSettings.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

namespace NSwag.CodeGeneration.CSharp
{
    /// <summary>Settings for the <see cref="SwaggerToCSharpClientGenerator"/>.</summary>
    public class SwaggerToCSharpClientGeneratorSettings : SwaggerToCSharpGeneratorSettings
    {
        /// <summary>Initializes a new instance of the <see cref="SwaggerToCSharpClientGeneratorSettings"/> class.</summary>
        public SwaggerToCSharpClientGeneratorSettings()
        {
            ClassName = "{controller}Client";

            GenerateExceptionClasses = true;
            ExceptionClass = "SwaggerException";
            ClientClassAccessModifier = "public";
            UseBaseUrl = true;
            HttpClientType = "System.Net.Http.HttpClient";
            WrapDtoExceptions = true;
        }

        /// <summary>Gets or sets the full name of the base class.</summary>
        public string ClientBaseClass { get; set; }

        /// <summary>Gets or sets the full name of the configuration class (<see cref="ClientBaseClass"/> must be set).</summary>
        public string ConfigurationClass { get; set; }

        /// <summary>Gets or sets a value indicating whether to generate exception classes (default: true).</summary>
        public bool GenerateExceptionClasses { get; set; }

        /// <summary>Gets or sets the name of the exception class (supports the '{controller}' placeholder, default 'SwaggerException').</summary>
        public string ExceptionClass { get; set; }

        /// <summary>Gets or sets a value indicating whether an HttpClient instance is injected into the client.</summary>
        public bool InjectHttpClient { get; set; }

        /// <summary>Gets or sets the list of methods with a protected access modifier ("classname.methodname").</summary>
        public string[] ProtectedMethods { get; set; }

        /// <summary>Gets or sets a value indicating whether to call CreateHttpClientAsync on the base class to create a new HttpClient instance (cannot be used when the HttpClient is injected).</summary>
        public bool UseHttpClientCreationMethod { get; set; }

        /// <summary>Gets or sets a value indicating whether to call CreateHttpRequestMessageAsync on the base class to create a new HttpRequestMethod.</summary>
        public bool UseHttpRequestMessageCreationMethod { get; set; }

        /// <summary>Gets or sets a value indicating whether DTO exceptions are wrapped in a SwaggerException instance (default: true).</summary>
        public bool WrapDtoExceptions { get; set; }

        /// <summary>Gets or sets the client class access modifier (default: public).</summary>
        public string ClientClassAccessModifier { get; set; }

        /// <summary>Gets or sets a value indicating whether to use and expose the base URL (default: true).</summary>
        public bool UseBaseUrl { get; set; }

        /// <summary>Gets or sets a value indicating whether to generate synchronous methods (not recommended, default: false).</summary>
        public bool GenerateSyncMethods { get; set; }

        /// <summary>
        /// Gets or sets the HttpClient type which will be used in the generation of the client code. By default the System.Net.Http.HttpClient
        /// will be used, but this can be overridden. Just keep in mind that the type you specify has the same default HttpClient method signatures.
        /// </summary>
        public string HttpClientType { get; set; }
    }
}
