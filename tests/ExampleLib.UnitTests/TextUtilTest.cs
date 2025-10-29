using Xunit;

namespace ExampleLib.UnitTests;

public class TextUtilTest
{
    [Fact]
    public void Can_extract_russian_words()
    {
        const string text = """
                            Играют волны — ветер свищет,
                            И мачта гнётся и скрыпит…
                            Увы! он счастия не ищет
                            И не от счастия бежит!
                            """;
        List<string> expected =
        [
            "Играют",
            "волны",
            "ветер",
            "свищет",
            "И",
            "мачта",
            "гнётся",
            "и",
            "скрыпит",
            "Увы",
            "он",
            "счастия",
            "не",
            "ищет",
            "И",
            "не",
            "от",
            "счастия",
            "бежит",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Can_extract_words_with_hyphens()
    {
        const string text = "Что-нибудь да как-нибудь, и +/- что- то ещё";
        List<string> expected =
        [
            "Что-нибудь",
            "да",
            "как-нибудь",
            "и",
            "что",
            "то",
            "ещё",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Can_extract_words_with_apostrophes()
    {
        const string text = "Children's toys and three cats' toys";
        List<string> expected =
        [
            "Children's",
            "toys",
            "and",
            "three",
            "cats'",
            "toys",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Can_extract_words_with_grave_accent()
    {
        const string text = "Children`s toys and three cats` toys, all of''them are green";
        List<string> expected =
        [
            "Children`s",
            "toys",
            "and",
            "three",
            "cats`",
            "toys",
            "all",
            "of'",
            "them",
            "are",
            "green",
        ];

        List<string> actual = TextUtil.ExtractWords(text);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Can_return_correct_value_for_simple_cases()
    {
        Assert.Equal(1, TextUtil.ParseRoman("I"));
        Assert.Equal(5, TextUtil.ParseRoman("V"));
        Assert.Equal(10, TextUtil.ParseRoman("X"));
        Assert.Equal(50, TextUtil.ParseRoman("L"));
        Assert.Equal(100, TextUtil.ParseRoman("C"));
        Assert.Equal(500, TextUtil.ParseRoman("D"));
        Assert.Equal(1000, TextUtil.ParseRoman("M"));
    }

    [Fact]
    public void Can_handle_subtractive_notation()
    {
        Assert.Equal(4, TextUtil.ParseRoman("IV"));
        Assert.Equal(9, TextUtil.ParseRoman("IX"));
        Assert.Equal(40, TextUtil.ParseRoman("XL"));
        Assert.Equal(90, TextUtil.ParseRoman("XC"));
        Assert.Equal(400, TextUtil.ParseRoman("CD"));
        Assert.Equal(900, TextUtil.ParseRoman("CM"));
    }

    [Fact]
    public void Can_parse_complex_roman_numbers()
    {
        Assert.Equal(7, TextUtil.ParseRoman("VII"));
        Assert.Equal(2004, TextUtil.ParseRoman("MMIV"));
    }

    [Fact]
    public void Can_throw_on_empty_string()
    {
        Assert.Throws<ArgumentException>(() => TextUtil.ParseRoman(""));
    }

    [Fact]
    public void Can_throw_on_invalid_characters()
    {
        Assert.Throws<ArgumentException>(() => TextUtil.ParseRoman("IIA"));
        Assert.Throws<ArgumentException>(() => TextUtil.ParseRoman("X1"));
        Assert.Throws<ArgumentException>(() => TextUtil.ParseRoman("V_"));
    }

    [Fact]
    public void Can_throw_on_out_of_range_values()
    {
        Assert.Throws<ArgumentException>(() => TextUtil.ParseRoman(""));
        Assert.Throws<ArgumentException>(() => TextUtil.ParseRoman("MMMCMXCIX"));
        Assert.Throws<ArgumentException>(() => TextUtil.ParseRoman("MMMM"));
    }
}