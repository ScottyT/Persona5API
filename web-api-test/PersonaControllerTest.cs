using Microsoft.AspNetCore.Mvc;
using Persona5Api.Controllers;
using Persona5API.Data;
using Persona5API.Models;
using Persona5API.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace web_api_test
{
    public class PersonaControllerTest
    {
        ApplicationDbContext _context;
        PersonasController _controller;
        PersonaService _service;

        public PersonaControllerTest()
        {
            _service = new PersonaService(_context);
            _controller = new PersonasController(_service);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var items = Assert.IsType<List<Persona>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            Random random = new Random();
            int randomInt = random.Next(1, 10);
            var notFoundResult = _controller.Get(randomInt);
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            var testId = 1;
            var okResult = _controller.Get(testId);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            var testId = 1;
            var okResult = _controller.Get(testId).Result as OkObjectResult;
            Assert.IsType<Persona>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as Persona).Id);
        }
    }
}
