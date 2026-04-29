namespace Cereal_API.Models.Types;

public enum VitaminAndMineralsType
{
    Zero = 0,
    TwentyFive = 25,
    OneHundred = 100
}

public static class VitaminAndMineralsTypeExtensions
{
    public static int ToInt(this VitaminAndMineralsType vitaminAndMineralsType)
    {
        return (int)vitaminAndMineralsType;
    }

    public static VitaminAndMineralsType FromInt(int value)
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