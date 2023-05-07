namespace PortfolioRealtime.Logger;

public class Logger
{
    private static readonly FileInfo LogFile = new($"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");
    public static void Initialize()
    {
        LogInfo($"Logging to {LogFile.FullName}.");
    }
    
    private static void Log(string message, ConsoleColor color)
    {
        Task.Run(() => LogAsync(message, color));
    }
    
    private static Task LogAsync(string message, ConsoleColor color)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        Console.WriteLine($"[{timestamp}] {message}");
        Console.ForegroundColor = originalColor;
        
        File.AppendAllText(LogFile.FullName, $"[{timestamp}] {message}\n");
        return Task.CompletedTask;
    }

    public static void LogError(string message)
    {
        Log(message, ConsoleColor.Red);
    }

    public static void LogWarning(string message)
    {
        Log(message, ConsoleColor.Yellow);
    }

    public static void LogInfo(string message)
    {
        Log(message, ConsoleColor.Green);
    }

    public static void LogDebug(string message)
    {
        Log(message, ConsoleColor.Cyan);
    }
}