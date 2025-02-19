namespace APICatalago.Logging;

public class CustomLogger : ILogger
{
    readonly string loggerName;
    private readonly CustomLoggerProviderConfiguration loggerConfig;
    private ILogger _loggerImplementation;

    public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        this.loggerName = loggerName;
        this.loggerConfig = loggerConfig;
    }

    public bool IsEnabled(LogLevel level)
    {
        return loggerConfig.LogLevel >= level;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                                        Exception exception, Func<TState, Exception?, string> formatter)
    {
        string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        writeTextFile(message);
    }

    private void writeTextFile(string message)
    {
        string filePath = @"C:\Users\WEBSTER\Documents\CursoC#\Loggers\CustomLogger.txt";

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            try
            {
                writer.WriteLine(message);
                writer.Close();

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

//Define os métodos necessários para registrar a mensagens de log