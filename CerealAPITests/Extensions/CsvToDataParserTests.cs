using Cereal_API.Extensions;
using Cereal_API.Models;
using Cereal_API.Models.Types;

namespace Cereal_API.Tests.Extensions;

[TestClass]
public sealed class CsvToDataParserTests
{
    [TestMethod]
    public void CsvDataToCereals_ShouldParseValidCsv()
    {
        // Arrange
        string csvPath = "../../../TestData/valid_cereal_data.csv";

        string cereal1Name = "100% Bran";
        Manufacturers cereal1Mfr = Manufacturers.N;
        int cereal1Calories = 70;
        VitaminAndMineralsType cereal1Vitamins = VitaminAndMineralsType.TwentyFive;
        DisplayShelfType cereal1Shelf = DisplayShelfType.Three;
        float cereal1Rating = 68.402973f;

        string cereal2Name = "Apple Jacks";
        Manufacturers cereal2Mfr = Manufacturers.K;
        int cereal2Calories = 110;
        VitaminAndMineralsType cereal2Vitamins = VitaminAndMineralsType.Zero;
        DisplayShelfType cereal2Shelf = DisplayShelfType.Two;
        float cereal2Rating = 33.174094f;

        string cereal3Name = "Nut&Honey Crunch";
        Manufacturers cereal3Mfr = Manufacturers.K;
        int cereal3Calories = 120;
        VitaminAndMineralsType cereal3Vitamins = VitaminAndMineralsType.OneHundred;
        DisplayShelfType cereal3Shelf = DisplayShelfType.Two;
        float cereal3Rating = 29.924285f;

        // Act
        List<Cereal> cereals = CsvToDataParser.CsvDataToCereals(csvPath);

        // Assert
        Assert.IsNotNull(cereals);
        Assert.IsNotEmpty(cereals);

        Assert.AreEqual(cereal1Name, cereals[0].Name);
        Assert.AreEqual(cereal1Mfr, cereals[0].Mfr);
        Assert.AreEqual(cereal1Calories, cereals[0].Calories);
        Assert.AreEqual(cereal1Vitamins, cereals[0].Vitamins);
        Assert.AreEqual(cereal1Shelf, cereals[0].Shelf);
        Assert.AreEqual(cereal1Rating, cereals[0].Rating);

        Assert.AreEqual(cereal2Name, cereals[1].Name);
        Assert.AreEqual(cereal2Mfr, cereals[1].Mfr);
        Assert.AreEqual(cereal2Calories, cereals[1].Calories);
        Assert.AreEqual(cereal2Vitamins, cereals[1].Vitamins);
        Assert.AreEqual(cereal2Shelf, cereals[1].Shelf);
        Assert.AreEqual(cereal2Rating, cereals[1].Rating);

        Assert.AreEqual(cereal3Name, cereals[2].Name);
        Assert.AreEqual(cereal3Mfr, cereals[2].Mfr);
        Assert.AreEqual(cereal3Calories, cereals[2].Calories);
        Assert.AreEqual(cereal3Vitamins, cereals[2].Vitamins);
        Assert.AreEqual(cereal3Shelf, cereals[2].Shelf);
        Assert.AreEqual(cereal3Rating, cereals[2].Rating);
    }

    [TestMethod]
    public void CsvDataToCereals_ShouldThrowArgumentException_WhenFileIsNotCsv()
    {
        // Arrange
        string invalidFilePath = "../../../MSTestSettings.cs";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => CsvToDataParser.CsvDataToCereals(invalidFilePath));
    }

    [TestMethod]
    public void CsvDataToCereals_ShouldNotAddDataRow_WhenItHasInvalidData()
    {
        // Arrange
        string csvPath = "../../../TestData/has_one_invalid_cereal_data.csv";

        // Act
        List<Cereal> cereals = CsvToDataParser.CsvDataToCereals(csvPath);

        // Assert
        Assert.IsNotNull(cereals);
        Assert.IsNotEmpty(cereals);
        Assert.HasCount(2, cereals);
    }
}
