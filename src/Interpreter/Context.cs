using System.Collections.Immutable;

namespace Interpreter;

/// <summary>
/// Контекст выполнения программы.
/// Управляет областями видимости и значениями переменных.
/// </summary>
public class Context
{
    private readonly Stack<Dictionary<string, decimal>> _scopes = new();

    public Context()
    {
        // Глобальная область видимости по умолчанию
        _scopes.Push(new Dictionary<string, decimal>());
    }

    /// <summary>
    /// Текущая область видимости.
    /// </summary>
    public IDictionary<string, decimal> CurrentScope => _scopes.Peek();

    /// <summary>
    /// Все переменные из текущей области видимости (копия).
    /// </summary>
    public IReadOnlyDictionary<string, decimal> Variables =>
        CurrentScope.ToImmutableDictionary();

    /// <summary>
    /// Добавляет новую область видимости.
    /// </summary>
    public void PushScope()
    {
        _scopes.Push(new Dictionary<string, decimal>(CurrentScope));
    }

    /// <summary>
    /// Удаляет текущую область видимости.
    /// </summary>
    public void PopScope()
    {
        if (_scopes.Count == 1)
        {
            throw new InvalidOperationException("Нельзя удалить глобальную область видимости.");
        }

        _scopes.Pop();
    }

    /// <summary>
    /// Объявляет новую переменную.
    /// </summary>
    public void DefineVariable(string name, decimal value)
    {
        if (CurrentScope.ContainsKey(name))
        {
            throw new InvalidOperationException($"Переменная '{name}' уже объявлена.");
        }

        CurrentScope[name] = value;
    }

    /// <summary>
    /// Присваивает значение существующей переменной.
    /// </summary>
    public void AssignVariable(string name, decimal value)
    {
        if (!CurrentScope.ContainsKey(name))
        {
            throw new InvalidOperationException($"Переменная '{name}' не объявлена.");
        }

        CurrentScope[name] = value;
    }

    /// <summary>
    /// Возвращает значение переменной.
    /// </summary>
    public decimal GetValue(string name)
    {
        if (!CurrentScope.TryGetValue(name, out decimal value))
        {
            throw new InvalidOperationException($"Переменная '{name}' не объявлена.");
        }

        return value;
    }
}
