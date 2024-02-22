using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Main.Services.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middlewares;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// LookUpController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LookUpController : BaseController
    {
        private readonly ILogger<LookUpController> _logger;
        private ILookUpService _lookupService;

        /// <summary>
        /// LookUpController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="lookupService"></param>
        public LookUpController(ILogger<LookUpController> logger, ILookUpService lookupService)
        {
            _logger = logger;
            _lookupService = lookupService;
            _logger.LogInformation("LookUpController Initialized");
        }

        /// <summary>
        /// Get All LookUp
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAll")]
        public async Task<ApiResponse<List<LookUpResponse>>> GetAll()
        {
            try
            {
                var result = await _lookupService.GetAllAsync();
                return new ApiResponse<List<LookUpResponse>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<List<LookUpResponse>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get All LookUp
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllByLookTypeId")]
        public async Task<ApiResponse<List<LookUpResponse>>> GetAllByLookTypeId(ValueRequest request)
        {
            try
            {
                var result = await _lookupService.GetAllByLookTypeIdAsync(request.Id);
                return new ApiResponse<List<LookUpResponse>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<List<LookUpResponse>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get LookUp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<LookUpModel>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _lookupService.GetByIdAsync(request.Id);
                return new ApiResponse<LookUpModel>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<LookUpModel>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="lookUp"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(LookUpModel lookUp)
        {
            try
            {
                var result = await _lookupService.CreateAsync(lookUp);
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
        /// Update
        /// </summary>
        /// <param name="lookUp"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(LookUpModel lookUp)
        {
            try
            {
                var result = await _lookupService.UpdateAsync(lookUp);
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
        /// Delete
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete, Route("Delete")]
        public async Task<ApiResponse<bool>> Delete(DeleteRequest request)
        {
            try
            {
                var result = await _lookupService.DeleteAsync(request);
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

    }
}