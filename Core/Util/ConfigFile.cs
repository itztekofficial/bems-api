using Core.Models;

namespace Core.Util
{
    /// <summary>
    /// The config file.
    /// </summary>
    public static class ConfigFile
    {
        public static AppSettings appSettings;
        public static ConnectionStrings connectionStrings;
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
        /// <param name="connectionStrings">The _db Connection settings.</param>
        public static void SetDBConnectionConfigration(ConnectionStrings _connectionStrings)
        {
            connectionStrings = _connectionStrings;
        }

        public static void SetStaticData(object _staticData)
        {
            StaticData = _staticData;
        }
    }
}