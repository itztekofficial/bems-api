namespace Core.Models.Request
{
    public class RequestBase
    {
        public int Id { get; set; }
    }

    public class ValueRequest
    {
        public int Id { get; set; }
    }

    public class DeleteRequest
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedById { get; set; } = 1;
    }

    public class ValueRequestString
    {
        public string Id { get; set; }
    }

    public class TypeValueInput
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class UtilityRequest
    {
        public string EntityId { get; set; }
        public string DepartmentId { get; set; }
    }

    public class CommonRequest
    {
        public int UserId { get; set; }
        public int EntityId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleTypeId { get; set; }
        public string RequestStatusIds { get; set; }
    }

    public class DownloadRequest
    {
        public string InitiationId { get; set; }
        public string Id { get; set; }
    }
}