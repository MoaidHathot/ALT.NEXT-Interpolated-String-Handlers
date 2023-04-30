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
        $"Literal length: {literalLength}, formattedCount: {formattedCount}".Dump();
    }

    public void AppendLiteral(string s)
    {
        AppendLog(s);
        $"Appended literal '{s}'".Dump();
    }

    public void AppendFormatted<T>(T t)
    {
        AppendLog(t?.ToString());
        $"Appended formatted '{t}' of type '{typeof(T)}'".Dump();
    }

    private void AppendLog(string? log)
    {
        _builder.Append(log);
    }

    internal string GetLoggingString() => _builder.ToString();
}