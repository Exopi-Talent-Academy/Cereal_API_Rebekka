using Cereal_API.Models;
using Cereal_API.Models.Types;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cereal_API.Extensions;

public class CsvToDataParser
{
    public static List<Cereal> CsvDataToCereals(string csvPath)
    {
        try
        {
            Regex.Match(csvPath, @"^.+\.csv$");
        }
        catch (ArgumentException) 
        { 
            throw new ArgumentException("The file given is not a csv-file.");
        }

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<dynamic>();

        List<Cereal> cereals = new List<Cereal>();

        foreach (var record in records)
        {
            Cereal cereal = new Cereal
            (
                Guid.NewGuid(),
                record.name,
                (Manufacturers)Enum.Parse(typeof(Manufacturers), record.mfr),
                (HotOrColdType)Enum.Parse(typeof(HotOrColdType), record.type),
                record.calories,
                record.protein,
                record.fat,
                record.sodium,
                float.Parse(record.fiber),
                float.Parse(record.carbo),
                record.sugars,
                record.potass,
                (VitaminAndMineralsType)Enum.Parse(typeof(VitaminAndMineralsType), record.vitamins),
                (DisplayShelfType)Enum.Parse(typeof(DisplayShelfType), record.shelf),
                float.Parse(record.weight),
                float.Parse(record.cups),
                float.Parse(record.rating)
            );

            cereals.Add(cereal);
        }

        return cereals;
    }
}
