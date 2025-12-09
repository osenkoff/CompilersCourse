namespace StarLightLexer;
#pragma warning disable SA1602 // Enumeration items should be documented
public enum TokenType
{
    ZVEZDA,
    ZAKRYTAYA_ZVEZDA,
    SVET,
    KONSTELLATSIYA,
    IZLUCHAT,
    PRIEM_SIGNALA,
    FOTON,
    VERNUT,
    ESLI,
    ILI_NET,
    ORBITA,
    SPEKTR,

    KVAZAR,
    NOVA,
    VAKUUM,
    GALAKTIKA,

    ISTINA,
    LOZH,

    IDENTIFIER,
    NUMERIC_LITERAL,

    PLUS,
    MINUS,
    MULTIPLY,
    DIVIDE,
    MODULO,
    EXPONENTIATION,

    EQUALS,
    NOT_EQUALS,
    GREATER_THAN,
    GREATER_OR_EQUAL,
    LESS_THAN,
    LESS_OR_EQUAL,

    AND,
    OR,
    NOT,

    ASSIGN,
    PLUS_ASSIGN,
    MINUS_ASSIGN,
    MULTIPLY_ASSIGN,
    DIVIDE_ASSIGN,
    EXPONENTIATION_ASSIGN,

    SEMICOLON,
    COMMA,
    COLON,
    OPEN_PARENTHESIS,
    CLOSE_PARENTHESIS,
    OPEN_BRACKET,
    CLOSE_BRACKET,
    OPEN_BRACE,
    CLOSE_BRACE,

    END_OF_FILE,
    ERROR,
}
#pragma warning restore SA1602 // Enumeration items should be documented