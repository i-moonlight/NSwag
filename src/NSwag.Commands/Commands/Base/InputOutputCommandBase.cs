//-----------------------------------------------------------------------
// <copyright file="InputOutputCommandBase.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using NConsole;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;

#pragma warning disable 1591

namespace NSwag.Commands.Base
{
    public abstract class InputOutputCommandBase : OutputCommandBase
    {
        [JsonIgnore]
        [Argument(Name = "Input", IsRequired = true, AcceptsCommandInput = true, Description = "A file path or URL to the data or the JSON data itself.")]
        public object Input { get; set; }

        [Argument(Name = "ServiceHost", IsRequired = false, Description = "Overrides the service host of the web document (optional, use '.' to remove the hostname).")]
        public string ServiceHost { get; set; }

        [Argument(Name = "ServiceSchemes", IsRequired = false, Description = "Overrides the allowed schemes of the web service (optional, comma separated, 'http', 'https', 'ws', 'wss').")]
        public string[] ServiceSchemes { get; set; }

        /// <exception cref="ArgumentException">The argument 'Input' was empty.</exception>
        protected async Task<SwaggerDocument> GetInputSwaggerDocument()
        {
            var document = Input as SwaggerDocument;
            if (document == null)
            {
                var inputString = Input.ToString();
                if (string.IsNullOrEmpty(inputString))
                    throw new ArgumentException("The argument 'Input' was empty.");

                if (IsJson(inputString))
                    document = await SwaggerDocument.FromJsonAsync(inputString).ConfigureAwait(false);
                else
                    document = await SwaggerDocument.FromUrlAsync(inputString).ConfigureAwait(false);
            }

            if (ServiceHost == ".")
                document.Host = string.Empty;
            else if (!string.IsNullOrEmpty(ServiceHost))
                document.Host = ServiceHost;

            if (ServiceSchemes != null && ServiceSchemes.Any())
                document.Schemes = ServiceSchemes.Select(s => (SwaggerSchema)Enum.Parse(typeof(SwaggerSchema), s, true)).ToList();

            return document;
        }

        /// <exception cref="ArgumentException">The argument 'Input' was empty.</exception>
        protected async Task<string> GetInputJsonAsync()
        {
            var inputString = Input.ToString();
            if (string.IsNullOrEmpty(inputString))
                throw new ArgumentException("The argument 'Input' was empty.");

            if (IsJson(inputString))
                return inputString;

            if (await DynamicApis.FileExistsAsync(inputString).ConfigureAwait(false))
                return await DynamicApis.FileReadAllTextAsync(inputString).ConfigureAwait(false);

            return await DynamicApis.HttpGetAsync(inputString).ConfigureAwait(false);
        }

        private bool IsJson(string data)
        {
            return !string.IsNullOrEmpty(data) && data.Contains("{");
        }
    }
}