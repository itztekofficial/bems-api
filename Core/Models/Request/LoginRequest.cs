namespace Core.Models.Request
{
    public class LoginRequest
    {
        public string userid { get; set; }
        public string password { get; set; }
    }

    public class LoginMultiUserRequest
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class MailRequest
    {
        public string EmailId { get; set; }
        public string UsrName { get; set; }
        public string UsrPwd { get; set; }
    }
}