// cSpell:disable

using StarLightLexer;

namespace StarLightParser;

#pragma warning disable RCS1194
public class UnexpectedLexemeException : Exception
{
    public UnexpectedLexemeException(TokenType expected, Token actual)
        : base($"Неожиданная лексема {actual} вместо ожидаемой {expected}")
    {
    }
}
#pragma warning restore RCS1194