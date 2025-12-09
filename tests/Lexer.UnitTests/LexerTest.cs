using StarLightLexer;

namespace StarLightLexer.UnitTests;

public class LexerTests
{
    [Theory]
    [MemberData(nameof(GetTokenizeData))]
    public void Can_tokenize_StarLight_code(string code, List<Token> expected)
    {
        // Act
        List<Token> actual = Tokenize(code);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<string, List<Token>> GetTokenizeData()
    {
        return new TheoryData<string, List<Token>>
        {
            {
                "ЗВЕЗДА",
                [new Token(TokenType.ZVEZDA)]
            },
            {
                "звезда",
                [new Token(TokenType.ZVEZDA)]
            },
            {
                "ЗАКРЫТАЯ_ЗВЕЗДА",
                [new Token(TokenType.ZAKRYTAYA_ZVEZDA)]
            },
            {
                "СВЕТ",
                [new Token(TokenType.SVET)]
            },
            {
                "КОНСТЕЛЛАЦИЯ",
                [new Token(TokenType.KONSTELLATSIYA)]
            },
            {
                "СВЕТ x : квазар = 1;",
                [
                    new Token(TokenType.SVET),
                    new Token(TokenType.IDENTIFIER, new TokenValue("x")),
                    new Token(TokenType.COLON),
                    new Token(TokenType.KVAZAR),
                    new Token(TokenType.ASSIGN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(1)),
                    new Token(TokenType.SEMICOLON)
                ]
            },
            {
                "КОНСТЕЛЛАЦИЯ PI : нова = 3.1416;",
                [
                    new Token(TokenType.KONSTELLATSIYA),
                    new Token(TokenType.IDENTIFIER, new TokenValue("PI")),
                    new Token(TokenType.COLON),
                    new Token(TokenType.NOVA),
                    new Token(TokenType.ASSIGN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(3.1416m)),
                    new Token(TokenType.SEMICOLON)
                ]
            },
            {
                "СВЕТ имя = ПРИЕМ_СИГНАЛА();",
                [
                    new Token(TokenType.SVET),
                    new Token(TokenType.IDENTIFIER, new TokenValue("имя")),
                    new Token(TokenType.ASSIGN),
                    new Token(TokenType.PRIEM_SIGNALA),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.SEMICOLON)
                ]
            },
            {
                "ФОТОН Сумма(a: квазар, b: квазар): квазар { ВЕРНУТЬ a + b; }",
                [
                    new Token(TokenType.FOTON),
                    new Token(TokenType.IDENTIFIER, new TokenValue("Сумма")),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.IDENTIFIER, new TokenValue("a")),
                    new Token(TokenType.COLON),
                    new Token(TokenType.KVAZAR),
                    new Token(TokenType.COMMA),
                    new Token(TokenType.IDENTIFIER, new TokenValue("b")),
                    new Token(TokenType.COLON),
                    new Token(TokenType.KVAZAR),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.COLON),
                    new Token(TokenType.KVAZAR),
                    new Token(TokenType.OPEN_BRACE),
                    new Token(TokenType.VERNUT),
                    new Token(TokenType.IDENTIFIER, new TokenValue("a")),
                    new Token(TokenType.PLUS),
                    new Token(TokenType.IDENTIFIER, new TokenValue("b")),
                    new Token(TokenType.SEMICOLON),
                    new Token(TokenType.CLOSE_BRACE)
                ]
            },
            {
                "ЕСЛИ (x > 0) { ИЗЛУЧАТЬ(x); }",
                [
                    new Token(TokenType.ESLI),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.IDENTIFIER, new TokenValue("x")),
                    new Token(TokenType.GREATER_THAN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(0)),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.OPEN_BRACE),
                    new Token(TokenType.IZLUCHAT),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.IDENTIFIER, new TokenValue("x")),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.SEMICOLON),
                    new Token(TokenType.CLOSE_BRACE)
                ]
            },
            {
                "ОРБИТА (x < 10) { x += 1; }",
                [
                    new Token(TokenType.ORBITA),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.IDENTIFIER, new TokenValue("x")),
                    new Token(TokenType.LESS_THAN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(10)),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.OPEN_BRACE),
                    new Token(TokenType.IDENTIFIER, new TokenValue("x")),
                    new Token(TokenType.PLUS_ASSIGN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(1)),
                    new Token(TokenType.SEMICOLON),
                    new Token(TokenType.CLOSE_BRACE)
                ]
            },
            {
                "2 + 2",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.PLUS),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2))
                ]
            },
            {
                "2 - 2",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.MINUS),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2))
                ]
            },
            {
                "2 * 2",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.MULTIPLY),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2))
                ]
            },
            {
                "2 / 2",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.DIVIDE),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2))
                ]
            },
            {
                "2 % 2",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.MODULO),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2))
                ]
            },
            {
                "2 ** 4",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.EXPONENTIATION),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(4))
                ]
            },
            {
                "2 == 2",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.EQUALS),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2))
                ]
            },
            {
                "2 != 4",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.NOT_EQUALS),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(4))
                ]
            },
            {
                "3 > 2",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(3)),
                    new Token(TokenType.GREATER_THAN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2))
                ]
            },
            {
                "2 < 3",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(2)),
                    new Token(TokenType.LESS_THAN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(3))
                ]
            },
            {
                "a && b",
                [
                    new Token(TokenType.IDENTIFIER, new TokenValue("a")),
                    new Token(TokenType.AND),
                    new Token(TokenType.IDENTIFIER, new TokenValue("b"))
                ]
            },
            {
                "x || y",
                [
                    new Token(TokenType.IDENTIFIER, new TokenValue("x")),
                    new Token(TokenType.OR),
                    new Token(TokenType.IDENTIFIER, new TokenValue("y"))
                ]
            },
            {
                "!flag",
                [
                    new Token(TokenType.NOT),
                    new Token(TokenType.IDENTIFIER, new TokenValue("flag"))
                ]
            },
            {
                "42 -15 3.1416 1.2e5 -2.0E+3",
                [
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(42)),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(-15)),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(3.1416m)),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(120000m)),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(-2000m))
                ]
            },
            {
                """
                // Это однострочный комментарий
                СВЕТ x = 42;
                """,
                [
                    new Token(TokenType.SVET),
                    new Token(TokenType.IDENTIFIER, new TokenValue("x")),
                    new Token(TokenType.ASSIGN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(42)),
                    new Token(TokenType.SEMICOLON)
                ]
            },
            {
                """
                /* 
                 Многострочный 
                 комментарий 
                */
                СВЕТ y = 100;
                """,
                [
                    new Token(TokenType.SVET),
                    new Token(TokenType.IDENTIFIER, new TokenValue("y")),
                    new Token(TokenType.ASSIGN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(100)),
                    new Token(TokenType.SEMICOLON)
                ]
            },
            {
                """
                ЗВЕЗДА
                    СВЕТ импульс : квазар = 42;
                    СВЕТ флаг : вакуум = истина;
                    
                    ЕСЛИ (импульс > 20) {
                        ИЗЛУЧАТЬ(импульс);
                    } ИЛИ_НЕТ {
                        ИЗЛУЧАТЬ(0);
                    }
                ЗАКРЫТАЯ_ЗВЕЗДА
                """,
                [
                    new Token(TokenType.ZVEZDA),
                    new Token(TokenType.SVET),
                    new Token(TokenType.IDENTIFIER, new TokenValue("импульс")),
                    new Token(TokenType.COLON),
                    new Token(TokenType.KVAZAR),
                    new Token(TokenType.ASSIGN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(42)),
                    new Token(TokenType.SEMICOLON),
                    new Token(TokenType.SVET),
                    new Token(TokenType.IDENTIFIER, new TokenValue("флаг")),
                    new Token(TokenType.COLON),
                    new Token(TokenType.VAKUUM),
                    new Token(TokenType.ASSIGN),
                    new Token(TokenType.ISTINA),
                    new Token(TokenType.SEMICOLON),
                    new Token(TokenType.ESLI),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.IDENTIFIER, new TokenValue("импульс")),
                    new Token(TokenType.GREATER_THAN),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(20)),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.OPEN_BRACE),
                    new Token(TokenType.IZLUCHAT),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.IDENTIFIER, new TokenValue("импульс")),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.SEMICOLON),
                    new Token(TokenType.CLOSE_BRACE),
                    new Token(TokenType.ILI_NET),
                    new Token(TokenType.OPEN_BRACE),
                    new Token(TokenType.IZLUCHAT),
                    new Token(TokenType.OPEN_PARENTHESIS),
                    new Token(TokenType.NUMERIC_LITERAL, new TokenValue(0)),
                    new Token(TokenType.CLOSE_PARENTHESIS),
                    new Token(TokenType.SEMICOLON),
                    new Token(TokenType.CLOSE_BRACE),
                    new Token(TokenType.ZAKRYTAYA_ZVEZDA)
                ]
            },
        };
    }

    [Fact]
    public void Can_parse_empty_string()
    {
        // Arrange
        string code = "";

        // Act
        List<Token> tokens = Tokenize(code);

        // Assert
        Assert.Empty(tokens);
    }

    [Fact]
    public void Can_parse_only_whitespace()
    {
        // Arrange
        string code = "   \t\n  ";

        // Act
        List<Token> tokens = Tokenize(code);

        // Assert
        Assert.Empty(tokens);
    }

    [Fact]
    public void Can_parse_identifiers_with_cyrillic()
    {
        // Arrange
        string code = "переменная _тест ИмяПеременной";

        // Act
        List<Token> tokens = Tokenize(code);

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.All(tokens, t => Assert.Equal(TokenType.IDENTIFIER, t.Type));
        Assert.Equal("переменная", tokens[0].Value?.ToString());
        Assert.Equal("_тест", tokens[1].Value?.ToString());
        Assert.Equal("ИмяПеременной", tokens[2].Value?.ToString());
    }

    [Fact]
    public void Can_parse_identifiers_with_mixed_chars()
    {
        // Arrange
        string code = "var1 test_var mixed123Name";

        // Act
        List<Token> tokens = Tokenize(code);

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.All(tokens, t => Assert.Equal(TokenType.IDENTIFIER, t.Type));
    }

    [Fact]
    public void Can_parse_boolean_literals()
    {
        // Arrange
        string code = "истина ложь";

        // Act
        List<Token> tokens = Tokenize(code);

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.ISTINA, tokens[0].Type);
        Assert.Equal(TokenType.LOZH, tokens[1].Type);
    }

    private List<Token> Tokenize(string code)
    {
        List<Token> results = [];
        Lexer lexer = new(code);

        Token token;
        do
        {
            token = lexer.ParseToken();
            if (token.Type != TokenType.END_OF_FILE)
            {
                results.Add(token);
            }
        }
        while (token.Type != TokenType.END_OF_FILE);

        return results;
    }
}