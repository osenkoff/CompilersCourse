using StarLightLexer;

namespace StarLightParser;

public class TokenStream
{
    private readonly List<Token> _tokens = new();
    private int _position;

    public TokenStream(string code)
    {
        Lexer lexer = new Lexer(code);
        Token token;

        do
        {
            token = lexer.ParseToken();
            _tokens.Add(token);
        }
        while (token.Type != TokenType.END_OF_FILE);
    }

    public Token Peek()
    {
        return _position < _tokens.Count ? _tokens[_position] : new Token(TokenType.END_OF_FILE);
    }

    public void Advance()
    {
        if (_position < _tokens.Count)
        {
            _position++;
        }
    }
}