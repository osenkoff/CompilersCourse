using Execution;
using StarLightParser;

namespace Interpreter;

/// <summary>
/// Интерпретатор языка DEA.
/// Является фасадом над парсером и окружением ввода/вывода.
/// </summary>
public class Interpreter
{
    private readonly Context _context;
    private readonly IEnvironment _environment;

    public Interpreter(IEnvironment environment)
    {
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        _context = new Context();
    }

    /// <summary>
    /// Выполняет программу на языке DEA.
    /// </summary>
    /// <param name="sourceCode">Исходный код программы.</param>
    public void Execute(string sourceCode)
    {
        if (string.IsNullOrWhiteSpace(sourceCode))
        {
            throw new ArgumentException("Source code cannot be null or empty.", nameof(sourceCode));
        }

        // Используем текущую область видимости контекста как хранилище переменных парсера.
        Parser parser = new Parser(
            sourceCode,
            initialVars: (Dictionary<string, decimal>)_context.CurrentScope,
            inputProvider: _environment.ReadNumber,
            outputConsumer: _environment.WriteNumber);

        parser.ParseProgram();
    }
}