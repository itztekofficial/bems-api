
namespace Core.DataModel
{
    public class EmailTemplate : BaseEntity
    {
        public int MailTypeId  { get; set; }
        public string? MailType { get; set; }
        public string? Template { get; set; }
    }
}
