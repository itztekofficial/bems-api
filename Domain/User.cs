namespace Core.DataModel
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? UserPwd { get; set; }
        public int UserType { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPredefined { get; set; }
        public bool IsLoggedIn { get; set; }
        public DateTime? LogoutDateTime { get; set; }
    }

    public class UserWithRole : User  //Only use in UserDetailById
    {
        public string? EntityId { get; set; }
        public string? RoleId { get; set; }
        public string? DepartmentId { get; set; }
    }
}