using System;
using System.Threading.Tasks;
using Core.Models.Response;
using Core.ViewModel;
using Main.Services.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middlewares;

namespace Admin.Api.Controllers
{
    /// <summary>
    ///AdminDashBoardController
    /// </summary>
    public class AdminDashBoardController : BaseController
    {
        private readonly ILogger<AdminDashBoardController> _logger;
        private readonly IAdminDashBoardService _adminDashBoardService;

        /// <summary>
        /// ActivityLogController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="adminDashBoardService"></param>
        public AdminDashBoardController(ILogger<AdminDashBoardController> logger, IAdminDashBoardService adminDashBoardService)
        {
            _logger = logger;
            _adminDashBoardService = adminDashBoardService;
            _logger.LogInformation("AdminDashBoardController Initialized");
        }

        /// <summary>
        /// Get Admin DashBoard
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAdminDashBoard")]
        public async Task<ApiResponse<AdminDashBoardResponse>> GetAdminDashBoard()
        {
            try
            {
                //GetHeaderData();
                //var model = new FormParmData()
                //{
                //    CompanyId = CompanyId
                //};

                var result = await _adminDashBoardService.GetAdminDashBoardData();
                return new ApiResponse<AdminDashBoardResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<AdminDashBoardResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

    }
}