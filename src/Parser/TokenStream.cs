using StarLightLexer;

namespace StarLightParser;

public class TokenStream
{
    private readonly List<Token> _tokens;
    private int _position;

    public TokenStream(string code)
    {
        _tokens = new List<Token>();
        _position = 0;

        Lexer lexer = new Lexer(code);
        Token token;

        do
        {
            token = lexer.ParseToken();
            _tokens.Add(token);
        }
        while (token.Type != TokenType.END_OF_FILE);
    }

    public TokenStream(IEnumerable<Token> tokens, int position = 0)
    {
        _tokens = tokens.ToList();
        _position = position;
    }

    public IReadOnlyList<Token> Tokens => _tokens;

    public int Position => _position;

    public Token Peek()
    {
        return _position < _tokens.Count
            ? _tokens[_position]
            : new Token(TokenType.END_OF_FILE);
    }

    public void Advance()
    {
        if (_position < _tokens.Count)
        {
            _position++;
        }
    }
}