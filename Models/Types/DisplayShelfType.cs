namespace Cereal_API.Models.Types;

public enum DisplayShelfType
{
    One = 1,
    Two = 2,
    Three = 3
}

public static class DisplayShelfTypeExtensions
{
    public static int ToInt(this DisplayShelfType displayShelfType)
    {
        return (int)displayShelfType;
    }

    public static DisplayShelfType FromInt(int value)
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
