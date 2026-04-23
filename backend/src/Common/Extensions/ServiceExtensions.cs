using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BIS.Common.Extensions;

/// <summary>
/// Service extension methods for dependency injection
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Add authentication services to the dependency injection container
    /// </summary>
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add placeholder for auth services - to be implemented
        return services;
    }

    /// <summary>
    /// Add player services to the dependency injection container
    /// </summary>
    public static IServiceCollection AddPlayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add placeholder for player services - to be implemented
        return services;
    }

    /// <summary>
    /// Add world services to the dependency injection container
    /// </summary>
    public static IServiceCollection AddWorldServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add placeholder for world services - to be implemented
        return services;
    }

    /// <summary>
    /// Add chat services to the dependency injection container
    /// </summary>
    public static IServiceCollection AddChatServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add placeholder for chat services - to be implemented
        return services;
    }
}
