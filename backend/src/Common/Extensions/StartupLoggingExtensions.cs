using Microsoft.AspNetCore.Builder;
using Serilog;

namespace BIS.Common.Extensions;

/// <summary>
/// Extension methods for configuring Serilog-based logging at application startup.
/// </summary>
public static class StartupLoggingExtensions
{
    /// <summary>
    /// Configures Serilog as the application logger: bootstraps the console logger before host
    /// initialization and wires the full configuration-driven logger into the DI container.
    /// </summary>
    public static WebApplicationBuilder AddBISLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.Services.AddSerilog((_, lc) => lc.ReadFrom.Configuration(builder.Configuration));

        return builder;
    }

    /// <summary>
    /// Runs the application with structured startup and fatal-error logging,
    /// then flushes all Serilog sinks on exit.
    /// </summary>
    /// <param name="app">The built <see cref="WebApplication"/> instance.</param>
    /// <param name="serviceName">The display name written to the start and fatal log entries.</param>
    public static void RunWithLogging(this WebApplication app, string serviceName)
    {
        try
        {
            Log.Information("Starting {ServiceName}...", serviceName);
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{ServiceName} terminated unexpectedly", serviceName);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
