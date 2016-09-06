﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;

namespace NSwag.Integration.WebAPI.Models
{
    [JsonConverter(typeof(JsonInheritanceConverter), "discriminator")]
    [KnownType(typeof(Teacher))]
    public class Person
    {
        public Guid Id { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        [Required]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Weight { get; set; }

        public double Height { get; set; }

        public int Age { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public Person[] Children { get; set; }

        public Dictionary<string, SkillLevel> Skills { get; set; }
    }
}