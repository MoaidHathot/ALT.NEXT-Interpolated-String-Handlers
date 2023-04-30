using System.Runtime.CompilerServices;
using System.Text;
using Dumpify;

[InterpolatedStringHandler]
public class LoggerInterpolationHanlder
{
    private readonly StringBuilder _builder;

    public LoggerInterpolationHanlder(int literalLength, int formattedCount)
    {
        _builder = new StringBuilder(literalLength);
        $"\tliteral length: {literalLength}, formattedCount: {formattedCount}".Dump();
    }

    public void AppendLiteral(string s)
    {
        _builder.Append(s);
        "Appended the literal string".Dump();
    }

    public void AppendFormatted<T>(T t)
    {
        _builder.Append(t?.ToString());
        "Appended the formatted object".Dump();
    }

    internal string GetLoggingString() => _builder.ToString();
}
