namespace StarLightLexer;

public static class LexicalStats
{
    public static string CollectFromFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File not found: {path}");
        }

        string code = File.ReadAllText(path);
        return CollectFromCode(code);
    }

    public static string CollectFromCode(string code)
    {
        Dictionary<string, int> stats = new Dictionary<string, int>
        {
            ["keywords"] = 0,
            ["identifier"] = 0,
            ["number literals"] = 0,
            ["operators"] = 0,
            ["other lexemes"] = 0,
        };

        Lexer lexer = new(code);
        Token token;

        do
        {
            token = lexer.ParseToken();
            if (token.Type == TokenType.END_OF_FILE)
            {
                break;
            }

            string category = GetTokenCategory(token.Type);
            stats[category]++;
        }
        while (token.Type != TokenType.END_OF_FILE);

        return FormatStats(stats);
    }

    private static string GetTokenCategory(TokenType type)
    {
        return type switch
        {
            TokenType.ZVEZDA or TokenType.ZAKRYTAYA_ZVEZDA or TokenType.SVET or
            TokenType.KONSTELLATSIYA or TokenType.IZLUCHAT or TokenType.PRIEM_SIGNALA or
            TokenType.FOTON or TokenType.VERNUT or TokenType.ESLI or TokenType.ILI_NET or
            TokenType.ORBITA or TokenType.SPEKTR or TokenType.KVAZAR or TokenType.NOVA or
            TokenType.VAKUUM or TokenType.GALAKTIKA or TokenType.ISTINA or TokenType.LOZH
                => "keywords",

            TokenType.IDENTIFIER => "identifier",

            TokenType.NUMERIC_LITERAL => "number literals",

            TokenType.PLUS or TokenType.MINUS or TokenType.MULTIPLY or TokenType.DIVIDE or
            TokenType.MODULO or TokenType.EXPONENTIATION or TokenType.EQUALS or
            TokenType.NOT_EQUALS or TokenType.GREATER_THAN or TokenType.GREATER_OR_EQUAL or
            TokenType.LESS_THAN or TokenType.LESS_OR_EQUAL or TokenType.AND or TokenType.OR or
            TokenType.NOT or TokenType.ASSIGN or TokenType.PLUS_ASSIGN or TokenType.MINUS_ASSIGN or
            TokenType.MULTIPLY_ASSIGN or TokenType.DIVIDE_ASSIGN or TokenType.EXPONENTIATION_ASSIGN
                => "operators",

            _ => "other lexemes"
        };
    }

    private static string FormatStats(Dictionary<string, int> stats)
    {
        List<string> lines = new List<string>
        {
            $"keywords: {stats["keywords"]}",
            $"identifier: {stats["identifier"]}",
            $"number literals: {stats["number literals"]}",
            $"operators: {stats["operators"]}",
            $"other lexemes: {stats["other lexemes"]}",
        };

        return string.Join(Environment.NewLine, lines);
    }
}