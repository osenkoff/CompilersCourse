using System.Globalization;

namespace StarLightLexer;

public class Lexer
{
        private static readonly Dictionary<string, TokenType> Keywords = new(StringComparer.OrdinalIgnoreCase)
    {
        { "ЗВЕЗДА", TokenType.ZVEZDA },
        { "ЗАКРЫТАЯ_ЗВЕЗДА", TokenType.ZAKRYTAYA_ZVEZDA },
        { "СВЕТ", TokenType.SVET },
        { "КОНСТЕЛЛАЦИЯ", TokenType.KONSTELLATSIYA },
        { "ИЗЛУЧАТЬ", TokenType.IZLUCHAT },
        { "ПРИЕМ_СИГНАЛА", TokenType.PRIEM_SIGNALA },
        { "ФОТОН", TokenType.FOTON },
        { "ВЕРНУТЬ", TokenType.VERNUT },
        { "ЕСЛИ", TokenType.ESLI },
        { "ИЛИ_НЕТ", TokenType.ILI_NET },
        { "ОРБИТА", TokenType.ORBITA },
        { "СПЕКТР", TokenType.SPEKTR },
        { "КВАЗАР", TokenType.KVAZAR },
        { "НОВА", TokenType.NOVA },
        { "ВАКУУМ", TokenType.VAKUUM },
        { "ГАЛАКТИКА", TokenType.GALAKTIKA },
        { "ИСТИНА", TokenType.ISTINA },
        { "ЛОЖЬ", TokenType.LOZH },
    };

        private readonly TextScanner _scanner;

        public Lexer(string code)
        {
                _scanner = new TextScanner(code);
        }

        public Token ParseToken()
        {
                SkipWhiteSpacesAndComments();

                if (_scanner.IsEnd())
                {
                        return new Token(TokenType.END_OF_FILE);
                }

                char c = _scanner.Peek();

                if (char.IsLetter(c) || c == '_')
                {
                        return ParseIdentifierOrKeyword();
                }

                if (char.IsDigit(c) || (c == '-' && char.IsDigit(_scanner.Peek(1))) || c == '.')
                {
                        return ParseNumericLiteral();
                }

                return ParseOperatorOrPunctuation();
        }

        private Token ParseIdentifierOrKeyword()
        {
                string value = _scanner.Peek().ToString();
                _scanner.Advance();

                while (!_scanner.IsEnd())
                {
                        char c = _scanner.Peek();
                        if (char.IsLetterOrDigit(c) || c == '_')
                        {
                                value += c;
                                _scanner.Advance();
                        }
                        else
                        {
                                break;
                        }
                }

                if (Keywords.TryGetValue(value, out TokenType type))
                {
                        return new Token(type);
                }

                return new Token(TokenType.IDENTIFIER, new TokenValue(value));
        }

        private Token ParseNumericLiteral()
        {
                string numberStr = "";

                // Обработка знака
                if (_scanner.Peek() == '-')
                {
                        numberStr += _scanner.Peek();
                        _scanner.Advance();
                }

                // Целая часть
                while (!_scanner.IsEnd() && char.IsDigit(_scanner.Peek()))
                {
                        numberStr += _scanner.Peek();
                        _scanner.Advance();
                }

                // Дробная часть
                if (!_scanner.IsEnd() && _scanner.Peek() == '.')
                {
                        numberStr += _scanner.Peek();
                        _scanner.Advance();

                        while (!_scanner.IsEnd() && char.IsDigit(_scanner.Peek()))
                        {
                                numberStr += _scanner.Peek();
                                _scanner.Advance();
                        }
                }

                if (!_scanner.IsEnd() && (_scanner.Peek() == 'e' || _scanner.Peek() == 'E'))
                {
                        numberStr += _scanner.Peek();
                        _scanner.Advance();

                        if (!_scanner.IsEnd() && (_scanner.Peek() == '+' || _scanner.Peek() == '-'))
                        {
                                numberStr += _scanner.Peek();
                                _scanner.Advance();
                        }

                        while (!_scanner.IsEnd() && char.IsDigit(_scanner.Peek()))
                        {
                                numberStr += _scanner.Peek();
                                _scanner.Advance();
                        }
                }

                if (decimal.TryParse(numberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                {
                        return new Token(TokenType.NUMERIC_LITERAL, new TokenValue(result));
                }

                return new Token(TokenType.ERROR, new TokenValue(numberStr));
        }

        private Token ParseOperatorOrPunctuation()
        {
                char current = _scanner.Peek();
                _scanner.Advance();

                switch (current)
                {
                        case ';': return new Token(TokenType.SEMICOLON);
                        case ',': return new Token(TokenType.COMMA);
                        case ':': return new Token(TokenType.COLON);
                        case '(': return new Token(TokenType.OPEN_PARENTHESIS);
                        case ')': return new Token(TokenType.CLOSE_PARENTHESIS);
                        case '[': return new Token(TokenType.OPEN_BRACKET);
                        case ']': return new Token(TokenType.CLOSE_BRACKET);
                        case '{': return new Token(TokenType.OPEN_BRACE);
                        case '}': return new Token(TokenType.CLOSE_BRACE);
                        case '+':
                                if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.PLUS_ASSIGN);
                                }

                                return new Token(TokenType.PLUS);
                        case '-':
                                if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.MINUS_ASSIGN);
                                }

                                return new Token(TokenType.MINUS);
                        case '*':
                                if (_scanner.Peek() == '*')
                                {
                                        _scanner.Advance();
                                        if (_scanner.Peek() == '=')
                                        {
                                                _scanner.Advance();
                                                return new Token(TokenType.EXPONENTIATION_ASSIGN);
                                        }

                                        return new Token(TokenType.EXPONENTIATION);
                                }
                                else if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.MULTIPLY_ASSIGN);
                                }

                                return new Token(TokenType.MULTIPLY);
                        case '/':
                                if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.DIVIDE_ASSIGN);
                                }

                                return new Token(TokenType.DIVIDE);
                        case '%': return new Token(TokenType.MODULO);
                        case '=':
                                if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.EQUALS);
                                }

                                return new Token(TokenType.ASSIGN);
                        case '!':
                                if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.NOT_EQUALS);
                                }

                                return new Token(TokenType.NOT);
                        case '>':
                                if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.GREATER_OR_EQUAL);
                                }

                                return new Token(TokenType.GREATER_THAN);
                        case '<':
                                if (_scanner.Peek() == '=')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.LESS_OR_EQUAL);
                                }

                                return new Token(TokenType.LESS_THAN);
                        case '&':
                                if (_scanner.Peek() == '&')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.AND);
                                }

                                break;
                        case '|':
                                if (_scanner.Peek() == '|')
                                {
                                        _scanner.Advance();
                                        return new Token(TokenType.OR);
                                }

                                break;
                }

                return new Token(TokenType.ERROR, new TokenValue(current.ToString()));
        }

        private void SkipWhiteSpacesAndComments()
        {
                do
                {
                        SkipWhiteSpaces();
                }
                while (TryParseSingleLineComment() || TryParseMultilineComment());
        }

        private void SkipWhiteSpaces()
        {
                while (!_scanner.IsEnd() && char.IsWhiteSpace(_scanner.Peek()))
                {
                        _scanner.Advance();
                }
        }

        private bool TryParseSingleLineComment()
        {
                if (_scanner.Peek() == '/' && _scanner.Peek(1) == '/')
                {
                        _scanner.Advance(2);
                        while (!_scanner.IsEnd() && _scanner.Peek() != '\n' && _scanner.Peek() != '\r')
                        {
                                _scanner.Advance();
                        }

                        return true;
                }

                return false;
        }

        private bool TryParseMultilineComment()
        {
                if (_scanner.Peek() == '/' && _scanner.Peek(1) == '*')
                {
                        _scanner.Advance(2);
                        while (!_scanner.IsEnd())
                        {
                                if (_scanner.Peek() == '*' && _scanner.Peek(1) == '/')
                                {
                                        _scanner.Advance(2);
                                        return true;
                                }

                                _scanner.Advance();
                        }

                        return true;
                }

                return false;
        }
}