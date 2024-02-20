namespace Core.DataModel
{
    public class RoleMenu : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPredefined { get; set; }
        public string? MenuIds { get; set; }
    }
}