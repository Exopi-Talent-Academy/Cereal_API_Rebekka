using Cereal_API.Models.Types;

namespace Cereal_API.Pages.Shared;

public class DisplayForCereal
{
    private readonly static string NameDisplayName = "Name";
    private readonly static string MfrDisplayName = "Manufacturer";
    private readonly static string TypeDisplayName = "Type";
    private readonly static string CaloriesDisplayName = "Calories per serving";
    private readonly static string ProteinDisplayName = "Protein";
    private readonly static string FatDisplayName = "Fat";
    private readonly static string SodiumDisplayName = "Sodium";
    private readonly static string FiberDisplayName = "Fiber";
    private readonly static string CarboDisplayName = "Carbohydrates";
    private readonly static string SugarsDisplayName = "Sugars";
    private readonly static string PotassDisplayName = "Potassium";
    private readonly static string VitaminsDisplayName = "Vitamins & Minerals";
    private readonly static string ShelfDisplayName = "Display Shelf";
    private readonly static string WeightDisplayName = "Weight per serving";
    private readonly static string CupsDisplayName = "Cups per serving";
    private readonly static string RatingDisplayName = "Rating";

    private readonly static string CaloriesMetric = "kcal";
    private readonly static string GramMetric = "g";
    private readonly static string MilligramMetric = "mg";
    private readonly static string PercentMetric = "%";
    private readonly static string OunceMetric = "oz";
    private readonly static string CupsMetric = "cups";

    /// <summary>
    /// Returns a user friendly display name for the given property name. If no display name is found, returns the original property name.
    /// </summary>
    /// <param name="propertyName">The name of the property for which to get a display name.</param>
    /// <returns>A user friendly display name for the specified property.</returns>
    public static string GetDisplayName(string propertyName)
    {
        return propertyName switch
        {
            nameof(Models.Cereal.Name) => NameDisplayName,
            nameof(Models.Cereal.Mfr) => MfrDisplayName,
            nameof(Models.Cereal.Type) => TypeDisplayName,
            nameof(Models.Cereal.Calories) => CaloriesDisplayName,
            nameof(Models.Cereal.Protein) => ProteinDisplayName,
            nameof(Models.Cereal.Fat) => FatDisplayName,
            nameof(Models.Cereal.Sodium) => SodiumDisplayName,
            nameof(Models.Cereal.Fiber) => FiberDisplayName,
            nameof(Models.Cereal.Carbo) => CarboDisplayName,
            nameof(Models.Cereal.Sugars) => SugarsDisplayName,
            nameof(Models.Cereal.Potass) => PotassDisplayName,
            nameof(Models.Cereal.Vitamins) => VitaminsDisplayName,
            nameof(Models.Cereal.Shelf) => ShelfDisplayName,
            nameof(Models.Cereal.Weight) => WeightDisplayName,
            nameof(Models.Cereal.Cups) => CupsDisplayName,
            nameof(Models.Cereal.Rating) => RatingDisplayName,
            _ => propertyName
        };
    }

    public static string GetDisplayForMfr(Manufacturers mfr)
    {
        return mfr.GetFullNameFromType();
    }

    public static string GetDisplayForType(HotOrColdType type)
    {
        return type.ToFullString();
    }

    public static string GetDisplayForVitamins(VitaminAndMineralsType vitamins, string name)
    {
        return vitamins.ToInt() + GetMetricForNumberDisplay(name);
    }

    public static string GetDisplayForShelf(DisplayShelfType shelf)
    {
        return shelf.ToInt().ToString();
    }

    /// <summary>
    /// Gets the appropriate metric to display for a given category. For example, "calories" would return "kcal", 
    /// while "protein" would return "g".
    /// </summary>
    /// <param name="category">The category for which to get the metric.</param>
    /// <returns>The metric corresponding to the specified category, or an empty string if no metric is found.</returns>
    public static string GetMetricForNumberDisplay(string category)
    {
        return category.ToLower().Trim() switch
        {
            "calories" => CaloriesMetric,
            "protein" => GramMetric,
            "fat" => GramMetric,
            "sodium" => MilligramMetric,
            "fiber" => GramMetric,
            "carbo" => GramMetric,
            "sugars" => GramMetric,
            "potass" => MilligramMetric,
            "vitamins" => PercentMetric,
            "weight" => OunceMetric,
            "cups" => CupsMetric,
            _ => ""
        };
    }
}