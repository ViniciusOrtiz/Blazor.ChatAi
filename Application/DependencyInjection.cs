using Application.Contracts.Settings;
using Application.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = AppSettings.FromConfiguration(configuration);
        services.AddSingleton<IAppSettings>(appSettings);

        return services;
    }
}