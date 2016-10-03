﻿using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSwag.Integration.ClientPCL.Contracts;

namespace NSwag.Integration.ClientPCL.Tests
{
    [TestClass]
    public class PersonsControllerTests
    {
        [TestMethod]
        [TestCategory("integration")]
        public async Task GetAll_SerializationTest()
        {
            //// Arrange
            var personsClient = new PersonsClient("http://localhost:13452");

            //// Act
            var persons = await personsClient.GetAllAsync();

            //// Assert
            Assert.AreEqual(2, persons.Count);
        }

        [TestMethod]
        [TestCategory("integration")]
        public async Task GetAll_InheritanceTest()
        {
            //// Arrange
            var personsClient = new PersonsClient("http://localhost:13452");

            //// Act
            var persons = await personsClient.GetAllAsync();

            //// Assert
            Assert.AreEqual("SE", ((Teacher)persons[1]).Course); // inheritance test
        }

        [TestMethod]
        [TestCategory("integration")]
        public async Task Throw()
        {
            //// Arrange
            var id = Guid.NewGuid();
            var personsClient = new PersonsClient("http://localhost:13452");

            //// Act
            try
            {
                var persons = await personsClient.ThrowAsync(id);
            }
            catch (SwaggerException<PersonNotFoundException> exception)
            {
                //// Assert
                Assert.AreEqual(id, exception.Response.Id); 
            }
        }
    }
}
