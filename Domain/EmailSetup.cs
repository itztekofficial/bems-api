
namespace Core.DataModel
{
    public class EmailSetup : BaseEntity
    {
        public string? IPAddress { get; set; }
        public string? EmailId { get; set; }
        public string? EmailPWD { get; set; }
        public string? SMTPPort { get; set; }   
    }
}
