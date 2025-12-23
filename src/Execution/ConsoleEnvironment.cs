namespace Execution;

/// <summary>
/// Реализация <see cref="IEnvironment"/> для работы с консолью.
/// </summary>
public class ConsoleEnvironment : IEnvironment
{
    /// <inheritdoc />
    public decimal ReadNumber()
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (input != null && decimal.TryParse(input, out decimal result))
            {
                return result;
            }

            Console.WriteLine("Ошибка! Введите корректное число:");
        }
    }

    /// <inheritdoc />
    public void WriteNumber(decimal value)
    {
        Console.WriteLine(value);
    }
}
