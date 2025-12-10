using StarLightLexer;

namespace StarLightParser;

public class Parser
{
    private readonly TokenStream _tokens;

    public Parser(string code)
    {
        _tokens = new TokenStream(code);
    }

    public void ParseProgram()
    {
        Match(TokenType.ZVEZDA);

        while (_tokens.Peek().Type != TokenType.ZAKRYTAYA_ZVEZDA &&
               _tokens.Peek().Type != TokenType.END_OF_FILE)
        {
            ParseStatement();
        }

        Match(TokenType.ZAKRYTAYA_ZVEZDA);
    }

    public void ParseExpression()
    {
        ParseAssignmentExpression();
    }

    private void ParseStatement()
    {
        Token token = _tokens.Peek();

        switch (token.Type)
        {
            case TokenType.SVET:
                ParseVariableDeclaration();
                break;
            case TokenType.KONSTELLATSIYA:
                ParseConstantDeclaration();
                break;
            case TokenType.IZLUCHAT:
                ParsePrintStatement();
                break;
            case TokenType.PRIEM_SIGNALA:
                ParseInputStatement();
                break;
            case TokenType.IDENTIFIER:
                // Может быть присваивание или вызов функции
                ParseIdentifierStatement();
                break;
            default:
                throw new UnexpectedLexemeException(TokenType.SVET, token);
        }
    }

    private void ParseVariableDeclaration()
    {
        Match(TokenType.SVET);
        Match(TokenType.IDENTIFIER);
        Match(TokenType.COLON);
        ParseType();

        if (_tokens.Peek().Type == TokenType.ASSIGN)
        {
            _tokens.Advance();
            ParseExpression();
        }

        Match(TokenType.SEMICOLON);
    }

    private void ParseConstantDeclaration()
    {
        Match(TokenType.KONSTELLATSIYA);
        Match(TokenType.IDENTIFIER);
        Match(TokenType.COLON);
        ParseType();
        Match(TokenType.ASSIGN);
        ParseExpression();
        Match(TokenType.SEMICOLON);
    }

    private void ParseType()
    {
        Token token = _tokens.Peek();
        if (token.Type == TokenType.KVAZAR ||
            token.Type == TokenType.NOVA ||
            token.Type == TokenType.VAKUUM ||
            token.Type == TokenType.GALAKTIKA)
        {
            _tokens.Advance();
        }
        else
        {
            throw new UnexpectedLexemeException(TokenType.KVAZAR, token);
        }
    }

    private void ParsePrintStatement()
    {
        Match(TokenType.IZLUCHAT);
        Match(TokenType.OPEN_PARENTHESIS);

        if (_tokens.Peek().Type != TokenType.CLOSE_PARENTHESIS)
        {
            ParseExpression();

            while (_tokens.Peek().Type == TokenType.COMMA)
            {
                _tokens.Advance();
                ParseExpression();
            }
        }

        Match(TokenType.CLOSE_PARENTHESIS);
        Match(TokenType.SEMICOLON);
    }

    private void ParseInputStatement()
    {
        Match(TokenType.PRIEM_SIGNALA);
        Match(TokenType.OPEN_PARENTHESIS);
        Match(TokenType.IDENTIFIER);
        Match(TokenType.CLOSE_PARENTHESIS);
        Match(TokenType.SEMICOLON);
    }

    private void ParseIdentifierStatement()
    {
        Match(TokenType.IDENTIFIER);

        // Проверяем, является ли это присваиванием или вызовом функции
        if (_tokens.Peek().Type == TokenType.ASSIGN ||
            _tokens.Peek().Type == TokenType.PLUS_ASSIGN ||
            _tokens.Peek().Type == TokenType.MINUS_ASSIGN ||
            _tokens.Peek().Type == TokenType.MULTIPLY_ASSIGN ||
            _tokens.Peek().Type == TokenType.DIVIDE_ASSIGN ||
            _tokens.Peek().Type == TokenType.EXPONENTIATION_ASSIGN)
        {
            // Это присваивание
            _tokens.Advance();
            ParseExpression();
            Match(TokenType.SEMICOLON);
        }
        else if (_tokens.Peek().Type == TokenType.OPEN_PARENTHESIS)
        {
            // Это вызов функции
            ParseFunctionCallArguments();
            Match(TokenType.SEMICOLON);
        }
        else
        {
            throw new UnexpectedLexemeException(TokenType.ASSIGN, _tokens.Peek());
        }
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