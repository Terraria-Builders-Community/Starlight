namespace Starlight.Database
{
    /// <summary>
    ///     Represents the configuration for a mongo database.
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        ///     The connection string for the mongo instance.
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        ///     The database to use.
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;
    }
}
