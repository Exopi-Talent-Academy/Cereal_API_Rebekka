namespace Cereal_API.Models.Types;

public enum Manufacturers
{
    A,
    G,
    K,
    N,
    P,
    Q,
    R
}

public static class ManufacturersExtensions
{
    public static Manufacturers GetManufacturers(this string manufacturer)
    {
        return manufacturer switch
        {
            "A" => Manufacturers.A,
            "G" => Manufacturers.G,
            "K" => Manufacturers.K,
            "N" => Manufacturers.N,
            "P" => Manufacturers.P,
            "Q" => Manufacturers.Q,
            "R" => Manufacturers.R,
            _ => throw new ArgumentOutOfRangeException(nameof(manufacturer), manufacturer, null)
        };
    }

    public static string GetFullNameFromType(this Manufacturers manufacturer)
    {
        return manufacturer switch
        {
            Manufacturers.A => "American Home Food Products",
            Manufacturers.G => "General Mills",
            Manufacturers.K => "Kelloggs",
            Manufacturers.N => "Nabisco",
            Manufacturers.P => "Post",
            Manufacturers.Q => "Quaker Oats",
            Manufacturers.R => "Ralston Purina",
            _ => throw new ArgumentOutOfRangeException(nameof(manufacturer), manufacturer, null)
        };
    }

    public static string GetFullNameFromString(this string manufacturer)
    {
        return manufacturer switch
        {
            "A" => "American Home Food Products",
            "G" => "General Mills",
            "K" => "Kelloggs",
            "N" => "Nabisco",
            "P" => "Post",
            "Q" => "Quaker Oats",
            "R" => "Ralston Purina",
            _ => throw new ArgumentOutOfRangeException(nameof(manufacturer), manufacturer, null)
        };
    }
}