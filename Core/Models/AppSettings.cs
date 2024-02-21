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

    public class ConnectionStrings
    {
        public string DBConnection { get; set; }
    }
}