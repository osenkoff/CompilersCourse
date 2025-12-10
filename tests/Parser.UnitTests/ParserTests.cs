using StarLightParser;

using Xunit;

namespace StarLightParser.UnitTests;

public class ParserTests
{
    // Существующие тесты
    [Fact]
    public void Can_parse_numeric_literal()
    {
        // Arrange
        string code = "42";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_decimal_number()
    {
        // Arrange
        string code = "3.14159";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_boolean_true()
    {
        // Arrange
        string code = "ИСТИНА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_boolean_false()
    {
        // Arrange
        string code = "ЛОЖЬ";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_addition_expression()
    {
        // Arrange
        string code = "2 + 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_subtraction_expression()
    {
        // Arrange
        string code = "5 - 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_multiplication_expression()
    {
        // Arrange
        string code = "3 * 4";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_division_expression()
    {
        // Arrange
        string code = "10 / 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_modulo_expression()
    {
        // Arrange
        string code = "7 % 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_power_expression()
    {
        // Arrange
        string code = "2 ** 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_expression_with_parentheses()
    {
        // Arrange
        string code = "(2 + 3)";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_complex_expression_with_priority()
    {
        // Arrange
        string code = "2 + 3 * 4";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_priority_with_parentheses()
    {
        // Arrange
        string code = "(2 + 3) * 4";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_unary_minus()
    {
        // Arrange
        string code = "-5";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_unary_plus()
    {
        // Arrange
        string code = "+5";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_unary_not()
    {
        // Arrange
        string code = "!ИСТИНА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
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
    public void Throws_on_unexpected_token()
    {
        // Arrange
        string code = "2 + ";
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

    // НОВЫЕ ТЕСТЫ для полного покрытия
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
    public void Can_parse_equality_expression()
    {
        // Arrange
        string code = "a == b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_inequality_expression()
    {
        // Arrange
        string code = "a != b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_greater_than_expression()
    {
        // Arrange
        string code = "a > b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_greater_or_equal_expression()
    {
        // Arrange
        string code = "a >= b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_less_than_expression()
    {
        // Arrange
        string code = "a < b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_less_or_equal_expression()
    {
        // Arrange
        string code = "a <= b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_logical_and_expression()
    {
        // Arrange
        string code = "a && b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_logical_or_expression()
    {
        // Arrange
        string code = "a || b";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_complex_comparison_expression()
    {
        // Arrange
        string code = "a > b && c < d";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_nested_parentheses_expression()
    {
        // Arrange
        string code = "((2 + 3) * (4 - 1))";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_power_priority_expression()
    {
        // Arrange
        string code = "2 * 3 ** 4";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_right_associative_power()
    {
        // Arrange
        string code = "2 ** 3 ** 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_left_associative_addition()
    {
        // Arrange
        string code = "1 + 2 + 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_left_associative_subtraction()
    {
        // Arrange
        string code = "10 - 5 - 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_unary_minus_priority()
    {
        // Arrange
        string code = "-3 * 4";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
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
    public void Throws_on_missing_operand()
    {
        // Arrange
        string code = "2 +";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    [Fact]
    public void Throws_on_missing_operator()
    {
        // Arrange
        string code = "2 + * 3"; // Более явная ошибка - два оператора подряд
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    [Fact]
    public void Can_parse_plus_assignment()
    {
        // Arrange
        string code = "x += 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_minus_assignment()
    {
        // Arrange
        string code = "x -= 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_multiply_assignment()
    {
        // Arrange
        string code = "x *= 4";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_divide_assignment()
    {
        // Arrange
        string code = "x /= 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_power_assignment()
    {
        // Arrange
        string code = "x **= 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_complex_mixed_operations()
    {
        // Arrange
        string code = "a + b * c > d && e == f";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_unary_with_power_priority()
    {
        // Arrange
        string code = "-x ** 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_and_over_or_priority()
    {
        // Arrange
        string code = "a && b || c && d";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_complex_arithmetic_expression()
    {
        // Arrange
        string code = "2 + 3 * (4 - 1) ** 2";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_mixed_logical_expression()
    {
        // Arrange
        string code = "a > 5 && b < 10 || c == 3";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    [Fact]
    public void Throws_on_invalid_operator_sequence()
    {
        // Arrange
        string code = "2 * * 3"; // Два оператора умножения подряд
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseExpression());
    }

    [Fact]
    public void Can_parse_deeply_nested_expression()
    {
        // Arrange
        string code = "((1 + (2 * (3 - (4 / 5)))))";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseExpression());
        Assert.Null(exception);
    }

    // Тесты для объявлений переменных и констант
    [Fact]
    public void Can_parse_variable_declaration_without_initialization()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_variable_declaration_with_initialization()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_variable_declaration_nova_type()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ y : нова = 3.14; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_variable_declaration_vakuum_type()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ флаг : вакуум = ИСТИНА; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_variable_declaration_with_expression()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ сумма : квазар = 2 + 3; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_constant_declaration()
    {
        // Arrange
        string code = "ЗВЕЗДА КОНСТЕЛЛАЦИЯ e : нова = 2.718; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_constant_declaration_kvazar_type()
    {
        // Arrange
        string code = "ЗВЕЗДА КОНСТЕЛЛАЦИЯ pi : квазар = 3; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Throws_on_constant_without_initialization()
    {
        // Arrange
        string code = "ЗВЕЗДА КОНСТЕЛЛАЦИЯ x : квазар; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    [Fact]
    public void Throws_on_variable_declaration_without_semicolon()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    // Тесты для инструкций ввода-вывода
    [Fact]
    public void Can_parse_print_statement_with_one_argument()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(42); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_print_statement_with_multiple_arguments()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(42, 3.14); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_print_statement_with_expression()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(x + y); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_print_statement_without_arguments()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_input_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА ПРИЕМ_СИГНАЛА(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Throws_on_print_statement_without_semicolon()
    {
        // Arrange
        string code = "ЗВЕЗДА ИЗЛУЧАТЬ(42) ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    [Fact]
    public void Throws_on_input_statement_without_semicolon()
    {
        // Arrange
        string code = "ЗВЕЗДА ПРИЕМ_СИГНАЛА(x) ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    // Тесты для присваиваний как инструкций
    [Fact]
    public void Can_parse_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА x = 5; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_plus_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА x += 3; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_minus_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА x -= 2; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_multiply_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА x *= 4; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_divide_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА x /= 2; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_power_assignment_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА x **= 2; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_assignment_with_expression()
    {
        // Arrange
        string code = "ЗВЕЗДА x = a + b * c; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Throws_on_assignment_without_semicolon()
    {
        // Arrange
        string code = "ЗВЕЗДА x = 5 ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    // Тесты для структуры программы
    [Fact]
    public void Can_parse_empty_program()
    {
        // Arrange
        string code = "ЗВЕЗДА ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_program_with_one_statement()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_program_with_multiple_statements()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; СВЕТ y : квазар = 10; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_program_with_declarations_and_assignments()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5; x = 10; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_parse_program_with_input_output()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар; ПРИЕМ_СИГНАЛА(x); ИЗЛУЧАТЬ(x); ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Exception exception = Record.Exception(() => parser.ParseProgram());
        Assert.Null(exception);
    }

    [Fact]
    public void Throws_on_program_without_zvezda()
    {
        // Arrange
        string code = "СВЕТ x : квазар = 5; ЗАКРЫТАЯ_ЗВЕЗДА";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }

    [Fact]
    public void Throws_on_program_without_zakrytaya_zvezda()
    {
        // Arrange
        string code = "ЗВЕЗДА СВЕТ x : квазар = 5;";
        Parser parser = new Parser(code);

        // Act & Assert
        Assert.Throws<UnexpectedLexemeException>(() => parser.ParseProgram());
    }
}