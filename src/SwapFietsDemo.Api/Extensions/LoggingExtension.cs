using Serilog;


namespace SwapFietsDemo.Api.Extensions;

public static class LoggingExtension
{
    public static void AddSerilog(this IServiceCollection services, IConfiguration configuration, string loggerName = "SwapFiets Logger")
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var logger = new LoggerFactory().AddSerilog(Log.Logger).CreateLogger(loggerName);

        services.AddSingleton(logger);
    }
}