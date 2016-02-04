﻿//-----------------------------------------------------------------------
// <copyright file="SwaggerParameterKind.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NSwag
{
    /// <summary>Enumeration of the parameter kinds. </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SwaggerParameterKind
    {
        /// <summary>An undefined kind.</summary>
        [EnumMember(Value = "undefined")]
        Undefined,

        /// <summary>A JSON object as POST or PUT body (only one parameter of this type is allowed). </summary>
        [EnumMember(Value = "body")]
        Body,

        /// <summary>A query key-value pair. </summary>
        [EnumMember(Value = "query")]
        Query,

        /// <summary>An URL path placeholder. </summary>
        [EnumMember(Value = "path")]
        Path,

        /// <summary>A HTTP header parameter.</summary>
        [EnumMember(Value = "header")]
        Header,

        /// <summary>A form data parameter.</summary>
        [EnumMember(Value = "formData")]
        FormData
    }
}