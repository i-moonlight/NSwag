﻿using System.Collections.Generic;

namespace NSwag.AspNetCore
{
    /// <summary>The OAuth client settings.</summary>
    public class OAuth2Settings
    {
        /// <summary>Gets or sets the client identifier.</summary>
        public string ClientId { get; set; } = "client_id";

        /// <summary>Gets or sets the client secret.</summary>
        public string ClientSecret { get; set; } = "client_secret";

        /// <summary>Gets or sets the realm.</summary>
        public string Realm { get; set; } = "realm";

        /// <summary>Gets or sets the name of the application.</summary>
        public string AppName { get; set; } = "app_name";

        /// <summary>Gets or sets the scope separator.</summary>
        public string ScopeSeparator { get; set; } = " ";

        /// <summary>Gets or sets the additional query string parameters.</summary>
        public IDictionary<string, string> AdditionalQueryStringParameters { get; } = new Dictionary<string, string>();
    }
}