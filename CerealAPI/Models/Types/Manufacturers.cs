namespace Cereal_API.Models.Types;

/// <summary>
/// Specifies the supported manufacturers.
/// </summary>
/// <remarks>Use this enumeration to represent or filter entities by manufacturer.</remarks>
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
    /// <summary>
    /// Converts the specified manufacturer code string to its corresponding Manufacturers enumeration value.
    /// </summary>
    /// <remarks>Valid manufacturer codes are "A", "G", "K", "N", "P", "Q", and "R". The method performs a
    /// case-insensitive comparison after trimming whitespace.</remarks>
    /// <param name="manufacturer">The manufacturer code to convert. Leading and trailing whitespace is ignored. The comparison is
    /// case-insensitive.</param>
    /// <returns>The Manufacturers enumeration value that corresponds to the specified manufacturer code.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified manufacturer code does not match a supported manufacturer.</exception>
    public static Manufacturers GetManufacturers(this string manufacturer)
    {
        return manufacturer.ToUpper().Trim() switch
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

    /// <summary>
    /// Returns the full manufacturer name corresponding to the specified <see cref="Manufacturers"/> enumeration value.
    /// </summary>
    /// <param name="manufacturer">The manufacturer enumeration value for which to retrieve the full name.</param>
    /// <returns>A string containing the full name of the manufacturer associated with the specified enumeration value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="manufacturer"/> is not a defined value of the <see cref="Manufacturers"/> enumeration.</exception>
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

    /// <summary>
    /// Returns the full manufacturer name corresponding to the specified manufacturer letter.
    /// </summary>
    /// <param name="manufacturer">The manufacturer code to convert to a full name. Must be one of "A", "G", "K", "N", "P", "Q", or "R".</param>
    /// <returns>The full name of the manufacturer that corresponds to the specified code.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the manufacturer code does not match a known value.</exception>
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