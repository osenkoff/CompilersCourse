using Execution;

namespace Interpreter;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: interpreter <file-path>");
            return 1;
        }

        string filePath = args[0];

        try
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Error: File '{filePath}' not found.");
                return 1;
            }

            string sourceCode = File.ReadAllText(filePath);

            ConsoleEnvironment environment = new ConsoleEnvironment();
            Interpreter interpreter = new Interpreter(environment);
            interpreter.Execute(sourceCode);

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }
}
