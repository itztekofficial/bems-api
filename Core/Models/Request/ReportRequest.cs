namespace Core.Models.Request;

public class ReportRequest : RequestBase
{
    public int UserId { get; set; }
    public int EntityId { get; set; }
    public int DepartmentId { get; set; }
    public int RoleTypeId { get; set; }
    public string RequestStatusIds { get; set; }
}