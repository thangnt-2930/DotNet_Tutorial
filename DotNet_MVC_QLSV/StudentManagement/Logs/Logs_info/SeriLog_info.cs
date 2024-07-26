using Serilog;

public static class SeriLogInfo
{
    public static void Configure()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-create_student.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}