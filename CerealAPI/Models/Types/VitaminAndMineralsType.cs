namespace Cereal_API.Models.Types;

/// <summary>
/// An enum representing the percentage of vitamins and minerals in a cereal. The original dataset uses integers to represent this.
/// </summary>
public enum VitaminAndMineralsType
{
    Zero = 0,
    TwentyFive = 25,
    OneHundred = 100
}

public static class VitaminAndMineralsTypeExtensions
{
    /// <summary>
    /// Converts the specified VitaminAndMineralsType enumeration value to its underlying integer value.
    /// </summary>
    /// <param name="vitaminAndMineralsType">The VitaminAndMineralsType enumeration value to convert.</param>
    /// <returns>The integer value corresponding to the specified VitaminAndMineralsType enumeration member.</returns>
    public static int ToInt(this VitaminAndMineralsType vitaminAndMineralsType)
    {
        return (int)vitaminAndMineralsType;
    }

    /// <summary>
    /// Converts the specified string to its corresponding VitaminAndMineralsType enumeration value.
    /// </summary>
    /// <remarks>Whitespace is trimmed and the comparison is case-insensitive. Common synonyms and numeric
    /// values are supported.</remarks>
    /// <param name="value">The string representation of the vitamin and minerals type to convert. The value is case-insensitive and may
    /// include numeric or textual representations such as "0", "25", "100", "Zero", "TwentyFive", or "OneHundred".</param>
    /// <returns>The VitaminAndMineralsType value that corresponds to the specified string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified value does not correspond to a valid VitaminAndMineralsType.</exception>
    public static VitaminAndMineralsType FromString(this string value)
    {
        return value.ToUpper().Trim() switch
        {
            "0" => VitaminAndMineralsType.Zero,
            "25" => VitaminAndMineralsType.TwentyFive,
            "100" => VitaminAndMineralsType.OneHundred,
            "ZERO" => VitaminAndMineralsType.Zero,
            "TWENTYFIVE" => VitaminAndMineralsType.TwentyFive,
            "ONEHUNDRED" => VitaminAndMineralsType.OneHundred,
            "TWENTY FIVE" => VitaminAndMineralsType.TwentyFive,
            "ONE HUNDRED" => VitaminAndMineralsType.OneHundred,
            "HUNDRED" => VitaminAndMineralsType.OneHundred,
            _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} is not a valid VitaminAndMineralsType")
        };
    }

    /// <summary>
    /// Converts an integer value to its corresponding VitaminAndMineralsType enumeration value.
    /// </summary>
    /// <param name="value">The integer value to convert. Must be 0, 25, or 100.</param>
    /// <returns>The VitaminAndMineralsType value that corresponds to the specified integer.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified value does not correspond to a valid VitaminAndMineralsType.</exception>
    public static VitaminAndMineralsType FromInt(this int value)
    {
        return value switch
        {
            0 => VitaminAndMineralsType.Zero,
            25 => VitaminAndMineralsType.TwentyFive,
            100 => VitaminAndMineralsType.OneHundred,
            _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} is not a valid VitaminAndMineralsType")
        };
    }
}