namespace Execution;

/// <summary>
/// Представляет окружение для выполнения программы.
/// Отвечает за ввод/вывод данных.
/// </summary>
public interface IEnvironment
{
    /// <summary>
    /// Читает число из входного потока.
    /// </summary>
    /// <returns>Прочитанное число.</returns>
    decimal ReadNumber();

    /// <summary>
    /// Выводит число в выходной поток.
    /// </summary>
    /// <param name="value">Значение для вывода.</param>
    void WriteNumber(decimal value);
}
