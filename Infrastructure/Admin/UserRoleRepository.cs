using System.Data;
using System.Data.SqlClient;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using Repositories.Contracts.Admin;




namespace Repositories.Admin
{
    public class UserRoleRepository : IUserRoleRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<UserRoleRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public UserRoleRepository(ILogger<UserRoleRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.Log(LogLevel.Information, "UserRoleRepository => Initialized");
        }
        #endregion

        #region ===[ User Methods ]===========================
        /// <summary>
        /// UserList
        /// </summary>
        /// <param name="formParmData"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserResponse>> UserList(FormParmData formParmData)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getAll");

                return await _sqlConnection.QueryAsync<UserResponse>("usp_User", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "UserList", ex.Message);
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
                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", obj.Id);
                param.Add("CompanyId", obj.CompanyId == 0 ? 1 : obj.CompanyId);
                param.Add("Name", obj.Name);
                param.Add("EmailId", obj.EmailId);
                param.Add("MobileNo", obj.MobileNo);
                param.Add("UserPwd", EncryptDecrypt.Encrypt(obj.UserPwd));
                param.Add("UserType", 2);
                param.Add("IsAdmin", false);
                param.Add("IsPredefined", false);
                param.Add("IsLoggedIn", false);
                param.Add("IsActive", obj.IsActive);
                param.Add("CreatedById", obj.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", obj.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_User", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "SaveUser", ex.Message);
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
                var param = new DynamicParameters();
                param.Add("ActionType", "update");
                param.Add("Id", obj.Id);
                param.Add("CompanyId", obj.CompanyId == 0 ? 1 : obj.CompanyId);
                param.Add("Name", obj.Name);
                param.Add("EmailId", obj.EmailId);
                param.Add("MobileNo", obj.MobileNo);
                param.Add("UserPwd", EncryptDecrypt.Encrypt(obj.UserPwd));
                param.Add("UserType", 2);
                param.Add("IsAdmin", false);
                param.Add("IsPredefined", false);
                param.Add("IsLoggedIn", false);
                param.Add("IsActive", obj.IsActive);
                param.Add("ModifiedById", obj.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_User", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "UpdateUser", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UpdatedById"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(DeleteRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "delete");
                param.Add("Id", request.Id);
                param.Add("IsActive", request.IsActive);
                param.Add("ModifiedById", request.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_User", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "DeleteUser", ex.Message);
                throw;
            }
        }

        #endregion "User"

        #region ===[ Menu Methods ]===========================
        /// <summary>
        /// MenuList
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> MenuList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getAll");

                return await _sqlConnection.QueryAsync<Menu>("usp_Menu", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "MenuList", ex.Message);
                throw;
            }
        }

        #endregion "Menu"

        #region ===[ RoleMenu Methods ]===========================
        /// <summary>
        /// UserMappingList
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RoleMenu>> RoleMenuList(FormParmData formParmData)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getAll");

                return await _sqlConnection.QueryAsync<RoleMenu>("usp_RoleMenu", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "RoleMenuList", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetRoleMenuById
        /// </summary>
        /// <returns></returns>
        public async Task<RoleMenu> GetRoleMenuById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getById");
                param.Add("Id", id);

                return await _sqlConnection.QueryFirstOrDefaultAsync<RoleMenu>("usp_RoleMenu", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "RoleMenuList", ex.Message);
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
                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", obj.Id);
                param.Add("CompanyId", obj.CompanyId == 0 ? 1 : obj.CompanyId);
                param.Add("Name", obj.Name);
                param.Add("MenuIds", obj.MenuIds);
                param.Add("IsActive", obj.IsActive);
                param.Add("IsAdmin", obj.IsAdmin);
                param.Add("IsPredefined", obj.IsPredefined);
                param.Add("IsActive", obj.IsActive);
                param.Add("CreatedById", obj.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", obj.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_RoleMenu", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "RoleMenu", ex.Message);
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
                var param = new DynamicParameters();
                param.Add("ActionType", "update");
                param.Add("Id", obj.Id);
                param.Add("CompanyId", obj.CompanyId == 0 ? 1 : obj.CompanyId);
                param.Add("Name", obj.Name);
                param.Add("MenuIds", obj.MenuIds);
                param.Add("IsActive", obj.IsActive);
                param.Add("IsAdmin", obj.IsAdmin);
                param.Add("IsPredefined", obj.IsPredefined);
                param.Add("ModifiedById", obj.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_RoleMenu", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "UpdateRoleMenu", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// DeleteRoleMenu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UpdatedById"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleMenu(DeleteRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "delete");
                param.Add("Id", request.Id);
                param.Add("IsActive", request.IsActive);
                param.Add("ModifiedById", request.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_RoleMenu", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "DeleteRoleMenu", ex.Message);
                throw;
            }
        }

        #endregion "RoleMenu"

        #region ===[ UserMapping Methods ]===========================
        /// <summary>
        /// UserMappingList
        /// </summary>
        /// <returns></returns>
        public async Task<UserMappingResponse> UserMappingList(FormParmData formParmData)
        {
            try
            {
                UserMappingResponse userMappingResponse = new();
                var param = new DynamicParameters();
                param.Add("ActionType", "getAll");

                var res = await _sqlConnection.QueryMultipleAsync("usp_UserMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                userMappingResponse.UserMappingList = res.Read<UserMappingResponseCore>();
                userMappingResponse.UserList = res.Read<CommonDropdown>();
                userMappingResponse.RoleList = res.Read<CommonDropdown>();
                userMappingResponse.EntityList = res.Read<CommonDropdown>();
                userMappingResponse.DepartmentList = res.Read<CommonDropdown>();
                userMappingResponse.RoleTypeList = res.Read<CommonDropdown>();
                userMappingResponse.MenuList = res.Read<UMMenuResponse>();

                return userMappingResponse;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "UserMappingList", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetMenuByRoleId
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> GetMenuByRoleId(string roleIds)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getMenuByRoleId");
                param.Add("RoleIds", roleIds);

                return await _sqlConnection.QueryAsync<Menu>("usp_UserMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetMenuByRoleId", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetUserMappingById
        /// </summary>
        /// <returns></returns>
        public async Task<UserMapping> GetUserMappingById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "getById");
                param.Add("Id", id);

                return await _sqlConnection.QueryFirstOrDefaultAsync<UserMapping>("usp_UserMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetUserMappingById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// SaveUserMapping
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> SaveUserMapping(UserMapping obj)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "insert");
                param.Add("Id", obj.Id);
                param.Add("CompanyId", obj.CompanyId == 0 ? 1 : obj.CompanyId);
                param.Add("RefUserId", obj.RefUserId);
                param.Add("RoleIds", obj.RoleIds);
                param.Add("EntityIds", obj.EntityIds);
                param.Add("DepartmentIds", obj.DepartmentIds);
                param.Add("RoleTypeId", obj.RoleTypeId);
                param.Add("MenuIds", obj.MenuIds);
                param.Add("IsActive", obj.IsActive);
                param.Add("CreatedById", obj.CreatedById);
                param.Add("CreateDate", DateTime.UtcNow);
                param.Add("ModifiedById", obj.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_UserMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "SaveUserMapping", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// UpdateUserMapping
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserMapping(UserMapping obj)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "update");
                param.Add("Id", obj.Id);
                param.Add("CompanyId", obj.CompanyId == 0 ? 1 : obj.CompanyId);
                param.Add("RefUserId", obj.RefUserId);
                param.Add("RoleIds", obj.RoleIds);
                param.Add("EntityIds", obj.EntityIds);
                param.Add("DepartmentIds", obj.DepartmentIds);
                param.Add("RoleTypeId", obj.RoleTypeId);
                param.Add("MenuIds", obj.MenuIds);
                param.Add("IsActive", obj.IsActive);
                param.Add("ModifiedById", obj.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_UserMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "UpdateUserMapping", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// DeleteUserMapping
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserMapping(DeleteRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "delete");
                param.Add("Id", request.Id);
                param.Add("IsActive", request.IsActive);
                param.Add("ModifiedById", request.UpdatedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_UserMapping", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "DeleteUserMapping", ex.Message);
                throw;
            }
        }

        #endregion "UserMapping"

        #region "usp_Settings"

        /// <summary>
        /// GetUserDetailsById
        /// </summary>
        /// <returns></returns>
        public async Task<UserMappingResponseCore> GetUserDetailsById(int uId)
        {
            try
            {
                UserMappingResponse userMappingResponse = new();
                var param = new DynamicParameters();
                param.Add("ActionType", "getUserDetailsById");
                param.Add("Id", uId);

                return await _sqlConnection.QueryFirstOrDefaultAsync<UserMappingResponseCore>("usp_Settings", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetUserDetailsById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetUserPWDById
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserPWDById(int uId)
        {
            try
            {
                UserMappingResponse userMappingResponse = new();
                var param = new DynamicParameters();
                param.Add("ActionType", "getUserPWDById");
                param.Add("Id", uId);

                var usrPass = await _sqlConnection.QueryFirstOrDefaultAsync<string>("usp_Settings", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                return !string.IsNullOrEmpty(usrPass) ? EncryptDecrypt.Decrypt(usrPass) : string.Empty;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetUserPWDById", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// GetUserPWDById
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateUserPWDById(PWDChangeRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("ActionType", "updateUserPWDById");
                param.Add("Id", request.Id);
                param.Add("UserPwd", EncryptDecrypt.Encrypt(request.UserPwd));
                param.Add("ModifiedById", request.ModifiedById);
                param.Add("ModifyDate", DateTime.UtcNow);

                var res = await _sqlConnection.ExecuteAsync("usp_Settings", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
                _dbTransaction.Commit();
                return res > 0;
            }
            catch (Exception ex)
            {
                Log.WriteLog("UserRoleRepository", "GetUserPWDById", ex.Message);
                throw;
            }
        }

        #endregion "usp_Settings"
    }
}