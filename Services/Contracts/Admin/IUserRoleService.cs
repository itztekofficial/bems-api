using Core.DataModel;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;

namespace Main.Services.Contracts.Admin
{
    public interface IUserRoleService
    {
        #region "user"

        Task<IEnumerable<UserResponse>> UserList(FormParmData formParmData);

        Task<bool> SaveUser(User obj);

        Task<bool> UpdateUser(User obj);

        Task<bool> DeleteUser(DeleteRequest request);

        #endregion "user"

        #region "Menu"

        Task<IEnumerable<Menu>> MenuList();

        #endregion "Menu"

        #region "RoleMenu"
        Task<IEnumerable<RoleMenu>> RoleMenuList(FormParmData formParmData);

        Task<RoleMenu> GetRoleMenuById(int id);

        Task<bool> SaveRoleMenu(RoleMenu obj);

        Task<bool> UpdateRoleMenu(RoleMenu obj);

        Task<bool> DeleteRoleMenu(DeleteRequest request);

        #endregion "RoleMenu"

        #region "UserMapping"

        Task<UserMappingResponse> UserMappingList(FormParmData formParmData);

        Task<IEnumerable<Menu>> GetMenuByRoleId(string roleIds);

        Task<UserMapping> GetUserMappingById(int id);

        Task<bool> SaveUserMapping(UserMapping obj);

        Task<bool> UpdateUserMapping(UserMapping obj);

        Task<bool> DeleteUserMapping(DeleteRequest request);

        #endregion "UserMapping"
    }
}