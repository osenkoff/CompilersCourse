using StarLightLexer;

namespace StarLightParser;

public class Parser(string code, Dictionary<string, decimal>? initialVars = null)
{
    private readonly string _code = code;
    private readonly List<decimal> _results = new();
    private readonly HashSet<string> _declaredVariables = new();
    private readonly HashSet<string> _constants = new();
    private TokenStream _tokens = null!;

    public Dictionary<string, decimal> Variables { get; } = initialVars ?? new Dictionary<string, decimal>();

    public IReadOnlyList<decimal> Results => _results.AsReadOnly();

    public void ParseProgram()
    {
        _tokens = new TokenStream(_code);
        _results.Clear();
        _declaredVariables.Clear();
        _constants.Clear();

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
        _tokens = new TokenStream(_code);
        ParseAssignmentExpression();
    }

    public decimal EvaluateExpression()
    {
        _tokens = new TokenStream(_code);
        decimal result = EvaluateAssignmentExpression();

        // Проверяем, что все токены были использованы
        if (_tokens.Peek().Type != TokenType.END_OF_FILE)
        {
            throw new InvalidOperationException("Not all tokens were consumed");
        }

        return result;
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
                ParseIdentifierStatement();
                break;
            default:
                throw new UnexpectedLexemeException(TokenType.SVET, token);
        }
    }

    private void ParseVariableDeclaration()
    {
        Match(TokenType.SVET);
        string name = MatchIdentifier();

        // Проверка на дублирование объявления
        if (_declaredVariables.Contains(name))
        {
            throw new InvalidOperationException($"Переменная '{name}' уже объявлена");
        }

        _declaredVariables.Add(name);

        Match(TokenType.COLON);
        ParseType();

        if (_tokens.Peek().Type == TokenType.ASSIGN)
        {
            _tokens.Advance();
            decimal value = EvaluateAssignmentExpression();
            Variables[name] = value;
        }
        else
        {
            // Переменная без инициализации получает значение 0
            Variables[name] = 0;
        }

        Match(TokenType.SEMICOLON);
    }

    private void ParseConstantDeclaration()
    {
        Match(TokenType.KONSTELLATSIYA);
        string name = MatchIdentifier();

        // Проверка на дублирование объявления
        if (_declaredVariables.Contains(name))
        {
            throw new InvalidOperationException($"Константа '{name}' уже объявлена");
        }

        _declaredVariables.Add(name);
        _constants.Add(name);

        Match(TokenType.COLON);
        ParseType();
        Match(TokenType.ASSIGN);

        decimal value = EvaluateAssignmentExpression();
        Variables[name] = value;

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

        // Парсим и вычисляем аргументы, если они есть
        if (_tokens.Peek().Type != TokenType.CLOSE_PARENTHESIS)
        {
            decimal value = EvaluateAssignmentExpression();
            _results.Add(value);

            // Парсим дополнительные аргументы через запятую
            while (_tokens.Peek().Type == TokenType.COMMA)
            {
                _tokens.Advance();
                value = EvaluateAssignmentExpression();
                _results.Add(value);
            }
        }

        Match(TokenType.CLOSE_PARENTHESIS);
        Match(TokenType.SEMICOLON);
    }

    private void ParseInputStatement()
    {
        Match(TokenType.PRIEM_SIGNALA);
        Match(TokenType.OPEN_PARENTHESIS);

        // Получаем имя переменной
        string variableName = MatchIdentifier();

        // Проверяем, что переменная объявлена
        if (!_declaredVariables.Contains(variableName))
        {
            throw new InvalidOperationException($"Переменная '{variableName}' не объявлена");
        }

        // В парсере просто считываем 0 (в реальном интерпретаторе было бы чтение из ввода)
        // Для тестов можно использовать начальные значения
        if (!Variables.ContainsKey(variableName))
        {
            Variables[variableName] = 0;
        }

        Match(TokenType.CLOSE_PARENTHESIS);
        Match(TokenType.SEMICOLON);
    }

    private void ParseIdentifierStatement()
    {
        string identifier = MatchIdentifier();

        // Проверяем, что переменная объявлена
        if (!_declaredVariables.Contains(identifier))
        {
            throw new InvalidOperationException($"Переменная '{identifier}' не объявлена");
        }

        // Проверяем, является ли это присваиванием
        if (_tokens.Peek().Type == TokenType.ASSIGN ||
            _tokens.Peek().Type == TokenType.PLUS_ASSIGN ||
            _tokens.Peek().Type == TokenType.MINUS_ASSIGN ||
            _tokens.Peek().Type == TokenType.MULTIPLY_ASSIGN ||
            _tokens.Peek().Type == TokenType.DIVIDE_ASSIGN ||
            _tokens.Peek().Type == TokenType.EXPONENTIATION_ASSIGN)
        {
            // Это присваивание
            TokenType opType = _tokens.Peek().Type;
            _tokens.Advance();

            // Проверяем, что это не константа
            if (_constants.Contains(identifier))
            {
                throw new InvalidOperationException($"Нельзя изменять константу '{identifier}'");
            }

            decimal rightValue = EvaluateAssignmentExpression();

            // Выполняем присваивание
            decimal currentValue = Variables.ContainsKey(identifier) ? Variables[identifier] : 0;
            switch (opType)
            {
                case TokenType.ASSIGN:
                    Variables[identifier] = rightValue;
                    break;
                case TokenType.PLUS_ASSIGN:
                    Variables[identifier] = currentValue + rightValue;
                    break;
                case TokenType.MINUS_ASSIGN:
                    Variables[identifier] = currentValue - rightValue;
                    break;
                case TokenType.MULTIPLY_ASSIGN:
                    Variables[identifier] = currentValue * rightValue;
                    break;
                case TokenType.DIVIDE_ASSIGN:
                    Variables[identifier] = currentValue / rightValue;
                    break;
                case TokenType.EXPONENTIATION_ASSIGN:
                    Variables[identifier] = (decimal)Math.Pow((double)currentValue, (double)rightValue);
                    break;
            }

            Match(TokenType.SEMICOLON);
        }
        else if (_tokens.Peek().Type == TokenType.OPEN_PARENTHESIS)
        {
            // Это вызов функции (но в этом задании не поддерживается)
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

        TokenType opType = _tokens.Peek().Type;
        if (opType == TokenType.ASSIGN ||
            opType == TokenType.PLUS_ASSIGN ||
            opType == TokenType.MINUS_ASSIGN ||
            opType == TokenType.MULTIPLY_ASSIGN ||
            opType == TokenType.DIVIDE_ASSIGN ||
            opType == TokenType.EXPONENTIATION_ASSIGN)
        {
            _tokens.Advance();
            ParseAssignmentExpression();
        }
    }

    private decimal EvaluateAssignmentExpression()
    {
        // Сохраняем текущую позицию токена
        int currentPos = _tokens.Position;

        // Пытаемся прочитать идентификатор и оператор
        if (_tokens.Peek().Type == TokenType.IDENTIFIER)
        {
            string varName = _tokens.Peek().Value!.ToString()!;
            TokenStream tempTokens = new TokenStream(_tokens.Tokens, _tokens.Position);
            tempTokens.Advance(); // пропускаем идентификатор

            TokenType opType = tempTokens.Peek().Type;
            if (opType == TokenType.ASSIGN ||
                opType == TokenType.PLUS_ASSIGN ||
                opType == TokenType.MINUS_ASSIGN ||
                opType == TokenType.MULTIPLY_ASSIGN ||
                opType == TokenType.DIVIDE_ASSIGN ||
                opType == TokenType.EXPONENTIATION_ASSIGN)
            {
                // Это присваивание
                _tokens.Advance(); // пропускаем идентификатор
                _tokens.Advance(); // пропускаем оператор

                decimal rightValue = EvaluateAssignmentExpression();

                switch (opType)
                {
                    case TokenType.ASSIGN:
                        Variables[varName] = rightValue;
                        break;
                    case TokenType.PLUS_ASSIGN:
                        Variables[varName] = (Variables.ContainsKey(varName) ? Variables[varName] : 0) + rightValue;
                        break;
                    case TokenType.MINUS_ASSIGN:
                        Variables[varName] = (Variables.ContainsKey(varName) ? Variables[varName] : 0) - rightValue;
                        break;
                    case TokenType.MULTIPLY_ASSIGN:
                        Variables[varName] = (Variables.ContainsKey(varName) ? Variables[varName] : 0) * rightValue;
                        break;
                    case TokenType.DIVIDE_ASSIGN:
                        Variables[varName] = (Variables.ContainsKey(varName) ? Variables[varName] : 0) / rightValue;
                        break;
                    case TokenType.EXPONENTIATION_ASSIGN:
                        Variables[varName] = (decimal)Math.Pow(
                            (double)(Variables.ContainsKey(varName) ? Variables[varName] : 0),
                            (double)rightValue);
                        break;
                }

                return Variables[varName];
            }
        }

        // Если не присваивание, парсим как логическое ИЛИ
        return EvaluateLogicalOrExpression();
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

    private decimal EvaluateLogicalOrExpression()
    {
        decimal left = EvaluateLogicalAndExpression();
        while (_tokens.Peek().Type == TokenType.OR)
        {
            _tokens.Advance();
            decimal right = EvaluateLogicalAndExpression();
            left = (left != 0 || right != 0) ? 1 : 0;
        }

        return left;
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

    private decimal EvaluateLogicalAndExpression()
    {
        decimal left = EvaluateEqualityExpression();
        while (_tokens.Peek().Type == TokenType.AND)
        {
            _tokens.Advance();
            decimal right = EvaluateEqualityExpression();
            left = (left != 0 && right != 0) ? 1 : 0;
        }

        return left;
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

    private decimal EvaluateEqualityExpression()
    {
        decimal left = EvaluateComparisonExpression();
        while (_tokens.Peek().Type == TokenType.EQUALS ||
               _tokens.Peek().Type == TokenType.NOT_EQUALS)
        {
            TokenType op = _tokens.Peek().Type;
            _tokens.Advance();
            decimal right = EvaluateComparisonExpression();
            bool result = op == TokenType.EQUALS ? left == right : left != right;
            left = result ? 1 : 0;
        }

        return left;
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

    private decimal EvaluateComparisonExpression()
    {
        decimal left = EvaluateAdditiveExpression();
        while (_tokens.Peek().Type == TokenType.GREATER_THAN ||
               _tokens.Peek().Type == TokenType.GREATER_OR_EQUAL ||
               _tokens.Peek().Type == TokenType.LESS_THAN ||
               _tokens.Peek().Type == TokenType.LESS_OR_EQUAL)
        {
            TokenType op = _tokens.Peek().Type;
            _tokens.Advance();
            decimal right = EvaluateAdditiveExpression();
            bool result = op switch
            {
                TokenType.GREATER_THAN => left > right,
                TokenType.GREATER_OR_EQUAL => left >= right,
                TokenType.LESS_THAN => left < right,
                TokenType.LESS_OR_EQUAL => left <= right,
                _ => false
            };
            left = result ? 1 : 0;
        }

        return left;
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

    private decimal EvaluateAdditiveExpression()
    {
        decimal left = EvaluateMultiplicativeExpression();
        while (_tokens.Peek().Type == TokenType.PLUS ||
               _tokens.Peek().Type == TokenType.MINUS)
        {
            TokenType op = _tokens.Peek().Type;
            _tokens.Advance();
            decimal right = EvaluateMultiplicativeExpression();
            left = op == TokenType.PLUS ? left + right : left - right;
        }

        return left;
    }

    private void ParseMultiplicativeExpression()
    {
        ParseUnaryExpression();
        while (_tokens.Peek().Type == TokenType.MULTIPLY ||
               _tokens.Peek().Type == TokenType.DIVIDE ||
               _tokens.Peek().Type == TokenType.MODULO)
        {
            _tokens.Advance();
            ParseUnaryExpression();
        }
    }

    private decimal EvaluateMultiplicativeExpression()
    {
        decimal left = EvaluateUnaryExpression();

        while (_tokens.Peek().Type == TokenType.MULTIPLY ||
               _tokens.Peek().Type == TokenType.DIVIDE ||
               _tokens.Peek().Type == TokenType.MODULO)
        {
            TokenType op = _tokens.Peek().Type;
            _tokens.Advance();
            decimal right = EvaluateUnaryExpression();

            left = op switch
            {
                TokenType.MULTIPLY => left * right,
                TokenType.DIVIDE => left / right,
                TokenType.MODULO => left % right,
                _ => left
            };
        }

        return left;
    }

    private void ParsePowerExpression()
    {
        ParsePrimaryExpression();
        if (_tokens.Peek().Type == TokenType.EXPONENTIATION)
        {
            _tokens.Advance();
            ParsePowerExpression();
        }
    }

    private decimal EvaluatePowerExpression()
    {
        decimal left = EvaluatePrimaryExpression();
        if (_tokens.Peek().Type == TokenType.EXPONENTIATION)
        {
            _tokens.Advance();
            decimal right = EvaluatePowerExpression();
            return (decimal)Math.Pow((double)left, (double)right);
        }

        return left;
    }

    private void ParseUnaryExpression()
    {
        // Унарные операторы: +, -, !
        if (_tokens.Peek().Type == TokenType.PLUS ||
            _tokens.Peek().Type == TokenType.MINUS ||
            _tokens.Peek().Type == TokenType.NOT)
        {
            _tokens.Advance();
            ParsePowerExpression(); // Исправлено: после унарного оператора парсим степень
            return;
        }

        // После унарных операторов парсим первичное выражение
        ParsePrimaryExpression();
    }

    private decimal EvaluateUnaryExpression()
    {
        // Обработка унарных операторов: +, -, !
        TokenType tokenType = _tokens.Peek().Type;

        if (tokenType == TokenType.MINUS)
        {
            _tokens.Advance();
            return -EvaluatePowerExpression(); // Исправлено
        }

        if (tokenType == TokenType.PLUS)
        {
            _tokens.Advance();
            return EvaluatePowerExpression(); // Исправлено
        }

        if (tokenType == TokenType.NOT)
        {
            _tokens.Advance();
            return EvaluatePowerExpression() == 0 ? 1 : 0; // Исправлено
        }

        // Если нет унарных операторов, переходим к степени
        return EvaluatePowerExpression();
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
                ParseAssignmentExpression();
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

    private decimal EvaluatePrimaryExpression()
    {
        Token token = _tokens.Peek();
        switch (token.Type)
        {
            case TokenType.NUMERIC_LITERAL:
                _tokens.Advance();
                return token.Value!.ToDecimal();
            case TokenType.ISTINA:
                _tokens.Advance();
                return 1;
            case TokenType.LOZH:
                _tokens.Advance();
                return 0;
            case TokenType.IDENTIFIER:
                string name = token.Value!.ToString()!;
                _tokens.Advance();

                // Проверяем, является ли это вызовом функции
                if (_tokens.Peek().Type == TokenType.OPEN_PARENTHESIS)
                {
                    throw new InvalidOperationException("Function calls are not supported in parser evaluation");
                }

                // Возвращаем значение переменной
                if (Variables.ContainsKey(name))
                {
                    return Variables[name];
                }

                // Если переменная не объявлена, выбрасываем исключение
                throw new InvalidOperationException($"Переменная '{name}' не объявлена");

            case TokenType.OPEN_PARENTHESIS:
                _tokens.Advance();
                decimal value = EvaluateAssignmentExpression();
                Match(TokenType.CLOSE_PARENTHESIS);
                return value;

            case TokenType.IZLUCHAT:
            case TokenType.PRIEM_SIGNALA:
                throw new InvalidOperationException("Built-in functions are not supported in parser evaluation");
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

    private string MatchIdentifier()
    {
        Token token = _tokens.Peek();
        if (token.Type != TokenType.IDENTIFIER)
        {
            throw new UnexpectedLexemeException(TokenType.IDENTIFIER, token);
        }

        string identifier = token.Value!.ToString()!;
        _tokens.Advance();
        return identifier;
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