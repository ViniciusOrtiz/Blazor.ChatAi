using Application.Models.Settings;

namespace Application.Contracts.Settings
{
    public interface IAppSettings
    {
        public ConnectionStringSettings ConnectionStringSettings { get; }
        public ConfigurationsSettings ConfigurationsSettings { get; }
        public OpenAiSettings OpenAiSettings { get; }
    }
}
