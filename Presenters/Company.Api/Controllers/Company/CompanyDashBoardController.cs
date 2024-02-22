using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Main.Services.Contracts.Company;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middlewares;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Api.Controllers
{
    /// <summary>
    ///CompanyDashBoardController
    /// </summary>
    public class CompanyDashBoardController : BaseController
    {
        private readonly ILogger<CompanyDashBoardController> _logger;
        private readonly ICompanyDashBoardService _companyDashBoardService;

        /// <summary>
        /// ActivityLogController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="companyDashBoardService"></param>
        public CompanyDashBoardController(ILogger<CompanyDashBoardController> logger, ICompanyDashBoardService companyDashBoardService)
        {
            _logger = logger;
            _companyDashBoardService = companyDashBoardService;
            _logger.LogInformation("CompanyDashBoardController Initialized");
        }

        /// <summary>
        /// Get Company DashBoard
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetCompanyDashBoard")]
        public async Task<ApiResponse<CompanyDashBoardResponse>> GetCompanyDashBoard(CommonRequest request)
        {
            try
            {
                var result = await _companyDashBoardService.GetCompanyDashBoardData(request);
                return new ApiResponse<CompanyDashBoardResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<CompanyDashBoardResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get Request History
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetRequestHistory")]
        public async Task<ApiResponse<IEnumerable<RequestHistoryResponse>>> GetRequestHistory(ValueRequestString request)
        {
            try
            {
                var result = await _companyDashBoardService.GetRequestHistory(request.Id);
                return new ApiResponse<IEnumerable<RequestHistoryResponse>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<RequestHistoryResponse>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }
    }
}