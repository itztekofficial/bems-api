using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
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
    /// Utility Controller
    /// </summary>
    public class UtilityController : BaseController
    {
        private readonly ILogger<UtilityController> _logger;
        private readonly IUtilityService _utilityService;

        /// <summary>
        /// UtilityController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="utilityService"></param>
        public UtilityController(ILogger<UtilityController> logger, IUtilityService utilityService)
        {
            _logger = logger;
            _utilityService = utilityService;
            _logger.LogInformation("UtilityController Initialized");
        }

        /// <summary>
        /// MasterList
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("MasterList")]
        public async Task<ApiResponse<MasterResponse>> MasterList(UtilityRequest utilityRequest)
        {
            try
            {
                var result = await _utilityService.MasterList(utilityRequest);
                return new ApiResponse<MasterResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilityController", "MasterList", ex.Message);
                return new ApiResponse<MasterResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetStateByCountryId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetStateByCountryId")]
        public async Task<ApiResponse<IEnumerable<CommonDropdown>>> GetStateByCountryId(ValueRequest request)
        {
            try
            {
                var result = await _utilityService.GetStateByCountryId(request.Id);
                return new ApiResponse<IEnumerable<CommonDropdown>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilityController", "GetStateByCountryId", ex.Message);
                return new ApiResponse<IEnumerable<CommonDropdown>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetCityByStateId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetCityByStateId")]
        public async Task<ApiResponse<IEnumerable<CommonDropdown>>> GetCityByStateId(ValueRequest request)
        {
            try
            {
                var result = await _utilityService.GetCityByStateId(request.Id);
                return new ApiResponse<IEnumerable<CommonDropdown>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Log.WriteLog("UtilityController", "GetCityByStateId", ex.Message);
                return new ApiResponse<IEnumerable<CommonDropdown>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }
    }
}