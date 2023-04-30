using Dumpify;
using Microsoft.Extensions.Logging;

public class Logger
{
    private readonly LogLevel _logLevel;

    public Logger(LogLevel logLevel)
    {
        _logLevel = logLevel;
    }
    
    public bool IsEnabled(LogLevel level)
        => level >= _logLevel;

    public void Log(LogLevel level, string message)
    {
        if(IsEnabled(level))
        {
            Output(message);
        }
    }

    private void Output(string message)
        => message.Dump();
}