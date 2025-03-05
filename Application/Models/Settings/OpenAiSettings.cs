namespace Application.Models.Settings;

public class OpenAiSettings
{
    public string ApiKey { get; private set; }

    public OpenAiSettings(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            // try to get the API key from environment variables
            apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        }
        
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey), "Cannot be null or empty");
        }
        
        ApiKey = apiKey;
    }
}