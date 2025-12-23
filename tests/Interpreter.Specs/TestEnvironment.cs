using System.Globalization;

using Execution;
using Reqnroll;

namespace Interpreter.Specs;

/// <summary>
/// Тестовая реализация окружения <see cref="IEnvironment"/> для сценариев Reqnroll.
/// Хранит ввод/вывод в памяти, чтобы шаги могли проверять результаты работы интерпретатора.
/// </summary>
public class TestEnvironment : IEnvironment
{
    private readonly Queue<decimal> _inputQueue = new();
    private readonly List<string> _outputLines = new();

    /// <summary>
    /// Полный вывод интерпретатора, строки разделены переводом строки LF.
    /// </summary>
    public string Output => string.Join("\n", _outputLines);

    public decimal ReadNumber()
    {
        if (_inputQueue.Count == 0)
        {
            throw new InvalidOperationException("No input available in test environment");
        }

        return _inputQueue.Dequeue();
    }

    /// <summary>
    /// Реализация вывода числа для интерпретатора.
    /// Используем инвариантную культуру, чтобы всегда получать точку в качестве разделителя.
    /// </summary>
    public void WriteNumber(decimal value)
    {
        string message = value.ToString(CultureInfo.InvariantCulture);
        _outputLines.Add(message);
    }

    /// <summary>
    /// Заполняет очередь ввода значениями из таблицы Gherkin.
    /// </summary>
    public void SetInputFromTable(Table table)
    {
        _inputQueue.Clear();
        foreach (DataTableRow? row in table.Rows)
        {
            string value = row[0].Replace(",", ".");
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue))
            {
                _inputQueue.Enqueue(decimalValue);
            }
        }
    }

    /// <summary>
    /// Очищает накопленный вывод.
    /// </summary>
    public void ClearOutput()
    {
        _outputLines.Clear();
    }
}