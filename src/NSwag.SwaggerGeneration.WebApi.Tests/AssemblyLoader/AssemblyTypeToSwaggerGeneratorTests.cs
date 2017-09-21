﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NSwag.SwaggerGeneration.WebApi.Tests.AssemblyLoader
{
    [TestClass]
    public class AssemblyTypeToSwaggerGeneratorTests
    {
        [TestMethod]
        public async Task When_loading_type_from_assembly_then_correct_count_of_properties_are_loaded()
        {
            //// Arrange
            var assemblyPath = "../../../NSwag.Demo.Web/bin/NSwag.Demo.Web.dll";
            var generator = new AssemblyTypeToSwaggerGenerator(new AssemblyTypeToSwaggerGeneratorSettings
            {
                AssemblySettings =
                {
                    AssemblyPaths = new [] { assemblyPath }
                }
            });

            //// Act
            var document = await generator.GenerateAsync(new[] { "NSwag.Demo.Web.Models.Person" });

            //// Assert
            Assert.AreEqual(6, document.Definitions["Person"].Properties.Count);
        }
    }
}