namespace StarLightLexer;

public class Token(TokenType type, TokenValue? value = null)
{
    public TokenType Type { get; } = type;

    public TokenValue? Value { get; } = value;

    public override bool Equals(object? obj)
    {
        if (obj is Token other)
        {
            if (Type != other.Type)
            {
                return false;
            }

            if (Value == null && other.Value == null)
            {
                return true;
            }

            if (Value == null || other.Value == null)
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }
}