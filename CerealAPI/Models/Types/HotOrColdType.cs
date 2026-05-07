namespace Cereal_API.Models.Types;

public enum HotOrColdType
{
    H, // Hot
    C  // Cold
}

public static class HotOrColdTypeExtensions
{
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
