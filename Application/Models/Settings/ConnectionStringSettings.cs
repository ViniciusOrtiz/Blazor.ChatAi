namespace Application.Models.Settings
{
    public class ConnectionStringSettings
    {
        public string DefaultConnection { get; private set; }

        public ConnectionStringSettings(string defaultConnection)
        {
            if (string.IsNullOrWhiteSpace(defaultConnection))
            {
                // try to get the connection string from environment variables
                defaultConnection = Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING");
            }
            
            if (string.IsNullOrWhiteSpace(defaultConnection))
            {
                throw new ArgumentNullException(nameof(defaultConnection), "Cannot be null or empty");
            }
            
            DefaultConnection = defaultConnection;
        }
    }
}
