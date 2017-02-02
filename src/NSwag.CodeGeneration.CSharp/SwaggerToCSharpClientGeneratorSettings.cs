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

            GenerateResponseClasses = true;
            ResponseClass = "SwaggerResponse";
        }

        /// <summary>Gets or sets the full name of the base class.</summary>
        public string ClientBaseClass { get; set; }

        /// <summary>Gets or sets the full name of the configuration class (<see cref="ClientBaseClass"/> must be set).</summary>
        public string ConfigurationClass { get; set; }

        /// <summary>Gets or sets a value indicating whether to generate exception classes (default: true).</summary>
        public bool GenerateExceptionClasses { get; set; }

        /// <summary>Gets or sets the name of the exception class (supports the '{controller}' placeholder).</summary>
        public string ExceptionClass { get; set; }

        /// <summary>Gets or sets a value indicating whether to wrap success responses to allow full response access (experimental).</summary>
        public bool WrapSuccessResponses { get; set; }

        /// <summary>Gets or sets a value indicating whether to generate the response classes (only needed when WrapSuccessResponses == true, default: true).</summary>
        public bool GenerateResponseClasses { get; set; }

        /// <summary>Gets or sets the name of the response class (supports the '{controller}' placeholder).</summary>
        public string ResponseClass { get; set; }

        /// <summary>Gets or sets a value indicating whether an HttpClient instance is injected into the client.</summary>
        public bool InjectHttpClient { get; set; }

        /// <summary>Gets or sets a value indicating whether to call CreateHttpClientAsync on the base class to create a new HttpClient instance (cannot be used when the HttpClient is injected).</summary>
        public bool UseHttpClientCreationMethod { get; set; }

        /// <summary>Gets or sets a value indicating whether to call CreateHttpRequestMessageAsync on the base class to create a new HttpRequestMethod.</summary>
        public bool UseHttpRequestMessageCreationMethod { get; set; }
    }
}
