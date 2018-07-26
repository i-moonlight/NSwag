﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

namespace NSwag.Integration.ClientPCL.Contracts
{
    #pragma warning disable // Disable all warnings

    
    
    
    
    

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class GeoPoint 
    {
        [Newtonsoft.Json.JsonProperty("Latitude", Required = Newtonsoft.Json.Required.Always)]
        public double Latitude { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Longitude", Required = Newtonsoft.Json.Required.Always)]
        public double Longitude { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
        
        public static GeoPoint FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GeoPoint>(data, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class GenericRequestOfAddressAndPerson 
    {
        [Newtonsoft.Json.JsonProperty("Item1", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Address Item1 { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Item2", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Person Item2 { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
        
        public static GenericRequestOfAddressAndPerson FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GenericRequestOfAddressAndPerson>(data, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class Address 
    {
        [Newtonsoft.Json.JsonProperty("IsPrimary", Required = Newtonsoft.Json.Required.Always)]
        public bool IsPrimary { get; set; }
    
        [Newtonsoft.Json.JsonProperty("City", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string City { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
        
        public static Address FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Address>(data, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
    
    }
    
    [Newtonsoft.Json.JsonConverter(typeof(JsonInheritanceConverter), "discriminator")]
    [JsonInheritanceAttribute("Teacher", typeof(Teacher))]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class Person 
    {
        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid Id { get; set; }
    
        /// <summary>Gets or sets the first name.</summary>
        [Newtonsoft.Json.JsonProperty("FirstName", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(int.MaxValue, MinimumLength = 2)]
        public string FirstName { get; set; }
    
        /// <summary>Gets or sets the last name.</summary>
        [Newtonsoft.Json.JsonProperty("LastName", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string LastName { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Gender", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Gender Gender { get; set; }
    
        [Newtonsoft.Json.JsonProperty("DateOfBirth", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTime DateOfBirth { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Weight", Required = Newtonsoft.Json.Required.Always)]
        public decimal Weight { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Height", Required = Newtonsoft.Json.Required.Always)]
        public double Height { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Age", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(5, 99)]
        public int Age { get; set; }
    
        [Newtonsoft.Json.JsonProperty("AverageSleepTime", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.TimeSpan AverageSleepTime { get; set; }
    
        [Newtonsoft.Json.JsonProperty("Address", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public Address Address { get; set; } = new Address();
    
        [Newtonsoft.Json.JsonProperty("Children", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Collections.ObjectModel.ObservableCollection<Person> Children { get; set; } = new System.Collections.ObjectModel.ObservableCollection<Person>();
    
        [Newtonsoft.Json.JsonProperty("Skills", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.Dictionary<string, SkillLevel> Skills { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
        
        public static Person FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(data, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum Gender
    {
        [System.Runtime.Serialization.EnumMember(Value = "Male")]
        Male = 0,
    
        [System.Runtime.Serialization.EnumMember(Value = "Female")]
        Female = 1,
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    public enum SkillLevel
    {
        Low = 0,
    
        Medium = 1,
    
        Height = 2,
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class Teacher : Person
    {
        [Newtonsoft.Json.JsonProperty("Course", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Course { get; set; }
    
        [Newtonsoft.Json.JsonProperty("SkillLevel", Required = Newtonsoft.Json.Required.Always)]
        public SkillLevel SkillLevel { get; set; } = NSwag.Integration.ClientPCL.Contracts.SkillLevel.Medium;
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
        
        public static Teacher FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Teacher>(data, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.50.0 (Newtonsoft.Json v9.0.0.0)")]
    [Newtonsoft.Json.JsonObjectAttribute]
    public partial class PersonNotFoundException : System.Exception
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid Id { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
        
        public static PersonNotFoundException FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PersonNotFoundException>(data, new Newtonsoft.Json.JsonSerializerSettings { PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All, Converters = new Newtonsoft.Json.JsonConverter[] { new Newtonsoft.Json.Converters.StringEnumConverter() } });
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    internal class JsonInheritanceAttribute : System.Attribute
    {
        public JsonInheritanceAttribute(string key, System.Type type)
        {
            Key = key;
            Type = type;
        }
    
        public string Key { get; }
    
        public System.Type Type { get; }
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    internal class JsonInheritanceConverter : Newtonsoft.Json.JsonConverter
    {
        internal static readonly string DefaultDiscriminatorName = "discriminator";
    
        private readonly string _discriminator;
    
        [System.ThreadStatic]
        private static bool _isReading;
    
        [System.ThreadStatic]
        private static bool _isWriting;
    
        public JsonInheritanceConverter()
        {
            _discriminator = DefaultDiscriminatorName;
        }
    
        public JsonInheritanceConverter(string discriminator)
        {
            _discriminator = discriminator;
        }
    
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            try
            {
                _isWriting = true;
    
                var jObject = Newtonsoft.Json.Linq.JObject.FromObject(value, serializer);
                jObject.AddFirst(new Newtonsoft.Json.Linq.JProperty(_discriminator, GetSubtypeDiscriminator(value.GetType())));
                writer.WriteToken(jObject.CreateReader());
            }
            finally
            {
                _isWriting = false;
            }
        }
    
        public override bool CanWrite
        {
            get
            {
                if (_isWriting)
                {
                    _isWriting = false;
                    return false;
                }
                return true;
            }
        }
    
        public override bool CanRead
        {
            get
            {
                if (_isReading)
                {
                    _isReading = false;
                    return false;
                }
                return true;
            }
        }
    
        public override bool CanConvert(System.Type objectType)
        {
            return true;
        }
    
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jObject = serializer.Deserialize<Newtonsoft.Json.Linq.JObject>(reader);
            if (jObject == null)
                return null;
    
            var discriminator = Newtonsoft.Json.Linq.Extensions.Value<string>(jObject.GetValue(_discriminator));
            var subtype = GetObjectSubtype(objectType, discriminator);
    
            try
            {
                _isReading = true;
                return serializer.Deserialize(jObject.CreateReader(), subtype);
            }
            finally
            {
                _isReading = false;
            }
        }
    
        private System.Type GetObjectSubtype(System.Type objectType, string discriminator)
        {
            foreach (var attribute in System.Reflection.CustomAttributeExtensions.GetCustomAttributes<JsonInheritanceAttribute>(System.Reflection.IntrospectionExtensions.GetTypeInfo(objectType), true))
            {
                if (attribute.Key == discriminator)
                    return attribute.Type;
            }
    
            return objectType;
        }
    
        private string GetSubtypeDiscriminator(System.Type objectType)
        {
            foreach (var attribute in System.Reflection.CustomAttributeExtensions.GetCustomAttributes<JsonInheritanceAttribute>(System.Reflection.IntrospectionExtensions.GetTypeInfo(objectType), true))
            {
                if (attribute.Type == objectType)
                    return attribute.Key;
            }
    
            return objectType.Name;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class FileParameter
    {
        public FileParameter(System.IO.Stream data) 
            : this (data, null)
        {
        }

        public FileParameter(System.IO.Stream data, string fileName)
        {
            Data = data;
            FileName = fileName;
        }

        public System.IO.Stream Data { get; private set; }

        public string FileName { get; private set; }
    }

    public partial class FileResponse : System.IDisposable
    {
        private System.IDisposable _client; 
        private System.IDisposable _response; 

        public int StatusCode { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public System.IO.Stream Stream { get; private set; }

        public bool IsPartial
        {
            get { return StatusCode == 206; }
        }

        public FileResponse(int statusCode, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.IO.Stream stream, System.IDisposable client, System.IDisposable response)
        {
            StatusCode = statusCode; 
            Headers = headers; 
            Stream = stream; 
            _client = client; 
            _response = response;
        }

        public void Dispose() 
        {
            if (Stream != null)
                Stream.Dispose();
            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class SwaggerResponse
    {
        public int StatusCode { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }
        
        public SwaggerResponse(int statusCode, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers) 
        {
            StatusCode = statusCode; 
            Headers = headers;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class SwaggerResponse<TResult> : SwaggerResponse
    {
        public TResult Result { get; private set; }
        
        public SwaggerResponse(int statusCode, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result) 
            : base(statusCode, headers)
        {
            Result = result;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class GeoClientException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public GeoClientException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Response = response; 
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class GeoClientException<TResult> : GeoClientException
    {
        public TResult Result { get; private set; }

        public GeoClientException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException) 
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class PersonsClientException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public PersonsClientException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Response = response; 
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "11.17.13.0 (NJsonSchema v9.10.50.0 (Newtonsoft.Json v9.0.0.0))")]
    public partial class PersonsClientException<TResult> : PersonsClientException
    {
        public TResult Result { get; private set; }

        public PersonsClientException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException) 
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}