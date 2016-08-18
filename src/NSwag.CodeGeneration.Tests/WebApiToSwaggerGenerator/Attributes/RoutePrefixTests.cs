﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSwag.CodeGeneration.SwaggerGenerators.WebApi;

namespace NSwag.CodeGeneration.Tests.WebApiToSwaggerGenerator.Attributes
{
    [TestClass]
    public class RoutePrefixTests
    {
        [RoutePrefix("api/Persons")]
        public class PersonsController : ApiController
        {
           // GET api/values
            [HttpGet]
            public IEnumerable<Person> Get()
            {
                throw new NotImplementedException();
            }

            // GET api/values/5
            [HttpGet, Route("{id}")]
            public Person Get(int id)
            {
                throw new NotImplementedException();
            }

            // POST api/values
            [HttpPost]
            public void Post([FromBody]Person value)
            {
                throw new NotImplementedException();
            }

            // PUT api/values/5
            [HttpPut, Route("{id}")]
            public void Put(int id, [FromBody]Person value)
            {
                throw new NotImplementedException();
            }

            // DELETE api/values/5
            [HttpDelete, Route("{id}")]
            public void Delete(int id)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void When_controller_has_RoutePrefix_then_paths_are_correct()
        {
            //// Arrange
            var swaggerGen = new SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());

            //// Act
            var swagger = swaggerGen.GenerateForController<PersonsController>();

            //// Assert
            Assert.IsNotNull(swagger.Paths["/api/Persons"][SwaggerOperationMethod.Get]);
            Assert.IsNotNull(swagger.Paths["/api/Persons/{id}"][SwaggerOperationMethod.Get]);
            Assert.IsNotNull(swagger.Paths["/api/Persons"][SwaggerOperationMethod.Post]);
            Assert.IsNotNull(swagger.Paths["/api/Persons/{id}"][SwaggerOperationMethod.Put]);
            Assert.IsNotNull(swagger.Paths["/api/Persons/{id}"][SwaggerOperationMethod.Delete]);
        }
    }
}
