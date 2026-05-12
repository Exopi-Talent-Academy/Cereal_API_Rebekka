using Cereal_API.Models;
using Cereal_API.Models.Types;
using Cereal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Cereal_API.Tests.Repositories;

[TestClass]
public sealed class CerealRepositoryTests
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
                                                     Manufacturers.K,
                                                     HotOrColdType.C,
                                                     3,
                                                     4,
                                                     2,
                                                     1,
                                                     1.5,
                                                     2.5,
                                                     0,
                                                     0,
                                                     VitaminAndMineralsType.OneHundred,
                                                     DisplayShelfType.Two,
                                                     2,
                                                     0.66,
                                                     0.66);

    private readonly Cereal TestCereal3 = new Cereal(Guid.NewGuid(), 
                                                     "Test Cereal 3",
                                                     Manufacturers.P,
                                                     HotOrColdType.C,
                                                     4,
                                                     2,
                                                     3,
                                                     1,
                                                     2.0,
                                                     3.0,
                                                     1,
                                                     1,
                                                     VitaminAndMineralsType.Zero,
                                                     DisplayShelfType.Three,
                                                     3,
                                                     0.99,
                                                     0.99);

    private CerealDbContext GetTestDbContext(string databaseName) // give it a unique name to ensure isolation between tests
    {
        var options = new DbContextOptionsBuilder<CerealDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        var context = new CerealDbContext(options);

        context.Cereals.Add(TestCereal1);
        context.Cereals.Add(TestCereal2);
        context.Cereals.Add(TestCereal3);
        context.SaveChanges();

        return context;
    }

    [TestMethod]
    public async Task GetCerealById_ReturnsCereal_WhenCerealExists()
    {
        // Arrange
        using (var context = GetTestDbContext("GetCerealById_ReturnsCereal_WhenCerealExists"))
        {
            var repository = new CerealRepository(context);

            // Act
            var result = await repository.GetCerealById(TestCereal1.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TestCereal1.Id, result.Id);
            Assert.AreEqual(TestCereal1.Name, result.Name);
        }
    }

    [TestMethod]
    public async Task GetCerealById_ThrowsException_WhenCerealDoesNotExist()
    {
        // Arrange
        using (var context = GetTestDbContext("GetCerealById_ThrowsException_WhenCerealDoesNotExist"))
        {
            var repository = new CerealRepository(context);
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await repository.GetCerealById(nonExistentId);
            });
        }
    }

    [TestMethod]
    public async Task GetAllCereals_ReturnsAllCereals()
    {
        // Arrange
        using (var context = GetTestDbContext("GetAllCereals_ReturnsAllCereals"))
        {
            var repository = new CerealRepository(context);

            // Act
            var result = await repository.GetAllCereals();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(c => c.Id == TestCereal1.Id));
            Assert.IsTrue(result.Any(c => c.Id == TestCereal2.Id));
            Assert.IsTrue(result.Any(c => c.Id == TestCereal3.Id));
        }
    }

    [TestMethod]
    public async Task GetAllCereals_ThrowsException_WhenNoCerealsExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CerealDbContext>()
            .UseInMemoryDatabase(databaseName: "GetAllCereals_ThrowsException_WhenNoCerealsExist")
            .Options;

        using (var context = new CerealDbContext(options))
        {
            var repository = new CerealRepository(context);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await repository.GetAllCereals();
            });
        }
    }

    [TestMethod]
    [DataRow("Calories", "3", 2)]
    [DataRow("Vitamins", "50", 1)]
    public async Task GetFilteredCereals_ForGreaterThan_ReturnsFilteredCereals(string category, string value, int expectedCount)
    {
        // Arrange
        using (var context = GetTestDbContext("GetFilteredCereals_ForGreaterThan_ReturnsSortedCereals" + category))
        {
            var repository = new CerealRepository(context);

            // Act
            var result = await repository.GetFilteredCereals(category, OperatorType.GreaterThan, value);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, result.Count());
            var propertyInfo = typeof(Cereal).GetProperty(category);
            Assert.IsTrue(result.All(c => (int)propertyInfo!.GetValue(c)! > int.Parse(value)));
        }
    }

    [TestMethod]
    [DataRow("Type", "C", 2)]
    [DataRow("Type", "H", 1)]
    public async Task GetFilteredCereals_ForEqualsType_ReturnsFilteredCereals(string category, string value, int expectedCount)
    {
        // Arrange
        using (var context = GetTestDbContext("GetFilteredCereals_ForEqualsType_ReturnsFilteredCereals" + value))
        {
            var repository = new CerealRepository(context);

            // Act
            var result = await repository.GetFilteredCereals(category, OperatorType.Equals, value);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, result.Count());
            var propertyInfo = typeof(Cereal).GetProperty(category);
            Assert.IsTrue(result.All(c => propertyInfo!.GetValue(c)!.ToString() == value));
        }
    }

    [TestMethod]
    public async Task GetFilteredCereals_WhenGivenNonExistingCategory_ThrowsException()
    {
        string nonExistingCategory = "Milk";

        // Arrange
        using (var context = GetTestDbContext("GetFilteredCereals_WhenGivenNonExistingCategory_ThrowsException"))
        {
            var repository = new CerealRepository(context);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await repository.GetFilteredCereals(nonExistingCategory, default, "0.4");
            });
        }
    }

    [TestMethod]
    [DataRow("Name", OperatorType.SmallerThan, "Cheese")]
    [DataRow("Mfr", OperatorType.GreaterThanOrEqual, "K")]
    public async Task GetFilteredCereals_WhenGivenNonEqualsOrNotEqualsForString_ThrowsException(string category, OperatorType operation, string value)
    {
        // Arrange
        using (var context = GetTestDbContext("GetFilteredCereals_WhenGivenNonEqualsOrNotEqualsForString_ThrowsException" + category))
        {
            var repository = new CerealRepository(context);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await repository.GetFilteredCereals(category, operation, value);
            });
        }
    }

    [TestMethod]
    [DataRow("Protein", OperatorType.Equals, "testing123")]
    [DataRow("Shelf", OperatorType.SmallerThan, "I love ramen")]
    [DataRow("Rating", OperatorType.GreaterThan, "6.7 is a numbaaaaa")]
    public async Task GetFilteredCereal_WhenGivenTextForNumber_ThrowsException(string category, OperatorType operation, string value)
    {
        // Arrange
        using (var context = GetTestDbContext("GetFilteredCereals_WhenGivenTextForNumber_ThrowsException" + category))
        {
            var repository = new CerealRepository(context);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await repository.GetFilteredCereals(category, operation, value);
            });
        }
    }

    [TestMethod]
    public async Task CreateCereal_CreatesCereal()
    {
        // Arrange
        using (var context = GetTestDbContext("CreateCereal_CreatesCereal"))
        {
            var repository = new CerealRepository(context);
            var newCereal = new Cereal(Guid.Empty, 
                                        "New Test Cereal",
                                        Manufacturers.G,
                                        HotOrColdType.H,
                                        2,
                                        3,
                                        1,
                                        0,
                                        1.0,
                                        2.0,
                                        0,
                                        0,
                                        VitaminAndMineralsType.TwentyFive,
                                        DisplayShelfType.One,
                                        4,
                                        0.3,
                                        0.33);

            // Act
            var createdCereal = await repository.CreateCereal(newCereal);

            // Assert
            Assert.IsNotNull(createdCereal);
            Assert.AreNotEqual(newCereal.Id, createdCereal.Id); // ID should be generated by the server
            Assert.AreEqual(newCereal.Name, createdCereal.Name);
        }
    }

    [TestMethod]
    public async Task UpdateCereal_WithExistingId_UpdatesDatabase() 
    {
        // Arrange
        using (var context = GetTestDbContext("UpdateCereal_WithExistingId_UpdatesDatabase"))
        {
            var repository = new CerealRepository(context);
            var updatedCereal = TestCereal1 with { Name = "Updated Cereal Name" };

            // Act
            var result = await repository.UpdateCereal(TestCereal1.Id, updatedCereal);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(TestCereal1.Id, result.Id);
            Assert.AreEqual(updatedCereal.Name, result.Name);
        }
    }

    [TestMethod]
    public async Task UpdateCereal_WithNonExistingId_ThrowsException() 
    {
        // Arrange
        using (var context = GetTestDbContext("UpdateCereal_WithNonExistingId_ThrowsException"))
        {
            var repository = new CerealRepository(context);
            var nonExistentId = Guid.NewGuid();
            var updatedCereal = TestCereal1 with { Id = nonExistentId, Name = "Updated Cereal Name" };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await repository.UpdateCereal(nonExistentId, updatedCereal);
            });
        }
    }

    [TestMethod]
    public async Task DeleteCereal_WithExistingId_DeletesFromDatabase() 
    {
        // Arrange
        using (var context = GetTestDbContext("DeleteCereal_WithExistingId_DeletesFromDatabase"))
        {
            var repository = new CerealRepository(context);

            // Act
            var result = await repository.DeleteCereal(TestCereal1.Id);

            // Assert
            Assert.IsTrue(result);
            var deletedCereal = await context.Cereals.FindAsync(TestCereal1.Id);
            Assert.IsNull(deletedCereal);
        }
    }

    [TestMethod]
    public async Task DeleteCereal_WithNonExistingId_ThrowsException() 
    {
        // Arrange
        using (var context = GetTestDbContext("DeleteCereal_WithNonExistingId_ThrowsException"))
        {
            var repository = new CerealRepository(context);
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await repository.DeleteCereal(nonExistentId);
            });
        }
    }
}
