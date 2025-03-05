namespace Application.Models.Settings;

public class SecuritySettings
{
    public string Key { get; private set; }
    public string Iv { get; private set; }
    
    public SecuritySettings(string key, string iv)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key), "Cannot be null or empty");
        }

        if (string.IsNullOrWhiteSpace(iv))
        {
            throw new ArgumentNullException(nameof(iv), "Cannot be null or empty");
        }

        Key = key;
        Iv = iv;
    }
}