//-----------------------------------------------------------------------
// <copyright file="SwaggerTag.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using Newtonsoft.Json;

namespace NSwag
{
    /// <summary>Describes a Swagger tag.</summary>
    public class SwaggerTag
    {
        /// <summary>Gets or sets the name.</summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        [JsonProperty(PropertyName = "description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>Gets or sets the external documentation.</summary>
        [JsonProperty(PropertyName = "externalDocs", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SwaggerExternalDocumentation ExternalDocumentation { get; set; }
    }
}