using Core.Models;

namespace Core.Util
{
    /// <summary>
    /// The config file.
    /// </summary>
    public static class ConfigFile
    {
        public static AppSettings appSettings;
        public static StorageSettings storageSettings;
        public static BusSettings busSettings;
        public static ConnectionStrings connectionStrings;
        public static ElasticConfiguration elasticConfiguration;
        public static object StaticData;

        /// <summary>
        /// Loads the configration.
        /// </summary>
        /// <param name="_appSettings">The _app settings.</param>
        public static void SetAppConfigration(AppSettings _appSettings)
        {
            appSettings = _appSettings;
        }

        /// <summary>
        /// Loads the configration.
        /// </summary>
        /// <param name="_storageSettings">The _Storage settings.</param>
        public static void SetStorageConfigration(StorageSettings _storageSettings)
        {
            storageSettings = _storageSettings;
        }

        /// <summary>
        /// Loads the configration.
        /// </summary>
        /// <param name="_busSettings">The _bus settings.</param>
        public static void SetBusConfigration(BusSettings _busSettings)
        {
            busSettings = _busSettings;
        }

        /// <summary>
        /// Loads the configration.
        /// </summary>
        /// <param name="connectionStrings">The _db Connection settings.</param>
        public static void SetDBConnectionConfigration(ConnectionStrings _connectionStrings)
        {
            connectionStrings = _connectionStrings;
        }

        /// <summary>
        /// Loads the configration.
        /// </summary>
        /// <param name="_elasticConfiguration">The _elastic settings.</param>
        public static void SetElasticConfigration(ElasticConfiguration _elasticConfiguration)
        {
            elasticConfiguration = _elasticConfiguration;
        }

        public static void SetStaticData(object _staticData)
        {
            StaticData = _staticData;
        }
    }
}