using Application.Contracts.Settings;
using Microsoft.Extensions.Configuration;

namespace Application.Models.Settings
{
    public class AppSettings : IAppSettings
    {
        public ConnectionStringSettings ConnectionStringSettings { get; set; } = null!;
        public ConfigurationsSettings ConfigurationsSettings { get; set; } = null!;
        public OpenAiSettings OpenAiSettings { get; set; } = null!;

        public static AppSettings FromConfiguration(IConfiguration configuration)
        {
            var appSettings = new AppSettings
            {
                ConnectionStringSettings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringSettings>() ?? throw new ArgumentNullException(nameof(ConnectionStringSettings), "Cannot be null or empty"),
                ConfigurationsSettings = configuration.GetSection("ConfigurationsSettings").Get<ConfigurationsSettings>() ?? throw new ArgumentNullException(nameof(ConfigurationsSettings), "Cannot be null or empty"),
                OpenAiSettings = configuration.GetSection("OpenAiSettings").Get<OpenAiSettings>() ?? throw new ArgumentNullException(nameof(OpenAiSettings), "Cannot be null or empty"),
            };

            return appSettings;
        }
    }
}
