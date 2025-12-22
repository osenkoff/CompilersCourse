using StarLightLexer;

namespace StarLightParser;

public class ParserTests
{
    [Fact]
    public void Can_parse_numeric_literal()
    {
        // Arrange
        string code = "42";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(42m, result);
    }

    [Fact]
    public void Can_parse_decimal_number()
    {
        // Arrange
        string code = "3.14159";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(3.14159m, result);
    }

    [Fact]
    public void Can_parse_boolean_true()
    {
        // Arrange
        string code = "ИСТИНА";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result);
    }

    [Fact]
    public void Can_parse_boolean_false()
    {
        // Arrange
        string code = "ЛОЖЬ";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result);
    }

    [Fact]
    public void Can_parse_identifier()
    {
        // Arrange
        string code = "переменная";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_identifier_with_numbers()
    {
        // Arrange
        string code = "x1";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_russian_identifier()
    {
        // Arrange
        string code = "счетчик";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_addition_expression()
    {
        // Arrange
        string code = "5 + 2";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(7m, result);
    }

    [Fact]
    public void Can_parse_subtraction_expression()
    {
        // Arrange
        string code = "5 - 2";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(3m, result);
    }

    [Fact]
    public void Can_parse_multiplication_expression()
    {
        // Arrange
        string code = "3 * 4";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(12m, result);
    }

    [Fact]
    public void Can_parse_division_expression()
    {
        // Arrange
        string code = "10 / 2";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(5m, result);
    }

    [Fact]
    public void Can_parse_modulo_expression()
    {
        // Arrange
        string code = "7 % 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result);
    }

    [Fact]
    public void Can_parse_power_expression()
    {
        // Arrange
        string code = "2 ** 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(8m, result);
    }

    [Fact]
    public void Can_parse_right_associative_power()
    {
        // Arrange
        string code = "2 ** (3 ** 2)";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(512m, result); // 2 ** (3 ** 2) = 2 ** 9 = 512
    }

    [Fact]
    public void Can_parse_left_associative_addition()
    {
        // Arrange
        string code = "(1 + 2) + 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(6m, result);
    }

    [Fact]
    public void Can_parse_left_associative_subtraction()
    {
        // Arrange
        string code = "(10 - 5) - 2";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(3m, result);
    }

    [Fact]
    public void Can_parse_expression_with_parentheses()
    {
        // Arrange
        string code = "(2 + 3)";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(5m, result);
    }

    [Fact]
    public void Can_parse_complex_expression_with_priority()
    {
        // Arrange
        string code = "2 + 3 * 4";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(14m, result);
    }

    [Fact]
    public void Can_parse_priority_with_parentheses()
    {
        // Arrange
        string code = "(2 + 3) * 4";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(20m, result);
    }

    [Fact]
    public void Can_parse_priority_with_power()
    {
        // Arrange
        string code = "2 * 3 ** 4";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(162m, result);
    }

    [Fact]
    public void Can_parse_nested_parentheses_expression()
    {
        // Arrange
        string code = "((2 + 3) * (4 - 1))";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(15m, result);
    }

    [Fact]
    public void Can_parse_deeply_nested_expression()
    {
        // Arrange
        string code = "((1 + (2 * (3 - (4 / 5)))))";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(5.4m, result); // 1 + 2 * (3 - 0.8) = 1 + 2 * 2.2 = 1 + 4.4 = 5.4
    }

    [Fact]
    public void Can_parse_hard_expression()
    {
        // Arrange
        string code = "2 + 3 * (4 - 1) ** 2";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(29m, result);
    }

    [Fact]
    public void Can_parse_unary_minus()
    {
        // Arrange
        string code = "-5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(-5m, result);
    }

    [Fact]
    public void Can_parse_unary_plus()
    {
        // Arrange
        string code = "+5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(5m, result);
    }

    [Fact]
    public void Can_parse_unary_not()
    {
        // Arrange
        string code = "!ИСТИНА";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result);
    }

    [Fact]
    public void Can_parse_unary_minus_priority()
    {
        // Arrange
        string code = "-3 * 4";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(-12m, result);
    }

    [Fact]
    public void Can_parse_unary_with_power_priority()
    {
        // Arrange
        string code = "-x ** 2";
        Dictionary<string, decimal> initialVars = new Dictionary<string, decimal> { { "x", 3m } };
        Parser parser = new Parser(code, initialVars);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        // -(x ** 2) = -(3 ** 2) = -9
        Assert.Equal(-9m, result);
    }

    [Fact]
    public void Unary_has_lower_priority_than_power()
    {
        string code = "-3 ** 2";
        Parser parser = new Parser(code);

        decimal result = parser.EvaluateExpression();

        Assert.Equal(9m, result);
    }

    [Fact]
    public void Can_parse_equality_expression()
    {
        // Arrange
        string code = "5 == 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_equality_expression_false()
    {
        // Arrange
        string code = "5 == 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_parse_inequality_expression()
    {
        // Arrange
        string code = "5 != 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_inequality_expression_false()
    {
        // Arrange
        string code = "5 != 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_parse_greater_than_expression()
    {
        // Arrange
        string code = "5 > 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_greater_than_expression_false()
    {
        // Arrange
        string code = "3 > 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_parse_greater_or_equal_expression()
    {
        // Arrange
        string code = "5 >= 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_greater_or_equal_expression_false()
    {
        // Arrange
        string code = "3 >= 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_parse_less_than_expression()
    {
        // Arrange
        string code = "3 < 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_less_than_expression_false()
    {
        // Arrange
        string code = "5 < 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_parse_less_or_equal_expression()
    {
        // Arrange
        string code = "3 <= 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_less_or_equal_expression_false()
    {
        // Arrange
        string code = "5 <= 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_parse_logical_and_expression()
    {
        // Arrange
        string code = "ИСТИНА && ИСТИНА";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_logical_and_expression_false()
    {
        // Arrange
        string code = "ИСТИНА && ЛОЖЬ";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_parse_logical_or_expression()
    {
        // Arrange
        string code = "ИСТИНА || ЛОЖЬ";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_parse_logical_or_expression_false()
    {
        // Arrange
        string code = "ЛОЖЬ || ЛОЖЬ";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(0m, result); // false
    }

    [Fact]
    public void Can_evaluate_and_over_or_priority_with_values()
    {
        // Arrange
        string code = "ИСТИНА && ЛОЖЬ || ИСТИНА && ИСТИНА";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        // (ИСТИНА && ЛОЖЬ) || (ИСТИНА && ИСТИНА) = ЛОЖЬ || ИСТИНА = ИСТИНА
        Assert.Equal(1m, result);
    }

    [Fact]
    public void Can_parse_complex_comparison_expression()
    {
        // Arrange
        string code = "5 > 3 && 2 < 4";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(1m, result); // true
    }

    [Fact]
    public void Can_evaluate_complex_mixed_operations()
    {
        // Arrange
        string code = "2 + 3 * 4 > 10 && 5 == 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        // (2 + 3 * 4 > 10) && (5 == 5) = (2 + 12 > 10) && ИСТИНА = (14 > 10) && ИСТИНА = ИСТИНА && ИСТИНА = ИСТИНА
        Assert.Equal(1m, result);
    }

    [Fact]
    public void Can_evaluate_mixed_logical_expression_with_values()
    {
        // Arrange
        string code = "6 > 5 && 8 < 10 || 3 == 3";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        // (6 > 5 && 8 < 10) || (3 == 3) = (ИСТИНА && ИСТИНА) || ИСТИНА = ИСТИНА || ИСТИНА = ИСТИНА
        Assert.Equal(1m, result);
    }

    [Fact]
    public void Can_evaluate_assignment_expression()
    {
        // Arrange
        string code = "x = 5";
        Parser parser = new Parser(code);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(5m, result);
        Assert.Equal(5m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_plus_assignment()
    {
        // Arrange
        string code = "x += 3";
        Dictionary<string, decimal> initialVars = new Dictionary<string, decimal> { { "x", 5m } };
        Parser parser = new Parser(code, initialVars);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(8m, result);
        Assert.Equal(8m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_minus_assignment()
    {
        // Arrange
        string code = "x -= 2";
        Dictionary<string, decimal> initialVars = new Dictionary<string, decimal> { { "x", 7m } };
        Parser parser = new Parser(code, initialVars);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(5m, result);
        Assert.Equal(5m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_multiply_assignment()
    {
        // Arrange
        string code = "x *= 4";
        Dictionary<string, decimal> initialVars = new Dictionary<string, decimal> { { "x", 3m } };
        Parser parser = new Parser(code, initialVars);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(12m, result);
        Assert.Equal(12m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_divide_assignment()
    {
        // Arrange
        string code = "x /= 2";
        Dictionary<string, decimal> initialVars = new Dictionary<string, decimal> { { "x", 10m } };
        Parser parser = new Parser(code, initialVars);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(5m, result);
        Assert.Equal(5m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_power_assignment()
    {
        // Arrange
        string code = "x **= 2";
        Dictionary<string, decimal> initialVars = new Dictionary<string, decimal> { { "x", 3m } };
        Parser parser = new Parser(code, initialVars);

        // Act
        decimal result = parser.EvaluateExpression();

        // Assert
        Assert.Equal(9m, result);
        Assert.Equal(9m, parser.Variables["x"]);
    }

    [Fact]
    public void Throws_on_missing_operand()
    {
        // Arrange
        string code = "2 +";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    [Fact]
    public void Throws_on_unclosed_parenthesis()
    {
        // Arrange
        string code = "(2 + 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    [Fact]
    public void Throws_on_empty_parentheses()
    {
        // Arrange
        string code = "()";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    [Fact]
    public void Throws_on_missing_operator()
    {
        // Arrange
        string code = "2 + * 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    [Fact]
    public void Throws_on_invalid_operator_sequence()
    {
        // Arrange
        string code = "2 * * 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    // 4.1D

    // Добавим в ParserTests.cs
    [Fact]
    public void Can_evaluate_program_with_print()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(42); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(42m, results[0]);
    }

    [Fact]
    public void Can_evaluate_program_with_expression_in_print()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(2 + 3 * 4); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(14m, results[0]);
    }

    [Fact]
    public void Can_evaluate_program_with_multiple_prints()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(1, 2, 3); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Equal(1m, results[0]);
        Assert.Equal(2m, results[1]);
        Assert.Equal(3m, results[2]);
    }

    [Fact]
    public void Throws_on_undefined_variable_in_expression()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => parser.ParseProgram());
        Assert.Contains("не объявлена", exception.Message);
    }

    [Fact]
    public void Throws_on_input_to_undeclared_variable()
    {
        // Arrange
        string code = "ЗВЕЗДА ПРИЕМ_СИГНАЛА(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => parser.ParseProgram());
    }

    // Объявление переменных и констант
    [Fact]
    public void Can_evaluate_program_with_variable_and_print()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 10; ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(10m, results[0]);
        Assert.Equal(10m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_program_with_variable_assignment()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; x = 42; ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(42m, results[0]);
        Assert.Equal(42m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_program_with_compound_assignment()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; x += 10; ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(15m, results[0]);
        Assert.Equal(15m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_evaluate_program_with_constant()
    {
        // Arrange
        string code = "ЗВЕЗДА КОНСТЕЛЛАЦИЯ пи : нова = 3.14159; ИЗЛУЧАТЬ(пи); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(3.14159m, results[0]);
        Assert.Equal(3.14159m, parser.Variables["пи"]);
    }

    [Fact]
    public void Throws_on_assignment_to_constant()
    {
        // Arrange
        string code = "ЗВЕЗДА КОНСТЕЛЛАЦИЯ x : квазар = 5; x = 10; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => parser.ParseProgram());
        Assert.Contains("Нельзя изменять константу", exception.Message);
    }

    [Fact]
    public void Can_evaluate_program_with_multiple_statements()
    {
        // Arrange
        string code = """
        ЗВЕЗДА
            СВЕТ a : квазар = 1;
            СВЕТ b : квазар = 2;
            ИЗЛУЧАТЬ(a + b);
        ЗАКРЫТАЯ_ЗВЕЗДА
        """;
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(3m, results[0]);
        Assert.Equal(1m, parser.Variables["a"]);
        Assert.Equal(2m, parser.Variables["b"]);
    }

    [Fact]
    public void Can_evaluate_empty_print_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void Throws_on_duplicate_variable_declaration()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 1; СВЕТ x : квазар = 2; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => parser.ParseProgram());
        Assert.Contains("уже объявлена", exception.Message);
    }

    [Fact]
    public void Can_parse_program_with_only_end_keywords()
    {
        // Arrange
        string code = "ЗВЕЗДА ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        Exception exception = Record.Exception(() => parser.ParseProgram());

        // Assert
        Assert.Null(exception);
        Assert.Empty(parser.Results);
    }

    [Fact]
    public void Can_parse_variable_declaration_without_initialization()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();

        // Assert
        Assert.Equal(0m, parser.Variables["x"]);
        Assert.True(parser.Variables.ContainsKey("x"));
    }

    [Fact]
    public void Can_parse_variable_declaration_with_vacuum_type_and_true()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ флаг : вакуум = ИСТИНА; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();

        // Assert
        Assert.Equal(1m, parser.Variables["флаг"]);
    }

    [Fact]
    public void Can_parse_variable_declaration_with_vacuum_type_and_false()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ флаг : вакуум = ЛОЖЬ; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();

        // Assert
        Assert.Equal(0m, parser.Variables["флаг"]);
    }

    [Fact]
    public void Can_parse_variable_declaration_with_galaktika_type()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : галактика = 10; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        Exception exception = Record.Exception(() => parser.ParseProgram());

        // Assert
        Assert.Null(exception);
        Assert.Equal(10m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_parse_variable_declaration_with_nova_type()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : нова = 3.14; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        Exception exception = Record.Exception(() => parser.ParseProgram());

        // Assert
        Assert.Null(exception);
        Assert.Equal(3.14m, parser.Variables["x"]);
    }

    [Fact]
    public void Throws_on_missing_semicolon_after_variable_declaration()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5 ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    [Fact]
    public void Throws_on_assignment_to_undeclared_variable()
    {
        // Arrange
        string code = "ЗВЕЗДА x = 5; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => parser.ParseProgram());
    }

    [Fact]
    public void Can_parse_simple_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 1; x = 42; ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(42m, results[0]);
        Assert.Equal(42m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_parse_plus_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; x += 3; ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(8m, results[0]);
        Assert.Equal(8m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_parse_multiply_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 3; x *= 4; ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(12m, results[0]);
        Assert.Equal(12m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_parse_assignment_with_expression_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ y : квазар; СВЕТ x : квазар = 2; y = x * 2 + 3; ИЗЛУЧАТЬ(y); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(7m, results[0]);
        Assert.Equal(7m, parser.Variables["y"]);
    }

    [Fact]
    public void Can_parse_input_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; ПРИЕМ_СИГНАЛА(x); ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(5m, results[0]); // Значение остается 5
        Assert.Equal(5m, parser.Variables["x"]);
    }

    [Fact]
    public void Can_parse_program_with_only_variable_declaration()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        Exception exception = Record.Exception(() => parser.ParseProgram());

        // Assert
        Assert.Null(exception);
        Assert.Equal(5m, parser.Variables["x"]);
        Assert.Empty(parser.Results);
    }

    [Fact]
    public void Can_parse_program_with_multiple_variables_and_prints()
    {
        // Arrange
        string code = """
    ЗВЕЗДА
        СВЕТ a : квазар = 1;
        СВЕТ b : квазар = 2;
        СВЕТ сумма : квазар;
        сумма = a + b;
        ИЗЛУЧАТЬ(a, b, сумма);
    ЗАКРЫТАЯ_ЗВЕЗДА
    """;
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Equal(1m, results[0]);
        Assert.Equal(2m, results[1]);
        Assert.Equal(3m, results[2]);
        Assert.Equal(3m, parser.Variables["сумма"]);
    }

    [Fact]
    public void Can_parse_program_with_nested_expressions_in_print()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ((2 + 3) * (4 - 1)); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(15m, results[0]);
    }

    [Fact]
    public void Can_parse_program_with_complex_assignment_chain()
    {
        // Arrange
        string code = """
    ЗВЕЗДА
        СВЕТ a : квазар = 10;
        СВЕТ b : квазар;
        СВЕТ c : квазар;
        b = a * 2;
        c = b + 5;
        ИЗЛУЧАТЬ(a, b, c);
    ЗАКРЫТАЯ_ЗВЕЗДА
    """;
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Equal(10m, results[0]);
        Assert.Equal(20m, results[1]);
        Assert.Equal(25m, results[2]);
        Assert.Equal(10m, parser.Variables["a"]);
        Assert.Equal(20m, parser.Variables["b"]);
        Assert.Equal(25m, parser.Variables["c"]);
    }

    [Fact]
    public void Throws_on_missing_semicolon_after_assignment()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 1; x = 2 ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    [Fact]
    public void Can_parse_program_with_constant_and_variable()
    {
        // Arrange
        string code = """
    ЗВЕЗДА
        КОНСТЕЛЛАЦИЯ ПИ : нова = 3.14159;
        СВЕТ радиус : квазар = 5;
        СВЕТ площадь : нова;
        площадь = ПИ * радиус * радиус;
        ИЗЛУЧАТЬ(площадь);
    ЗАКРЫТАЯ_ЗВЕЗДА
    """;
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Single(results);
        Assert.Equal(78.53975m, results[0]); // 3.14159 * 5 * 5
        Assert.Equal(3.14159m, parser.Variables["ПИ"]);
        Assert.Equal(5m, parser.Variables["радиус"]);
        Assert.Equal(78.53975m, parser.Variables["площадь"]);
    }

    [Fact]
    public void Can_parse_empty_print_statement_in_program()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void Can_parse_complex_expression_with_all_operators_in_program()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(2 + 3 * 4 > 10 && 5 == 5 || 3 < 2); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act
        parser.ParseProgram();
        IReadOnlyList<decimal> results = parser.Results;

        // Assert
        // (2 + 3 * 4 > 10) && (5 == 5) || (3 < 2) = (14 > 10) && ИСТИНА || ЛОЖЬ = ИСТИНА && ИСТИНА || ЛОЖЬ = ИСТИНА || ЛОЖЬ = ИСТИНА
        Assert.Single(results);
        Assert.Equal(1m, results[0]);
    }

    [Fact]
    public void Throws_on_invalid_type_in_declaration()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : НЕПРАВИЛЬНЫЙ_ТИП = 5; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    [Fact]
    public void Can_parse_variable_name_with_underscore()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ my_var : квазар = 10; ИЗЛУЧАТЬ(my_var); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }
}