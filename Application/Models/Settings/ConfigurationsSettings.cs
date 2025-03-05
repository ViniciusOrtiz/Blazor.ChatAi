namespace Application.Models.Settings
{
    public class ConfigurationsSettings
    {
        public string DatabaseSchema { get; private set; }
        public string DatabaseMigrationHistoryTable { get; private set; }

        public ConfigurationsSettings(string databaseSchema, string databaseMigrationHistoryTable)
        {
            if (string.IsNullOrWhiteSpace(databaseSchema))
            {
                throw new ArgumentNullException(nameof(databaseSchema), "Cannot be null or empty");
            }
            
            if (string.IsNullOrWhiteSpace(databaseMigrationHistoryTable))
            {
                throw new ArgumentNullException(nameof(databaseMigrationHistoryTable), "Cannot be null or empty");
            }
            
            DatabaseSchema = databaseSchema;
            DatabaseMigrationHistoryTable = databaseMigrationHistoryTable;
        }
    }
}
