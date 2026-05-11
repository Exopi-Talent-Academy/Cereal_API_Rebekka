namespace Cereal_API.Models.Types;

/// <summary>
/// An enum representing the type of cereal, either hot or cold. The original dataset uses "H" for hot cereals and "C" for cold cereals.
/// </summary>
public enum HotOrColdType
{
    H, // Hot
    C  // Cold
}

public static class HotOrColdTypeExtensions
{
    /// <summary>
    /// Converts the specified string to its corresponding HotOrColdType enumeration value.
    /// </summary>
    /// <param name="type">The string representation of the hot or cold type. Accepts "H", "C", "Hot", or "Cold".</param>
    /// <returns>The HotOrColdType value that corresponds to the specified string.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified value does not correspond to a valid HotOrColdType.</exception>
    public static HotOrColdType FromString(this string type)
    {
        return type.ToUpper().Trim() switch
        {
            "H" => HotOrColdType.H,
            "C" => HotOrColdType.C,
            "HOT" => HotOrColdType.H,
            "COLD" => HotOrColdType.C,
            _ => throw new ArgumentException($"Invalid HotOrColdType value: {type}")
        };
    }

    /// <summary>
    /// Converts the specified HotOrColdType enumeration value to its corresponding full-word string representation. "H" for hot cereals and "C" for cold cereals.
    /// </summary>
    /// <param name="type">The HotOrColdType value to convert.</param>
    /// <returns>The full-word string representation of the specified HotOrColdType value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified value does not correspond to a valid HotOrColdType.</exception>
    public static string ToFullString(this HotOrColdType type)
    {
        return type switch
        {
            HotOrColdType.H => "Hot",
            HotOrColdType.C => "Cold",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
