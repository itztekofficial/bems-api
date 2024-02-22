namespace Core.Models.Request;

public class NotificationRequest : RequestBase
{
    public string ActionType { get; set; }
    public int EntityId { get; set; }
    public int DepartmentId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int UserId { get; set; }
    public int RoleTypeId { get; set; }
}