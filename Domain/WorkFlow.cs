namespace Core.DataModel
{
    public class WorkFlow : BaseEntity
    {
        public int EntityId { get; set; }
        public int RequestStatusId { get; set; }
        public int RoleTypeId { get; set; }
        public string? ApproverRoleTypeId { get; set; }
        public int Sequence { get; set; }
    }
}