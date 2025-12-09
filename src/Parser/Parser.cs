using StarLightLexer;

namespace StarLightParser;

public class Parser
{
    private readonly TokenStream _tokens;

    public Parser(string code)
    {
        _tokens = new TokenStream(code);
    }

    public void ParseExpression()
    {
        ParseAssignmentExpression();
    }

    private void ParseAssignmentExpression()
    {
        ParseLogicalOrExpression();

        if (_tokens.Peek().Type == TokenType.ASSIGN ||
            _tokens.Peek().Type == TokenType.PLUS_ASSIGN ||
            _tokens.Peek().Type == TokenType.MINUS_ASSIGN ||
            _tokens.Peek().Type == TokenType.MULTIPLY_ASSIGN ||
            _tokens.Peek().Type == TokenType.DIVIDE_ASSIGN ||
            _tokens.Peek().Type == TokenType.EXPONENTIATION_ASSIGN)
        {
            _tokens.Advance();
            ParseAssignmentExpression();
        }
    }

    private void ParseLogicalOrExpression()
    {
        ParseLogicalAndExpression();

        while (_tokens.Peek().Type == TokenType.OR)
        {
            _tokens.Advance();
            ParseLogicalAndExpression();
        }
    }

    private void ParseLogicalAndExpression()
    {
        ParseEqualityExpression();

        while (_tokens.Peek().Type == TokenType.AND)
        {
            _tokens.Advance();
            ParseEqualityExpression();
        }
    }

    private void ParseEqualityExpression()
    {
        ParseComparisonExpression();

        while (_tokens.Peek().Type == TokenType.EQUALS ||
               _tokens.Peek().Type == TokenType.NOT_EQUALS)
        {
            _tokens.Advance();
            ParseComparisonExpression();
        }
    }

    private void ParseComparisonExpression()
    {
        ParseAdditiveExpression();

        while (_tokens.Peek().Type == TokenType.GREATER_THAN ||
               _tokens.Peek().Type == TokenType.GREATER_OR_EQUAL ||
               _tokens.Peek().Type == TokenType.LESS_THAN ||
               _tokens.Peek().Type == TokenType.LESS_OR_EQUAL)
        {
            _tokens.Advance();
            ParseAdditiveExpression();
        }
    }

    private void ParseAdditiveExpression()
    {
        ParseMultiplicativeExpression();

        while (_tokens.Peek().Type == TokenType.PLUS ||
               _tokens.Peek().Type == TokenType.MINUS)
        {
            _tokens.Advance();
            ParseMultiplicativeExpression();
        }
    }

    private void ParseMultiplicativeExpression()
    {
        ParsePowerExpression();

        while (_tokens.Peek().Type == TokenType.MULTIPLY ||
               _tokens.Peek().Type == TokenType.DIVIDE ||
               _tokens.Peek().Type == TokenType.MODULO)
        {
            _tokens.Advance();
            ParsePowerExpression();
        }
    }

    private void ParsePowerExpression()
    {
        ParseUnaryExpression();

        if (_tokens.Peek().Type == TokenType.EXPONENTIATION)
        {
            _tokens.Advance();
            ParsePowerExpression();
        }
    }

    private void ParseUnaryExpression()
    {
        if (_tokens.Peek().Type == TokenType.PLUS ||
            _tokens.Peek().Type == TokenType.MINUS ||
            _tokens.Peek().Type == TokenType.NOT)
        {
            _tokens.Advance();
        }

        ParsePrimaryExpression();
    }

    private void ParsePrimaryExpression()
    {
        Token token = _tokens.Peek();

        switch (token.Type)
        {
            case TokenType.NUMERIC_LITERAL:
            case TokenType.ISTINA:
            case TokenType.LOZH:
                _tokens.Advance();
                break;
            case TokenType.IDENTIFIER:
                ParseIdentifierOrFunctionCall();
                break;
            case TokenType.OPEN_PARENTHESIS:
                _tokens.Advance();
                ParseExpression();
                Match(TokenType.CLOSE_PARENTHESIS);
                break;
            case TokenType.IZLUCHAT:
            case TokenType.PRIEM_SIGNALA:
                ParseFunctionCallExpression();
                break;
            default:
                throw new UnexpectedLexemeException(TokenType.NUMERIC_LITERAL, token);
        }
    }

    private void ParseIdentifierOrFunctionCall()
    {
        _tokens.Advance(); // consume identifier

        // Проверяем, является ли это вызовом функции
        if (_tokens.Peek().Type == TokenType.OPEN_PARENTHESIS)
        {
            ParseFunctionCallArguments();
        }
    }

    private void ParseFunctionCallExpression()
    {
        _tokens.Advance(); // consume function name (ИЗЛУЧАТЬ или ПРИЕМ_СИГНАЛА)
        Match(TokenType.OPEN_PARENTHESIS);
        ParseFunctionCallArguments();
    }

    private void ParseFunctionCallArguments()
    {
        // Если есть аргументы - парсим их
        if (_tokens.Peek().Type != TokenType.CLOSE_PARENTHESIS)
        {
            ParseExpression();

            // Парсим дополнительные аргументы через запятую
            while (_tokens.Peek().Type == TokenType.COMMA)
            {
                _tokens.Advance();
                ParseExpression();
            }
        }

        Match(TokenType.CLOSE_PARENTHESIS);
    }

    private void Match(TokenType expected)
    {
        Token token = _tokens.Peek();
        if (token.Type != expected)
        {
            throw new UnexpectedLexemeException(expected, token);
        }

        _tokens.Advance();
    }
}