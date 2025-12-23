using System.Collections.Immutable;

namespace Execution;

/// <summary>
/// Фейковое окружение для тестирования.
/// Симулирует ввод/вывод без реального использования консоли.
/// </summary>
public class FakeEnvironment : IEnvironment
{
    private readonly Queue<decimal> _inputs = new();
    private readonly List<decimal> _outputs = new();

    public FakeEnvironment(IEnumerable<decimal>? inputs = null)
    {
        if (inputs is null)
        {
            return;
        }

        foreach (decimal value in inputs)
        {
            _inputs.Enqueue(value);
        }
    }

    /// <summary>
    /// Результаты, которые были выведены интерпретатором.
    /// </summary>
    public IReadOnlyList<decimal> Outputs => _outputs.ToImmutableArray();

    /// <inheritdoc />
    public decimal ReadNumber()
    {
        if (_inputs.Count == 0)
        {
            throw new InvalidOperationException("Нет входных данных в тестовом окружении.");
        }

        return _inputs.Dequeue();
    }

    /// <inheritdoc />
    public void WriteNumber(decimal value)
    {
        _outputs.Add(value);
    }
}
