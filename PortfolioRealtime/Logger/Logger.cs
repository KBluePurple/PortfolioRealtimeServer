namespace PortfolioRealtime.Logger;

public class Logger
{
    private static void Log(string message, ConsoleColor color)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        Console.WriteLine($"[{timestamp}] {message}");
        Console.ForegroundColor = originalColor;
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