namespace Cereal_API.Models.Types;

/// <summary>
/// An enum representing the display shelf location of a cereal. The original dataset uses integers to represent this, with 1 being the bottom shelf and 3 being the top shelf.
/// </summary>
public enum DisplayShelfType
{
    One = 1,
    Two = 2,
    Three = 3
}

public static class DisplayShelfTypeExtensions
{
    /// <summary>
    /// Converts the specified DisplayShelfType value to its underlying integer representation.
    /// </summary>
    /// <param name="displayShelfType">The DisplayShelfType value to convert to an integer.</param>
    /// <returns>The integer value corresponding to the specified DisplayShelfType.</returns>
    public static int ToInt(this DisplayShelfType displayShelfType)
    {
        return (int)displayShelfType;
    }

    /// <summary>
    /// Converts the specified string to its corresponding DisplayShelfType enumeration value.
    /// </summary>
    /// <remarks>The comparison is case-insensitive and ignores leading and trailing whitespace.</remarks>
    /// <param name="value">The string representation of the display shelf type. Accepts numeric values ("1", "2", "3") or their
    /// case-insensitive word equivalents ("One", "Two", "Three").</param>
    /// <returns>The DisplayShelfType value that corresponds to the specified string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified value does not correspond to a valid DisplayShelfType.</exception>
    public static DisplayShelfType FromString(this string value)
    {
        return value.ToUpper().Trim() switch
        {
            "1" => DisplayShelfType.One,
            "2" => DisplayShelfType.Two,
            "3" => DisplayShelfType.Three,
            "ONE" => DisplayShelfType.One,
            "TWO" => DisplayShelfType.Two,
            "THREE" => DisplayShelfType.Three,
            _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} is not a valid DisplayShelfType")
        };
    }

    /// <summary>
    /// Converts an integer value to its corresponding DisplayShelfType enumeration value.
    /// </summary>
    /// <param name="value">The integer value to convert. Must be 1, 2, or 3.</param>
    /// <returns>The DisplayShelfType value that corresponds to the specified integer.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when value does not correspond to a valid DisplayShelfType.</exception>
    public static DisplayShelfType FromInt(this int value)
    {
        return value switch
        {
            1 => DisplayShelfType.One,
            2 => DisplayShelfType.Two,
            3 => DisplayShelfType.Three,
            _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} is not a valid DisplayShelfType")
        };
    }
}
