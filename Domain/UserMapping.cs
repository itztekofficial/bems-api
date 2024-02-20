namespace Core.DataModel
{
    public class UserMapping : BaseEntity
    {
        public int RefUserId { get; set; }
        public string? RoleIds { get; set; }
        public string? EntityIds { get; set; }
        public string? DepartmentIds { get; set; }
        public int RoleTypeId { get; set; }
        public string? MenuIds { get; set; }
    }
}