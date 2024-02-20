using Core.DataModel;
using System.Collections.Generic;

namespace Core.Models.Response
{
    public class RoleResponse : ResponseBase
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPredefined { get; set; }
    }

    public class UserResponse : BaseEntity
    {
        //public int CompanyId { get; set; }
        //public int UserId { get; set; }

        //public string UserName { get; set; }
        //public string EmailId { get; set; }
        //public string MobileNo { get; set; }
        //public int RoleId { get; set; }
        //public List<Role> Roles { get; set; }
        //public string RoleName { get; set; }
        //public string Password { get; set; }
        //public bool IsActive { get; set; }
        //public int UserType { get; set; }
        //public bool IsAdmin { get; set; }
        //public bool IsPredefined { get; set; }
        //public DateTime? LastUpdated { get; set; }

        //public bool IsLoggedIn { get; set; }
        //public DateTime? LogoutDateTime { get; set; }

        // public int CompanyId { get; set; }
        public string Name { get; set; }

        public string RoleIds { get; set; }
        public string RoleNames { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserPwd { get; set; }
        public int LoginUserType { get; set; }
        public int UserType { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPredefined { get; set; }
        //public bool IsActive { get; set; }
    }

    public class UserMappingResponse
    {
        public IEnumerable<UserMappingResponseCore> UserMappingList { get; set; }
        public IEnumerable<CommonDropdown> UserList { get; set; }
        public IEnumerable<CommonDropdown> RoleList { get; set; }
        public IEnumerable<CommonDropdown> EntityList { get; set; }
        public IEnumerable<CommonDropdown> DepartmentList { get; set; }
        public IEnumerable<CommonDropdown> RoleTypeList { get; set; }
        public IEnumerable<UMMenuResponse> MenuList { get; set; }
    }

    public class UserMappingResponseCore : BaseEntity
    {
        public int RefUserId { get; set; }
        public string UserName { get; set; }
        public string RoleIds { get; set; }
        public string EntityIds { get; set; }
        public string DepartmentIds { get; set; }
        public bool IsExtraMenu { get; set; }
        public string MenuIds { get; set; }

        public string Entitys { get; set; }
        public string Departments { get; set; }
        public string Roles { get; set; }
        public int RoleTypeId { get; set; }
    }

    public class UMMenuResponse : ResponseBase
    {
        public int ParentId { get; set; }
        public string Label { get; set; }
        public bool IsParent { get; set; }
    }

    public class MyProfileResponse
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Entitys { get; set; }
        public string Departments { get; set; }
        public string Roles { get; set; }
        public string DeptHOD { get; set; }
    }
}