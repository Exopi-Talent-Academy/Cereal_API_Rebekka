using Cereal_API.Controllers;
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
                                                     2.3,
                                                     3.2,
                                                     1,
                                                     1,
                                                     VitaminAndMineralsType.TwentyFive,
                                                     DisplayShelfType.One,
                                                     1,
                                                     0.33,
                                                     0.33);

    private readonly Cereal TestCereal2 = new Cereal(Guid.NewGuid(), 
                                                     "Test Cereal 2",
                                                     Manufacturers.P,
                                                     HotOrColdType.C,
                                                     4,
                                                     2,
                                                     3,
                                                     1,
                                                     1.5,
                                                     2.8,
                                                     0,
                                                     0,
                                                     VitaminAndMineralsType.OneHundred,
                                                     DisplayShelfType.Two,
                                                     0.5,
                                                     0.25,
                                                     0.25);

    [TestMethod]
    public async Task GetCereal_ReturnsCereal_WhenCerealExists()
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var cerealId = TestCereal1.Id;
        mockRepo.Setup(repo => repo.GetCerealById(cerealId)).ReturnsAsync(TestCereal1);

        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetCereal(cerealId);

        // Assert
        Assert.IsInstanceOfType(result.Value, typeof(Cereal));
        Assert.AreEqual(TestCereal1, result.Value);
    }

    [TestMethod]
    public async Task GetCereal_ReturnsNotFoundResult_WhenCerealDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var cerealId = Guid.NewGuid();
        mockRepo.Setup(repo => repo.GetCerealById(cerealId)).ThrowsAsync(new Exception("Cereal not found"));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetCereal(cerealId);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task GetCerealsAll_ReturnsAllCereals() 
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
        var result = await controller.GetCereals();

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(ViewResult));
        var viewResult = result.Result as ViewResult;
        Assert.AreEqual(expectedCereals.Count, (viewResult!.Model as IEnumerable<Cereal>)!.Count());
    }

    [TestMethod]
    public async Task GetCerealsAll_ReturnsBadRequest_WhenExceptionThrown() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        mockRepo.Setup(repo => repo.GetAllCereals()).ThrowsAsync(new Exception("Database error"));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetCereals();

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetCerealsFiltered_GivesFilteredCereals()
    {
        // Arrange
        string category = "Rating";
        OperatorType op = OperatorType.GreaterThan;
        string value = "0.2";

        var mockRepo = new Mock<ICerealRepository>();
        var expectedCereals = new List<Cereal>
        {
            TestCereal1,
            TestCereal2
        };
        mockRepo.Setup(repo => repo.GetFilteredCereals(category, op, value)).ReturnsAsync(expectedCereals);
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetCereals(category, op, value);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(ViewResult));
        var viewResult = result.Result as ViewResult;
        Assert.AreEqual(expectedCereals.Count, (viewResult!.Model as IEnumerable<Cereal>)!.Count());
    }

    [TestMethod]
    public async Task GetCerealsFiltered_ReturnsBadRequest_WhenGivenException()
    {
        // Arrange
        string category = "Mfr";
        OperatorType op = OperatorType.GreaterThan;
        string value = "K";

        var mockRepo = new Mock<ICerealRepository>();
        mockRepo.Setup(repo => repo.GetFilteredCereals(category, op, value)).ThrowsAsync(new Exception("Invalid operator."));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.GetCereals(category, op, value);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task PostCerealNoId_WithoutId_CreatesNewCereal() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        Cereal testCereal = TestCereal1 with { Id = Guid.Empty }; // Simulate a client sending a cereal without an ID
        mockRepo.Setup(repo => repo.CreateCereal(testCereal)).ReturnsAsync(TestCereal1);
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.PostCereal(testCereal);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        var createdAt = (CreatedAtActionResult)result.Result;
        Assert.AreNotEqual(testCereal.Id, ((Cereal)createdAt.Value!).Id); // The ID should be generated by the repository, not provided by the client
        Assert.AreEqual(testCereal.Name, ((Cereal)createdAt.Value!).Name);
    }

    [TestMethod]
    public async Task PostCerealNoId_WithId_ReturnsBadRequest() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.PostCereal(TestCereal1);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task PostCerealNoId_WhenExceptionThrown_ReturnsBadRequest() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        Cereal testCereal = TestCereal1 with { Id = Guid.Empty }; // Simulate a client sending a cereal without an ID
        mockRepo.Setup(repo => repo.CreateCereal(testCereal)).ThrowsAsync(new Exception("Database error"));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.PostCereal(testCereal);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task PostCerealWithId_WhenIdExists_UpdatesCereal() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var existingCerealId = TestCereal1.Id;
        Cereal updatedCereal = TestCereal1 with { Name = "Updated Cereal Name" };
        mockRepo.Setup(repo => repo.CerealExists(existingCerealId)).Returns(true);
        mockRepo.Setup(repo => repo.UpdateCereal(existingCerealId, updatedCereal)).ReturnsAsync(updatedCereal);
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.PostCereal(existingCerealId, updatedCereal);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task PostCerealWithId_WhenIdDoesNotExist_ReturnsBadRequest() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var nonExistentCerealId = Guid.NewGuid();
        Cereal updatedCereal = TestCereal1 with { Id = nonExistentCerealId, Name = "Updated Cereal Name" };
        mockRepo.Setup(repo => repo.CerealExists(nonExistentCerealId)).Returns(false);
        mockRepo.Setup(repo => repo.UpdateCereal(nonExistentCerealId, updatedCereal)).ThrowsAsync(new Exception("Cereal not found"));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.PostCereal(nonExistentCerealId, updatedCereal);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task PostCerealWithId_WhenExceptionThrown_ReturnsBadRequest() 
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var existingCerealId = TestCereal1.Id;
        Cereal updatedCereal = TestCereal1 with { Name = "Updated Cereal Name" };
        mockRepo.Setup(repo => repo.CerealExists(existingCerealId)).Returns(true);
        mockRepo.Setup(repo => repo.UpdateCereal(existingCerealId, updatedCereal)).ThrowsAsync(new Exception("Database error"));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.PostCereal(existingCerealId, updatedCereal);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task DeleteCereal_WithValidId_DeletesCereal()
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var existingCerealId = TestCereal1.Id;
        mockRepo.Setup(repo => repo.CerealExists(existingCerealId)).Returns(true);
        mockRepo.Setup(repo => repo.DeleteCereal(existingCerealId)).Returns(Task.FromResult(true));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.DeleteCereal(existingCerealId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task DeleteCereal_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var nonExistentCerealId = Guid.NewGuid();
        mockRepo.Setup(repo => repo.CerealExists(nonExistentCerealId)).Returns(false);
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.DeleteCereal(nonExistentCerealId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task DeleteCereal_WhenExceptionThrown_ReturnsBadRequest()
    {
        // Arrange
        var mockRepo = new Mock<ICerealRepository>();
        var existingCerealId = TestCereal1.Id;
        mockRepo.Setup(repo => repo.CerealExists(existingCerealId)).Returns(true);
        mockRepo.Setup(repo => repo.DeleteCereal(existingCerealId)).ThrowsAsync(new Exception("Database error"));
        var controller = new CerealController(mockRepo.Object);

        // Act
        var result = await controller.DeleteCereal(existingCerealId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}
