namespace Cereal_API.Models.Types;

public enum HotOrColdType
{
    H, // Hot
    C  // Cold
}

public static class HotOrColdTypeExtensions
{
    public static HotOrColdType FromString(string type)
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
}
