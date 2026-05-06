using Cereal_API.Models;
using Cereal_API.Models.Types;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cereal_API.Extensions;

public class CsvToDataParser
{
    public static List<Cereal> CsvDataToCereals(string csvPath)
    {
        if (!Regex.IsMatch(csvPath, @"^.+\.csv$"))
        {
            throw new ArgumentException("The file given is not a csv-file.");
        }

        try
        {
            using var stream = File.OpenRead(csvPath);
        }
        catch (Exception ex)
        {
            throw new FileNotFoundException($"Could not find the file at path {csvPath}. Error: {ex.Message}");
        }

        using var reader = new StreamReader(csvPath);

        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };
        using var csv = new CsvHelper.CsvReader(reader, configuration);
        var records = csv.GetRecords<dynamic>().ToList();

        List<Cereal> cereals = new List<Cereal>();

        for (int i = 1; i < records.Count(); i++) // skip index 0 since it contains the column types
        {
            Cereal cereal;

            try
            {
                cereal = new Cereal
                (
                    Guid.NewGuid(),
                    records[i].name,
                    ManufacturersExtensions.GetManufacturers(records[i].mfr),
                    HotOrColdTypeExtensions.FromString(records[i].type),
                    int.Parse(records[i].calories),
                    int.Parse(records[i].protein),
                    int.Parse(records[i].fat),
                    int.Parse(records[i].sodium),
                    double.Parse(records[i].fiber, CultureInfo.InvariantCulture.NumberFormat), // need to specify the culture so it parses the double with a dot instead of a comma
                    double.Parse(records[i].carbo, CultureInfo.InvariantCulture.NumberFormat),
                    int.Parse(records[i].sugars),
                    int.Parse(records[i].potass),
                    VitaminAndMineralsTypeExtensions.FromString(records[i].vitamins),
                    DisplayShelfTypeExtensions.FromString(records[i].shelf),
                    double.Parse(records[i].weight, CultureInfo.InvariantCulture.NumberFormat),
                    double.Parse(records[i].cups, CultureInfo.InvariantCulture.NumberFormat),
                    double.Parse(records[i].rating, CultureInfo.InvariantCulture.NumberFormat)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing record at index {i}: {ex.Message}");
                continue; // skip this record and move to the next one
            }

            cereals.Add(cereal);
        }

        return cereals;
    }
}
