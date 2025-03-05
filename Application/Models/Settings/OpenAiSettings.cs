namespace Application.Models.Settings;

public class OpenAiSettings
{
    public string ApiKey { get; private set; }

    public OpenAiSettings(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey), "Cannot be null or empty");
        }
        
        ApiKey = apiKey;
    }
}