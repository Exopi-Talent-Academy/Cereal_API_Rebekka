using Cereal_Api.Controllers;
using Cereal_API.Models;
using Cereal_API.Models.Types;
using Cereal_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Cereal_API.Tests.Controllers;

[TestClass]
public sealed class CerealControllerTests
{
    private readonly Cereal TestCereal1 = new Cereal(Guid.NewGuid(), 
                                                     "Test Cereal 1",
                                                     Manufacturers.A,
                                                     HotOrColdType.H,
                                                     5,
                                                     3,
                                                     4,
                                                     2,
                                                     2.3f,
                                                     3.2f,
                                                     1,
                                                     1,
                                                     VitaminAndMineralsType.TwentyFive,
                                                     DisplayShelfType.One,
                                                     1f,
                                                     0.33f,
                                                     0.33f);

    private readonly Cereal TestCereal2 = new Cereal(Guid.NewGuid(), 
                                                     "Test Cereal 2",
                                                     Manufacturers.P,
                                                     HotOrColdType.C,
                                                     4,
                                                     2,
                                                     3,
                                                     1,
                                                     1.5f,
                                                     2.8f,
                                                     0,
                                                     0,
                                                     VitaminAndMineralsType.OneHundred,
                                                     DisplayShelfType.Two,
                                                     0.5f,
                                                     0.25f,
                                                     0.25f);

    [TestMethod]
    public async Task GetCereal_ReturnsOkResult_WhenCerealExists()
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var cerealId = TestCereal1.Id;
        mockRepo.Setup(repo => repo.GetCerealById(cerealId)).ReturnsAsync(TestCereal1);

        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetCereal(cerealId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.AreEqual(TestCereal1, okResult!.Value);
    }

    [TestMethod]
    public async Task GetCereal_ReturnsNotFoundResult_WhenCerealDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var cerealId = Guid.NewGuid();
        mockRepo.Setup(repo => repo.GetCerealById(cerealId)).ReturnsAsync((Cereal?)null);
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetCereal(cerealId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task GetAllCereals_ReturnsAllCereals() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var expectedCereals = new List<Cereal>
        {
            TestCereal1,
            TestCereal2
        };
        mockRepo.Setup(repo => repo.GetAllCereals()).ReturnsAsync(expectedCereals);
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetAllCereals();

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.AreEqual(expectedCereals.Count, (okResult!.Value as IEnumerable<Cereal>)!.Count());
    }
}
