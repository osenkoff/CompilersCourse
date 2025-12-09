namespace StarLightParser;

public static class BuiltinFunctions
{
    public static decimal Invoke(string name, List<decimal> arguments)
    {
        return name switch
        {
            "ИЗЛУЧАТЬ" => Emit(arguments),
            "ПРИЕМ_СИГНАЛА" => ReceiveSignal(arguments),
            "abs" => Abs(arguments),
            "min" => Min(arguments),
            "max" => Max(arguments),
            "sin" => Sin(arguments),
            "cos" => Cos(arguments),
            "tan" => Tan(arguments),
            "round" => Round(arguments),
            "ceil" => Ceil(arguments),
            "floor" => Floor(arguments),
            _ => throw new ArgumentException($"Unknown function: {name}")
        };
    }

    private static decimal Emit(List<decimal> arguments)
    {
        ValidateArgumentCount("ИЗЛУЧАТЬ", 1, arguments.Count);
        Console.WriteLine(arguments[0]);
        return arguments[0];
    }

    private static decimal ReceiveSignal(List<decimal> arguments)
    {
        ValidateArgumentCount("ПРИЕМ_СИГНАЛА", 0, arguments.Count);

        // For testing, return 0. In real implementation, read from input
        return 0;
    }

    private static decimal Abs(List<decimal> arguments)
    {
        ValidateArgumentCount("abs", 1, arguments.Count);
        return Math.Abs(arguments[0]);
    }

    private static decimal Min(List<decimal> arguments)
    {
        ValidateArgumentCount("min", 1, arguments.Count);
        return arguments.Min();
    }

    private static decimal Max(List<decimal> arguments)
    {
        ValidateArgumentCount("max", 1, arguments.Count);
        return arguments.Max();
    }

    private static decimal Sin(List<decimal> arguments)
    {
        ValidateArgumentCount("sin", 1, arguments.Count);
        return (decimal)Math.Sin((double)arguments[0]);
    }

    private static decimal Cos(List<decimal> arguments)
    {
        ValidateArgumentCount("cos", 1, arguments.Count);
        return (decimal)Math.Cos((double)arguments[0]);
    }

    private static decimal Tan(List<decimal> arguments)
    {
        ValidateArgumentCount("tan", 1, arguments.Count);
        return (decimal)Math.Tan((double)arguments[0]);
    }

    private static decimal Round(List<decimal> arguments)
    {
        ValidateArgumentCount("round", 1, arguments.Count);
        return Math.Round(arguments[0]);
    }

    private static decimal Ceil(List<decimal> arguments)
    {
        ValidateArgumentCount("ceil", 1, arguments.Count);
        return Math.Ceiling(arguments[0]);
    }

    private static decimal Floor(List<decimal> arguments)
    {
        ValidateArgumentCount("floor", 1, arguments.Count);
        return Math.Floor(arguments[0]);
    }

    private static void ValidateArgumentCount(string functionName, int expected, int actual)
    {
        if (actual < expected)
        {
            throw new ArgumentException($"Function {functionName} requires at least {expected} arguments, got {actual}");
        }
    }
}