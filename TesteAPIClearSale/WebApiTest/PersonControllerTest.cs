using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using TesteAPIClearSale.Models;
using TesteAPIClearSale.Controllers;
using TesteAPIClearSale.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest
{
    public class PersonControllerTest
    {      
            PersonController _controller;
            IPersonService _service;

            public PersonControllerTest()
            {
                _service = new PersonServiceFake();
                _controller = new PersonController(_service);
            }

        //Testing the Get Method

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        // Testing Get Method with Find All
        [Fact]
        public void Get_WhenCalled_ReturnsFindAll()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var people = Assert.IsType<List<Person>>(okResult.Value);
            Assert.Equal(8, people.Count);


        }

        // Tests GetById Method passing an id that doesn't exists
        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(20); // aleatory number here

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        // Tests GetById Method passing an id that exists
        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
           
            // Act
            var okResult = _controller.Get(5); 

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = 5;

            // Act
            var okResult = _controller.Get(testGuid).Result as OkObjectResult;

            // Assert
            Assert.IsType<Person>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as Person).Id);
        }

        //Tests Post passing missing the name
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingPerson = new Person()
            {
                Age = 25,
                Address = "Rua da Paz - Santos - Brasil",
                Gender = "Female"
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingPerson);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        //Tests Post passing the person correctly
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Person testPerson = new Person()
            {
                Name = "Yasmin",
                Age = 25,
                Address = "Rua da Paz - Santos - Brasil",
                Gender = "Female"
            };

            // Act
            var createdResponse = _controller.Post(testPerson);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        //Tests Post correctly returning the right values.
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            Person testPerson = new Person()
            {
                Name = "Yasmin",
                Age = 25,
                Address = "Rua da Paz - Santos - Brasil",
                Gender = "Female"
            };

            // Act
            var createdResponse = _controller.Post(testPerson) as CreatedAtActionResult;
            var p = createdResponse.Value as Person;

            // Assert
            Assert.IsType<Person>(p);
            Assert.Equal("Yasmin", p.Name);
        }

        //Testing Delete Method with wrong ID
        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 20; //aleatory number

            // Act
            var badResponse = _controller.Delete(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        //Testing Delete Method with right ID
        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var ExistingId = 5; 

            // Act
            var okResponse = _controller.Delete(ExistingId);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        //Tests Delete method removing one person
        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 5;

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.Equal(7, _service.FindAll().Count());
        }

    }
}
