namespace Application.Models.Settings
{
    public class ConnectionStringSettings
    {
        public string DefaultConnection { get; private set; }

        public ConnectionStringSettings(string defaultConnection)
        {
            if (string.IsNullOrWhiteSpace(defaultConnection))
            {
                throw new ArgumentNullException(nameof(defaultConnection), "Cannot be null or empty");
            }
            
            DefaultConnection = defaultConnection;
        }
    }
}
