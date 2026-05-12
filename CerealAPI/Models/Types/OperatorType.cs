namespace Cereal_API.Models.Types;

public enum OperatorType
{
    Equals,
    NotEquals,
    GreaterThan,
    SmallerThan,
    GreaterThanOrEqual,
    SmallerThanOrEqual
}

public static class OperatorTypeExtensions
{
    public static string GetSymbol(this OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Equals => "==",
            OperatorType.NotEquals => "!=",
            OperatorType.GreaterThan => ">",
            OperatorType.SmallerThan => "<",
            OperatorType.GreaterThanOrEqual => ">=",
            OperatorType.SmallerThanOrEqual => "<=",
            _ => throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null)
        };
    }
}
