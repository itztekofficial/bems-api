namespace Core.DataModel
{
    public class ActivityLog : BaseEntity
    {
        public string? Module { get; set; }
        public string? Action { get; set; }
        public string? Message { get; set; }
    }
}