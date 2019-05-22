using Microsoft.AspNetCore.Mvc;
using Persona5Api.Controllers;
using Persona5API.Data;
using Persona5API.Models;
using Persona5API.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace web_api_test
{
    public class PersonaControllerTest
    {
        //PersonasController _controller;
        //IPersonaService _service;

        //public PersonaControllerTest()
        //{
        //    _service = new PersonaAppContext();
        //    _controller = new PersonasController(_service);
        //}
        //[Fact]
        //public void Get_WhenCalled_ReturnsOkResult()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IPersonaService>();
        //    mockRepo.Setup(repo => repo.GetAll())
        //        .Returns(GetTestSessions());
        //    var controller = new PersonasController(mockRepo.Object);
        //    // Act
        //    var okResult = controller.Get();

        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult.Result);
        //}

        [Fact]
        public async Task Get_ReturnsAViewResult()
        {
            var mockRepo = new Mock<IPersonaService>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestSessions());
            var controller = new PersonaController(mockRepo.Object);

            var result = await controller.Index();

            Assert.IsType<ViewResult>(result);
            
        }

        private async Task<List<Persona>> GetTestSessions()
        {
            var personas = new List<Persona>();
            personas.Add(new Persona()
            {
                Id = 4,
                Name = "Arsene",
                Level = 1,
                Arcana = "Fool",
                Stats =
                    {
                        Id = 1,
                        Strength = 2,
                        Magic = 2,
                        Endurance = 2,
                        Agility = 3,
                        Luck = 1
                    },
                Description = "A being based off the main character of Maurice Leblanc's novels, Arsene Lupin. He appears everywhere and is a master of disguise. He is known to help law-abiding citizens."
            });
            return personas;
        }

        //[Fact]
        //public void Get_WhenCalled_ReturnsAllItems()
        //{
        //    var okResult = _controller.Get().Result as OkObjectResult;
        //    var items = Assert.IsType<List<Persona>>(okResult.Value);
        //    Assert.Equal(3, items.Count);
        //}

        //[Fact]
        //public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        //{
        //    Random random = new Random();
        //    int randomInt = random.Next(1, 10);
        //    var notFoundResult = _controller.Get(randomInt);
        //    Assert.IsType<NotFoundResult>(notFoundResult.Result);
        //}

        //[Fact]
        //public void GetById_ExistingIdPassed_ReturnsOkResult()
        //{
        //    var testId = 1;
        //    var okResult = _controller.Get(testId);
        //    Assert.IsType<OkObjectResult>(okResult.Result);
        //}

        //[Fact]
        //public void GetById_ExistingIdPassed_ReturnsRightItem()
        //{
        //    var testId = 1;
        //    var okResult = _controller.Get(testId).Result as OkObjectResult;
        //    Assert.IsType<Persona>(okResult.Value);
        //    Assert.Equal(testId, (okResult.Value as Persona).Id);
        //}
    }
}
