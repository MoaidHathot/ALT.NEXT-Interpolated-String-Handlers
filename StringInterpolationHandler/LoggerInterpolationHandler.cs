using System.Runtime.CompilerServices;
using System.Text;
using Dumpify;
using Microsoft.Extensions.Logging;

[InterpolatedStringHandler]
public ref struct LoggerInterpolationHanlder
{
    private readonly StringBuilder _builder;
    private readonly bool _isEnabled;
    private readonly PiiScrubber _scrubber = new PiiScrubber();
    public LoggerInterpolationHanlder(int literalLength, int formattedCount, Logger logger, LogLevel level, out bool isEnabled)
    {
        _isEnabled = logger.IsEnabled(level);
        isEnabled = _isEnabled;
        _builder = new StringBuilder(literalLength);
        $"Literal length: {literalLength}, formattedCount: {formattedCount}".Dump();
    }

    public bool AppendLiteral(string s)
    {
        if(_isEnabled is false)
        {
            return false;
        }

        AppendLog(s);
        $"Appended literal '{s}'".Dump();
         return true;
    }

    public bool AppendFormatted<T>((bool pii, T item) t)
    {
        if(_isEnabled is not true)
        {
            return false;
        }

        AppendLog(t.item?.ToString());        
        $"Appended formatted '{t}' of type '{typeof(T)}'".Dump();

        return true;
    }

    public bool AppendFormatted<T>(T t)
    {
        if(_isEnabled is not true)
        {
            return false;
        }

        AppendLog(t?.ToString());
        $"Appended formatted '{t}' of type '{typeof(T)}'".Dump();

        return true;
    }

    private void AppendLog(string? log)
    {
        var message = _scrubber.DetectAndTagPII(log ?? "");
        _builder.Append(message);
    }

    internal string GetLoggingString() => _builder.ToString();
}