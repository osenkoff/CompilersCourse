namespace StarLightLexer;

public class TextScanner
{
    private readonly string _text;
    private int _position;

    public TextScanner(string text)
    {
        _text = text;
        _position = 0;
    }

    public char Peek(int offset = 0)
    {
        int pos = _position + offset;
        return pos < _text.Length ? _text[pos] : '\0';
    }

    public void Advance(int count = 1)
    {
        _position = Math.Min(_position + count, _text.Length);
    }

    public bool IsEnd()
    {
        return _position >= _text.Length;
    }

    public int GetPosition()
    {
        return _position;
    }
}