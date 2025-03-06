namespace Application.Models.Settings
{
    public class ConnectionStringSettings
    {
        public string DefaultConnection { get; private set; }

        public ConnectionStringSettings(string defaultConnection)
        {
            var environmentConnection = Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING");
            if (!string.IsNullOrWhiteSpace(environmentConnection))
            {
                // try to get the connection string from environment variables
                defaultConnection = environmentConnection;
            }
            
            if (string.IsNullOrWhiteSpace(defaultConnection))
            {
                throw new ArgumentNullException(nameof(defaultConnection), "Cannot be null or empty");
            }
            
            DefaultConnection = defaultConnection;
        }
    }
}
