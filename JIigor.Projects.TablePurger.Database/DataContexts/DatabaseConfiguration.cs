namespace JIigor.Projects.TablePurger.Database.DataContexts
{
    internal class DatabaseConfiguration
    {
        public DatabaseConfiguration(string connectionString, string schema)
        {
            ConnectionString = connectionString;
            Schema = schema;
        }

        public DatabaseConfiguration()
        {
        }

        public string ConnectionString { get; set; }

        public string Schema { get; set; }
    }
}
