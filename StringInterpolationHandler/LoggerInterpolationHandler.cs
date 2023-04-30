using System.Runtime.CompilerServices;
using System.Text;
using Dumpify;
using Microsoft.Extensions.Logging;

[InterpolatedStringHandler]
public class LoggerInterpolationHanlder
{
    private readonly StringBuilder _scrubbedBuilder;
    private readonly StringBuilder _nonScrubbedBuilder;
    private readonly bool _enabled;
    private readonly PiiScrubber _scrubber = new PiiScrubber();

    public LoggerInterpolationHanlder(int literalLength, int formattedCount, Logger logger, LogLevel logLevel)
    {
        _enabled = logger.IsEnabled(logLevel);

        _scrubbedBuilder = new StringBuilder(literalLength);
        _nonScrubbedBuilder = new StringBuilder(literalLength);

        $"literal length: {literalLength}, formattedCount: {formattedCount}".Dump();
    }

    public bool AppendLiteral(string s)
    {
        if (_enabled is false)
        {
            return false;
        }

        var scrubbed = Scrub(s);
        _scrubbedBuilder.Append(scrubbed);
        _nonScrubbedBuilder.Append(s);
        "Appended the literal string".Dump();

        return true;
    }

    public bool AppendFormatted<T>(T t)
    {
        if (_enabled is false)
        {
            return false;
        }

        var scrubbed = Scrub(t?.ToString());
        _scrubbedBuilder.Append(scrubbed);
        _nonScrubbedBuilder.Append(t?.ToString());
        "Appended the formatted object".Dump();

        return true;
    }    

    public bool AppendFormatted<T>((bool pii, T item) pair)
    {
        if (_enabled is false)
        {
            return false;
        }
        
        var message = pair.item?.ToString();
        var scrubbed = pair.pii switch
        {
            true => "*****",
            false => Scrub(message),
        };

        _scrubbedBuilder.Append(scrubbed);
        _nonScrubbedBuilder.Append(message);

        "Appended the formatted object".Dump();

        return true;
    }    
    private string Scrub(string? message)
    {
        return message switch
        {
            null => "",
            string str => _scrubber.DetectAndTagPII(message)
        };
    }

    internal string GetLoggingString() => _scrubbedBuilder.ToString();
}