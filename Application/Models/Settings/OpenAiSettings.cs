namespace Application.Models.Settings;

public class OpenAiSettings
{
    public string ApiKey { get; private set; }

    public OpenAiSettings(string apiKey)
    {
        var environmentApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (!string.IsNullOrWhiteSpace(environmentApiKey))
        {
            // try to get the connection string from environment variables
            apiKey = environmentApiKey;
        }
        
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey), "Cannot be null or empty");
        }
        
        ApiKey = apiKey;
    }
}