using Core.DataModel;
using System.Collections.Generic;

namespace Core.Models.Response
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public int UserType { get; set; }
    }

    public class LoginDetailResponse
    {
        public UserDetail UserData { get; set; }
        public CompanyResponse CompanyData { get; set; }
        public List<Menu> MenuData { get; set; }
        public string Token { get; set; }
    }

    public class UserDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int UserType { get; set; }
        public string RoleId { get; set; }
        public string DepartmentId { get; set; }
        public string EntityId { get; set; }
    }

    public class ValidateUserMultiRoleResponse
    {
        public UserMultiRoleResponse UserMultiRoleResponse { get; set; }
        public IEnumerable<CommonDropdown> EntityList { get; set; }
        public IEnumerable<CommonDropdown> DepartmentList { get; set; }
    }

    public class UserMultiRoleResponse
    {
        public bool IsMultiEntiry { get; set; }
        public bool IsMultiDepartment { get; set; }
        public int RoleTypeId { get; set; }
    }

    public class ForgotPassword
    {
        public string Name { get; set; }
        public string UserPwd { get; set; }
    }

    public class ForgotPasswordResponse : ForgotPassword
    {
        public string RandomCode { get; set; }
    }

}