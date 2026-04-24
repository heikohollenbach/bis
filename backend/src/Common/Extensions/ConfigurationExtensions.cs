using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace BIS.Common.Extensions;

/// <summary>
/// Extension methods for configuration loading.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Inserts the shared <c>appsettings.Base.json</c> (shipped by BIS.Common) as the
    /// lowest-priority configuration source so that every service-specific
    /// <c>appsettings.json</c> and <c>appsettings.{Environment}.json</c> can override it.
    /// </summary>
    public static IConfigurationManager AddBaseConfiguration(this IConfigurationManager configuration)
    {
        var assemblyDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)
                          ?? AppContext.BaseDirectory;

        configuration.Sources.Insert(0, new JsonConfigurationSource
        {
            FileProvider = new PhysicalFileProvider(assemblyDir),
            Path = "appsettings.Base.json",
            Optional = false,
            ReloadOnChange = true
        });

        return configuration;
    }
}
