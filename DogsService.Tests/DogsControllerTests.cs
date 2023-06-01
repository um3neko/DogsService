using AutoMapper;
using CodeBridge.Models.DTOs.In;
using CodeBridge.Models.Entities;
using CodeBridge.Repositories.DogRepository;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DogsService.Tests;

public class DogsControllerTests
{
    /// <summary>
    /// Get Dogs Endpoint
    /// </summary>
    [Fact]
    public void GetDogs_DefaultParameters_ReturnsOkResult()
    {
        // Arrange
        var dogRepository = A.Fake<IDogRepository>();
        var mapper = A.Fake<IMapper>();
        var controller = new DogsController(dogRepository, mapper);

        // Act
        var result = controller.GetDogs();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetDogs_ValidAttribute_ReturnsOkResult()
    {
        // Arrange
        var fakeDogRepository = A.Fake<IDogRepository>();
        var fakeMapper = A.Fake<IMapper>();
        var controller = new DogsController(fakeDogRepository, fakeMapper);

        // Act
        var result = controller.GetDogs("weight");

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetDogs_InvalidAttribute_ReturnsBadRequest()
    {
        // Arrange
        var fakeDogRepository = A.Fake<IDogRepository>();
        var fakeMapper = A.Fake<IMapper>();
        var controller = new DogsController(fakeDogRepository, fakeMapper);
        var invalideAttribute = "invalide attribute";

        A.CallTo(() => fakeDogRepository.OrderDogsByAttribute(invalideAttribute, Order.asc)).Throws<InvalidOperationException>();
        // Act
        var result = controller.GetDogs(invalideAttribute);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid attribute", (result as BadRequestObjectResult).Value);
    }

    [Fact]
    public void GetDogs_OrderByDesc_ReturnsOkResult()
    {
        // Arrange
        var fakeDogRepository = A.Fake<IDogRepository>();
        var fakeMapper = A.Fake<IMapper>();
        var controller = new DogsController(fakeDogRepository, fakeMapper);

        // Act
        var result = controller.GetDogs("weight", true);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    /// <summary>
    /// Add Dog Endpoing
    /// </summary>
    /// <returns></returns>

    [Fact]
    public async Task AddDog_DuplicateName_ReturnsBadRequestResult()
    {
        // Arrange
        var dogRepository = A.Fake<IDogRepository>();
        var mapper = A.Fake<IMapper>();
        var controller = new DogsController(dogRepository, mapper);
        var model = new CreateDogDTO();

        A.CallTo(() => dogRepository.AddDog(A<Dog>.Ignored)).Throws<DbUpdateException>();

        // Act
        var result = await controller.AddDog(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Name should be unique. Try another name", (result as BadRequestObjectResult).Value);
    }

    [Fact]
    public async Task AddDog_ValidModel_ReturnsOkResult()
    {
        // Arrange
        var dogRepository = A.Fake<IDogRepository>();
        var mapper = A.Fake<IMapper>();
        var controller = new DogsController(dogRepository, mapper);
        var model = new CreateDogDTO();

        A.CallTo(() => dogRepository.AddDog(A<Dog>.Ignored)).Returns(Task.FromResult(true));

        // Act
        var result = await controller.AddDog(model);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

}

    



