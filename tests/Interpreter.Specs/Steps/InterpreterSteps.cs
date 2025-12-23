using System.Globalization;
using System.Text.RegularExpressions;

using Reqnroll;

using Xunit;

namespace Interpreter.Specs;

/// <summary>
/// Описания шагов Gherkin для запуска интерпретатора DEA из сценариев Reqnroll.
/// </summary>
[Binding]
public class InterpreterSteps
{
    private readonly TestEnvironment _testEnvironment;
    private readonly global::Interpreter.Interpreter _interpreter;
    private string _programCode = string.Empty;

    public InterpreterSteps()
    {
        _testEnvironment = new TestEnvironment();
        _interpreter = new global::Interpreter.Interpreter(_testEnvironment);
    }

    [When(@"я выполняю программу:")]
    public void WhenIExecuteProgram(string multilineText)
    {
        _programCode = multilineText;
        _testEnvironment.ClearOutput();

        _interpreter.Execute(_programCode);
    }

    [When(@"я ввожу в консоли:")]
    public void WhenIEnterInConsole(Table table)
    {
        _testEnvironment.SetInputFromTable(table);
    }

    [Then(@"я получаю результаты:")]
    public void ThenIGetResults(string expectedOutput)
    {
        string actualOutput = _testEnvironment.Output.Trim();
        string expected = expectedOutput.Trim();

        actualOutput = NormalizeLineEndings(actualOutput);
        expected = NormalizeLineEndings(expected);

        // Если вывод содержит только числа, сравниваем их как числа
        if (IsNumericOutput(actualOutput) && IsNumericOutput(expected))
        {
            CompareNumericOutputs(actualOutput, expected);
        }
        else
        {
            Assert.Equal(expected, actualOutput);
        }
    }

    private static bool IsNumericOutput(string output)
    {
        // Проверяем, состоит ли вывод только из чисел, пробелов, точек и минусов
        return Regex.IsMatch(output, @"^[\d\s\.\-]+$");
    }

    private static void CompareNumericOutputs(string actualOutput, string expectedOutput)
    {
        List<decimal> actualNumbers = SplitNumbers(actualOutput);
        List<decimal> expectedNumbers = SplitNumbers(expectedOutput);

        Assert.Equal(expectedNumbers.Count, actualNumbers.Count);

        for (int i = 0; i < expectedNumbers.Count; i++)
        {
            decimal actualNumber = actualNumbers[i];
            decimal expectedNumber = expectedNumbers[i];

            // Сравниваем с точностью до 10^-28 (максимальная точность decimal)
            Assert.InRange(
                actualNumber,
                expectedNumber - 0.0000000000000000000000000001m,
                expectedNumber + 0.0000000000000000000000000001m);
        }
    }

    private static List<decimal> SplitNumbers(string output)
    {
        List<decimal> numbers = new List<decimal>();
        string[] parts = output.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in parts)
        {
            if (decimal.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal number))
            {
                numbers.Add(number);
            }
        }

        return numbers;
    }

    private static string NormalizeLineEndings(string text)
    {
        return text.Replace("\r\n", "\n").Replace("\r", "\n");
    }
}