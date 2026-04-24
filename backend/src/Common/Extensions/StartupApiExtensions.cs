using BIS.Common.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BIS.Common.Extensions;

/// <summary>
/// Extension methods for configuring the standard BIS REST API service stack and middleware pipeline.
/// </summary>
public static class StartupApiExtensions
{
    /// <summary>
    /// Registers the standard BIS API services: Controllers, Swagger, SignalR,
    /// and the <see cref="BISConstants.CORS_POLICY_ALLOW_ALL"/> CORS policy.
    /// </summary>
    public static IServiceCollection AddBISApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddSignalR();
        services.AddCors(options =>
            options.AddPolicy(BISConstants.CORS_POLICY_ALLOW_ALL, policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        return services;
    }

    /// <summary>
    /// Configures the standard BIS REST API middleware pipeline.
    /// In Development, Swagger UI is enabled.
    /// CORS, Authorization, and controller endpoint routing are always applied.
    /// </summary>
    public static WebApplication UseBISApiPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(BISConstants.CORS_POLICY_ALLOW_ALL);
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
