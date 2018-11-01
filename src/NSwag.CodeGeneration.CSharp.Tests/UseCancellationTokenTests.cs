﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.CodeGeneration.CSharp.Models;
using NSwag.SwaggerGeneration.WebApi;
using Xunit;

namespace NSwag.CodeGeneration.CSharp.Tests
{
    public class UseCancellationTokenTests
    {
        public class TestController : Controller
        {
            [Route("Foo")]
            public string Foo(string test, bool test2)
            {
                throw new NotImplementedException();
            }

            [Route("Bar")]
            public void Bar()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public async Task When_controllerstyleispartial_and_usecancellationtokenistrue_and_requesthasnoparameter_then_cancellationtoken_is_added()
        {
            //// Arrange
            var swaggerGen = new WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());
            var document = await swaggerGen.GenerateForControllerAsync<TestController>();

            //// Act
            var codeGen = new SwaggerToCSharpControllerGenerator(document, new SwaggerToCSharpControllerGeneratorSettings
            {
                UseCancellationToken = true
            });
            var code = codeGen.GenerateFile();

            //// Assert
            Assert.Contains("(System.Threading.CancellationToken cancellationToken)", code);
            Assert.Contains("_implementation.BarAsync(cancellationToken)", code);
            Assert.Contains("System.Threading.Tasks.Task BarAsync(System.Threading.CancellationToken cancellationToken)", code);
        }

        [Fact]
        public async Task When_controllerstyleispartial_and_usecancellationtokenistrue_and_requesthasparameter_then_cancellationtoken_is_added()
        {
            //// Arrange
            var swaggerGen = new WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());
            var document = await swaggerGen.GenerateForControllerAsync<TestController>();

            //// Act
            var codeGen = new SwaggerToCSharpControllerGenerator(document, new SwaggerToCSharpControllerGeneratorSettings
            {
                UseCancellationToken = true
            });
            var code = codeGen.GenerateFile();

            //// Assert
            Assert.Contains("System.Threading.Tasks.Task<string> FooAsync(string test, bool test2, System.Threading.CancellationToken cancellationToken)", code);
            Assert.Contains("_implementation.FooAsync(test, test2, cancellationToken);", code);
            Assert.Contains("public System.Threading.Tasks.Task<string> Foo(string test, bool test2, System.Threading.CancellationToken cancellationToken)", code);
        }

        [Fact]
        public async Task When_controllerstyleisabstract_and_usecancellationtokenistrue_and_requesthasnoparameter_then_cancellationtoken_is_added()
        {
            //// Arrange
            var swaggerGen = new WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());
            var document = await swaggerGen.GenerateForControllerAsync<TestController>();

            //// Act
            var codeGen = new SwaggerToCSharpControllerGenerator(document, new SwaggerToCSharpControllerGeneratorSettings
            {
                ControllerStyle = CSharpControllerStyle.Abstract,
                UseCancellationToken = true
            });
            var code = codeGen.GenerateFile();

            //// Assert
            Assert.Contains("abstract System.Threading.Tasks.Task Bar(System.Threading.CancellationToken cancellationToken)", code);
        }

        [Fact]
        public async Task When_controllerstyleisabstract_and_usecancellationtokenistrue_and_requesthasparameter_then_cancellationtoken_is_added()
        {
            //// Arrange
            var swaggerGen = new WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());
            var document = await swaggerGen.GenerateForControllerAsync<TestController>();

            //// Act
            var codeGen = new SwaggerToCSharpControllerGenerator(document, new SwaggerToCSharpControllerGeneratorSettings
            {
                ControllerStyle = CSharpControllerStyle.Abstract,
                UseCancellationToken = true
            });
            var code = codeGen.GenerateFile();

            //// Assert
            Assert.Contains("System.Threading.Tasks.Task<string> Foo(string test, bool test2, System.Threading.CancellationToken cancellationToken)", code);
        }

        [Fact]
        public async Task When_usecancellationtokenparameter_notsetted_then_cancellationtoken_isnot_added()
        {
            //// Arrange
            var swaggerGen = new WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());
            var document = await swaggerGen.GenerateForControllerAsync<TestController>();

            //// Act
            var codeGen = new SwaggerToCSharpControllerGenerator(document, new SwaggerToCSharpControllerGeneratorSettings
            {
                ControllerStyle = CSharpControllerStyle.Abstract
            });
            var code = codeGen.GenerateFile();

            //// Assert
            Assert.False(code.Contains("System.Threading.CancellationToken cancellationToken"));
        }
    }
}
