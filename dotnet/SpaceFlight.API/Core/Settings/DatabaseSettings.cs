using SpaceFlight.API.Core.Contracts.Settings;

namespace SpaceFlight.API.Core.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
