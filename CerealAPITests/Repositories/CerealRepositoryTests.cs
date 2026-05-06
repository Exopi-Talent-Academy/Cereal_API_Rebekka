using Cereal_API.Models;
using Cereal_API.Models.Types;
using Cereal_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cereal_API.Tests.Repositories;

[TestClass]
public sealed class CerealRepositoryTests
{
    [TestMethod]
    public async Task GetCerealById_ReturnsCereal_WhenCerealExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CerealDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var testCereal = new Cereal(Guid.NewGuid(), 
                                     "Test Cereal",
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

        using (var context = new CerealDbContext(options))
        {
            context.Cereals.Add(testCereal);
            context.SaveChanges();
        }

        using (var context = new CerealDbContext(options))
        {
            var repository = new CerealRepository(context);

            // Act
            var result = await repository.GetCerealById(testCereal.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testCereal.Id, result.Id);
            Assert.AreEqual(testCereal.Name, result.Name);
        }
    }

    [TestMethod]
    public async Task GetCerealById_ThrowsException_WhenCerealDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CerealDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new CerealDbContext(options))
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
}
