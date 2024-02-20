namespace Core.Util
{
    /// <summary>
    /// The cache configuration.
    /// </summary>
    public class CacheConfiguration
    {
        public static string Name = "CacheConfiguration";

        /// <summary>
        /// Gets or sets the CacheServer.
        /// </summary>
        public bool UseDistributed { get; set; }

        /// <summary>
        /// Gets or sets the CacheServer.
        /// </summary>
        public string CacheServer { get; set; }

        /// <summary>
        /// Gets or sets the Instance.
        /// </summary>
        public string Instance { get; set; }
    }
}