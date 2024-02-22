using Core.DataModel;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
using Microsoft.Extensions.Logging;

namespace Main.Services.Admin
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogger<IUserRoleService> _logger;

        public UserRoleService(IUserRoleRepository userRoleRepository, ILogger<IUserRoleService> logger)
        {
            _userRoleRepository = userRoleRepository;
            _logger = logger;
            _logger.Log(LogLevel.Information, "UserRoleService => Initialized");
        }

        #region "User"

        public async Task<IEnumerable<UserResponse>> UserList(FormParmData formParmData)
        {
            try
            {
                var userResponses = await _userRoleRepository.UserList(formParmData);
                _ = userResponses.All(x =>
                {
                    x.UserPwd = EncryptDecrypt.Decrypt(x.UserPwd);
                    return true;
                });
                return userResponses;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "UserList", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// SaveUser
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> SaveUser(User obj)
        {
            try
            {
                return await _userRoleRepository.SaveUser(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "SaveUser", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(User obj)
        {
            try
            {
                return await _userRoleRepository.UpdateUser(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "UpdateUser", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(DeleteRequest request)
        {
            try
            {
                return await _userRoleRepository.DeleteUser(request);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "DeleteUser", ex.Message);
                throw;
            }
        }

        #endregion "User"

        #region "Menu"

        /// <summary>
        /// MenuList
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> MenuList()
        {
            try
            {
                return await _userRoleRepository.MenuList();
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "UserList", ex.Message);
                throw;
            }
        }

        #endregion "Menu"

        #region "RoleMenu"

        /// <summary>
        /// RoleMenuList
        /// </summary>
        /// <param name="formParmData"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleMenu>> RoleMenuList(FormParmData formParmData)
        {
            try
            {
                return await _userRoleRepository.RoleMenuList(formParmData);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "RoleMenuList", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetRoleMenuById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RoleMenu> GetRoleMenuById(int id)
        {
            try
            {
                return await _userRoleRepository.GetRoleMenuById(id);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "GetRoleMenuById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// SaveRoleMenu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> SaveRoleMenu(RoleMenu obj)
        {
            try
            {
                return await _userRoleRepository.SaveRoleMenu(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "SaveRoleMenu", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// UpdateRoleMenu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleMenu(RoleMenu obj)
        {
            try
            {
                return await _userRoleRepository.UpdateRoleMenu(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "UpdateRoleMenu", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// DeleteRoleMenu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleMenu(DeleteRequest request)
        {
            try
            {
                return await _userRoleRepository.DeleteRoleMenu(request);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "DeleteRoleMenu", ex.Message);
                throw;
            }
        }

        #endregion "RoleMenu"

        #region "UserMapping"

        /// <summary>
        /// UserMappingList
        /// </summary>
        /// <param name="formParmData"></param>
        /// <returns></returns>
        public async Task<UserMappingResponse> UserMappingList(FormParmData formParmData)
        {
            try
            {
                return await _userRoleRepository.UserMappingList(formParmData);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "UserMappingList", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetMenuByRoleId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> GetMenuByRoleId(string roleIds)
        {
            try
            {
                return await _userRoleRepository.GetMenuByRoleId(roleIds);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "GetMenuByRoleId", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetUserMappingById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserMapping> GetUserMappingById(int Id)
        {
            try
            {
                return await _userRoleRepository.GetUserMappingById(Id);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "GetUserMappingById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// SaveUserMapping
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> SaveUserMapping(UserMapping obj)
        {
            try
            {
                return await _userRoleRepository.SaveUserMapping(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "SaveUserMapping", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// UpdateUserMapping
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserMapping(UserMapping obj)
        {
            try
            {
                return await _userRoleRepository.UpdateUserMapping(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "UpdateUserMapping", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// DeleteUserMapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserMapping(DeleteRequest request)
        {
            try
            {
                return await _userRoleRepository.DeleteUserMapping(request);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleService", "DeleteUserMapping", ex.Message);
                throw;
            }
        }

        #endregion "UserMapping"
    }
}