using Core.Enums;

namespace Core.Models
{
    public class AppSettings
    {
        public float APIVersion { get; set; }
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public EnmUseQueue UseQueue { get; set; }
        public bool IsElectron { get; set; }
        public string NoReplyMail { get; set; }
        public string SupportMail { get; set; }
        public string ComapnyLogo { get; set; }
        public float DbVersion { get; set; }
    }

    public class StorageSettings
    {
        public string StorageType { get; set; }
        public string StorageConnection { get; set; }
        public string StorageFolder { get; set; }
    }

    public class BusSettings
    {
        public string QueueType { get; set; }
        public string ServiceBusConnection { get; set; }
        public string ServiceBusLogConnection { get; set; }
        public string ServiceBusNotificationConnection { get; set; }
    }

    public class ConnectionStrings
    {
        public string DBConnection { get; set; }
    }

    public class ElasticConfiguration
    {
        public static string Name = "ElasticConfiguration";
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class CacheConfiguration
    {
        public static string Name = "CacheConfiguration";
        public bool UseDistributed { get; set; }
        public string CacheServer { get; set; }
        public string Instance { get; set; }
    }
}