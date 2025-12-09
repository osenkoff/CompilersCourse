using System.Globalization;

namespace StarLightLexer;

public class TokenValue
{
    private readonly object _value;

    public TokenValue(string value)
    {
        _value = value;
    }

    public TokenValue(decimal value)
    {
        _value = value;
    }

    public TokenValue(bool value)
    {
        _value = value;
    }

    public override string ToString()
    {
        return _value switch
        {
            string str => str,
            decimal num => num.ToString(CultureInfo.InvariantCulture),
            bool boolean => boolean ? "истина" : "ложь",
            _ => _value.ToString() ?? ""
        };
    }

    public decimal ToDecimal()
    {
        return (decimal)_value;
    }

    public bool ToBoolean()
    {
        return (bool)_value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is TokenValue other)
        {
            return _value.Equals(other._value);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}