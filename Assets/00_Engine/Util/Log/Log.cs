public static class Log
{
    private static ILogger _logger = new DefaultLogger();

    public static void SetLogger(ILogger logger) => _logger = logger;

    public static void Info(string msg) => _logger.Log(msg);
    public static void Warn(string msg) => _logger.LogWarning(msg);
    public static void Error(string msg) => _logger.LogError(msg);
}