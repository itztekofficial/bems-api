using Admin.Services.Contracts;
using ClosedXML.Excel;
using Core.DataModel;
using Core.Models;
using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Middlewares;
using System.Data;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// User and Role Controller
    /// </summary>
    public class UserRoleController : BaseController
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IUserRoleService _userRoleService;

        /// <summary>
        /// UserRoleController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRoleService"></param>
        public UserRoleController(ILogger<UserRoleController> logger, IUserRoleService userRoleService)
        {
            _logger = logger;
            _userRoleService = userRoleService;
            _logger.LogInformation("UserRoleController Initialized");
        }

        #region "User"

        /// <summary>
        /// RoleList
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UserList")]
        public async Task<ApiResponse<IEnumerable<UserResponse>>> UserList()
        {
            try
            {
                GetHeaderData();

                var model = new FormParmData()
                {
                    CompanyId = CompanyId
                };
                var result = await _userRoleService.UserList(model);
                return new ApiResponse<IEnumerable<UserResponse>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<UserResponse>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// SaveUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveUser")]
        public async Task<ApiResponse<bool>> SaveUser(User request)
        {
            try
            {
                bool result = await _userRoleService.SaveUser(request);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<ApiResponse<bool>> UpdateUser(User user)
        {
            try
            {
                bool result = await _userRoleService.UpdateUser(user);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteUser")]
        public async Task<ApiResponse<bool>> DeleteUser(DeleteRequest request)
        {
            try
            {
                bool result = await _userRoleService.DeleteUser(request);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        #endregion "User"

        #region "Menu"

        /// <summary>
        /// MenuList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("MenuList")]
        public async Task<ApiResponse<IEnumerable<Menu>>> MenuList()
        {
            try
            {
                var result = await _userRoleService.MenuList();
                return new ApiResponse<IEnumerable<Menu>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<Menu>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        #endregion "Menu"

        #region "RoleMenu"

        /// <summary>
        /// RoleMenuList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RoleMenuList")]
        public async Task<ApiResponse<IEnumerable<RoleMenu>>> RoleMenuList()
        {
            try
            {
                FormParmData formParmData = new();
                var result = await _userRoleService.RoleMenuList(formParmData);
                return new ApiResponse<IEnumerable<RoleMenu>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<RoleMenu>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetRoleMenuById
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRoleMenuById")]
        public async Task<ApiResponse<RoleMenu>> GetRoleMenuById(ValueRequest request)
        {
            try
            {
                var result = await _userRoleService.GetRoleMenuById(request.Id);
                return new ApiResponse<RoleMenu>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<RoleMenu>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// SaveRoleMenu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveRoleMenu")]
        public async Task<ApiResponse<bool>> SaveRoleMenu(RoleMenu request)
        {
            try
            {
                bool result = await _userRoleService.SaveRoleMenu(request);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// UpdateRoleMenu
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateRoleMenu")]
        public async Task<ApiResponse<bool>> UpdateRoleMenu(RoleMenu obj)
        {
            try
            {
                bool result = await _userRoleService.UpdateRoleMenu(obj);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// DeleteRoleMenu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteRoleMenu")]
        public async Task<ApiResponse<bool>> DeleteRoleMenu(DeleteRequest request)
        {
            try
            {
                bool result = await _userRoleService.DeleteRoleMenu(request);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        #endregion "RoleMenu"

        #region "UserMapping"

        /// <summary>
        /// UserMappingList
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UserMappingList")]
        public async Task<ApiResponse<UserMappingResponse>> UserMappingList()
        {
            try
            {
                GetHeaderData();

                var model = new FormParmData()
                {
                    CompanyId = CompanyId
                };

                var result = await _userRoleService.UserMappingList(model);
                return new ApiResponse<UserMappingResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<UserMappingResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetMenuByRoleId
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMenuByRoleId")]
        public async Task<ApiResponse<IEnumerable<Menu>>> GetMenuByRoleId(ValueRequestString request)
        {
            try
            {
                var result = await _userRoleService.GetMenuByRoleId(request.Id);
                return new ApiResponse<IEnumerable<Menu>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<Menu>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetUserMappingById
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserMappingById")]
        public async Task<ApiResponse<UserMapping>> GetUserMappingById(ValueRequest request)
        {
            try
            {
                var result = await _userRoleService.GetUserMappingById(request.Id);
                return new ApiResponse<UserMapping>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<UserMapping>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// SaveUserMapping
        /// </summary>
        /// <param name="userMappings"></param>
        /// <returns></returns>
        [HttpPost, Route("SaveUserMapping")]
        public async Task<ApiResponse<bool>> SaveUserMapping(List<UserMapping> userMappings)
        {
            bool result = false;
            try
            {
                foreach (var item in userMappings)
                {
                    try
                    {
                        result = await _userRoleService.SaveUserMapping(item);
                    }
                    catch { }
                }

                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// UpdateUserMapping
        /// </summary>
        /// <param name="userMappings"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateUserMapping")]
        public async Task<ApiResponse<bool>> UpdateUserMapping(List<UserMapping> userMappings)
        {
            bool result = false;
            try
            {
                foreach (var item in userMappings)
                {
                    try
                    {
                        result = await _userRoleService.UpdateUserMapping(item);
                    }
                    catch { }
                }
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// DeleteUserMapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteUserMapping")]
        public async Task<ApiResponse<bool>> DeleteUserMapping(DeleteRequest request)
        {
            try
            {
                bool result = await _userRoleService.DeleteUserMapping(request);
                return new ApiResponse<bool>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        #endregion "UserMapping"

        /// <summary>
        /// Export User List
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllUserExport")]
        public IActionResult GetAllExport()
        {
            try
            {
                var model = new FormParmData();
                var result = _userRoleService.UserList(model);
                var res = result.Result.ToList();
                List<Department> data = new();
                var ndata = res.Select(x => new {x.Name,x.EmailId,x.MobileNo, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("UserList");
                        worksheet.Range("A1:H1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "Name";
                        worksheet.Cell("B1").Value = "Email Address";
                        worksheet.Cell("C1").Value = "Contact No.";                        
                        worksheet.Cell("D1").Value = "Active";
                        worksheet.Cell("E1").Value = "CreatedBy";
                        worksheet.Cell("F1").Value = "CreatedDate";
                        worksheet.Cell("G1").Value = "UpdatedBy";
                        worksheet.Cell("H1").Value = "UpdatedDate";

                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("B1").Style.Font.Bold = true;
                        worksheet.Cell("C1").Style.Font.Bold = true;
                        worksheet.Cell("D1").Style.Font.Bold = true;
                        worksheet.Cell("E1").Style.Font.Bold = true;
                        worksheet.Cell("F1").Style.Font.Bold = true;
                        worksheet.Cell("G1").Style.Font.Bold = true;
                        worksheet.Cell("H1").Style.Font.Bold = true;

                        worksheet.Cell(2, 1).InsertData(dt.Rows);
                        worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Columns(1, 8).AdjustToContents();

                        worksheet.Range("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("F1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("G1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("H1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        break;
                    }

                    MemoryStream ms = new();
                    workbook.SaveAs(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(
                    fileContents: ms.ToArray(),
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    );
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Export User Menu List
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllUserMenuExport")]
        public IActionResult GetAllUserMenuExport()
        {
            try
            {
                FormParmData formParmData = new();               
                var result = _userRoleService.RoleMenuList(formParmData);
                var res = result.Result.ToList();
                List<Department> data = new();
                var ndata = res.Select(x => new { x.Name, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("RoleList");
                        worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "Role";
                        worksheet.Cell("B1").Value = "Active";
                        worksheet.Cell("C1").Value = "CreatedBy";
                        worksheet.Cell("D1").Value = "CreatedDate";
                        worksheet.Cell("E1").Value = "UpdatedBy";
                        worksheet.Cell("F1").Value = "UpdatedDate";

                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("B1").Style.Font.Bold = true;
                        worksheet.Cell("C1").Style.Font.Bold = true;
                        worksheet.Cell("D1").Style.Font.Bold = true;
                        worksheet.Cell("E1").Style.Font.Bold = true;
                        worksheet.Cell("F1").Style.Font.Bold = true;                        

                        worksheet.Cell(2, 1).InsertData(dt.Rows);
                        worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Columns(1, 8).AdjustToContents();

                        worksheet.Range("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("F1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;                       
                        break;
                    }

                    MemoryStream ms = new();
                    workbook.SaveAs(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(
                    fileContents: ms.ToArray(),
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    );
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Export User Mapping
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllUserMappingExport")]
        public IActionResult GetAllUserMappingExport()
        {
            try
            {
                var model = new FormParmData();
                var result = _userRoleService.UserMappingList(model);
                var res = result.Result.UserMappingList.ToList();
                List<Department> data = new();
                var ndata = res.Select(x => new { x.UserName,x.Roles,x.Departments, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("UserMapping");
                        worksheet.Range("A1:H1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "User";
                        worksheet.Cell("B1").Value = "Assigned Role(s)";
                        worksheet.Cell("C1").Value = "Department";
                        worksheet.Cell("D1").Value = "Active";
                        worksheet.Cell("E1").Value = "CreatedBy";
                        worksheet.Cell("F1").Value = "CreatedDate";
                        worksheet.Cell("G1").Value = "UpdatedBy";
                        worksheet.Cell("H1").Value = "UpdatedDate";

                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("B1").Style.Font.Bold = true;
                        worksheet.Cell("C1").Style.Font.Bold = true;
                        worksheet.Cell("D1").Style.Font.Bold = true;
                        worksheet.Cell("E1").Style.Font.Bold = true;
                        worksheet.Cell("F1").Style.Font.Bold = true;
                        worksheet.Cell("G1").Style.Font.Bold = true;
                        worksheet.Cell("H1").Style.Font.Bold = true;

                        worksheet.Cell(2, 1).InsertData(dt.Rows);
                        worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Columns(1, 8).AdjustToContents();

                        worksheet.Range("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("F1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("G1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("H1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        break;
                    }

                    MemoryStream ms = new();
                    workbook.SaveAs(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(
                    fileContents: ms.ToArray(),
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    );
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}