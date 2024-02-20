using Admin.Services.Contracts;
using ClosedXML.Excel;
using Core.DataModel;
using Core.Models.Request;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Middlewares;
using System.Data;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// EmailSetupController
    /// </summary>
    public class EmailSetupController : BaseController
    {
        private readonly ILogger<EmailSetupController> _logger;
        private readonly IEmailSetupService _emailSetupService;

        /// <summary>
        /// StateController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="emailSetupService"></param>
        public EmailSetupController(ILogger<EmailSetupController> logger, IEmailSetupService emailSetupService)
        {
            _logger = logger;
            _emailSetupService = emailSetupService;
            _logger.LogInformation("EmailSetupController Initialized");
        }

        /// <summary>
        /// Get All EmailSetup
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<EmailSetup>>> GetAll()
        {
            try
            {
                var result = await _emailSetupService.GetAllAsync();
                return new ApiResponse<IEnumerable<EmailSetup>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<EmailSetup>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get EmailSetup
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<EmailSetup>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _emailSetupService.GetByIdAsync(request.Id);
                return new ApiResponse<EmailSetup>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<EmailSetup>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="emailSetup"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(EmailSetup emailSetup)
        {
            try
            {
                var result = await _emailSetupService.CreateAsync(emailSetup);
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
        /// <param name="emailSetup"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(EmailSetup emailSetup)
        {
            try
            {
                var result = await _emailSetupService.UpdateAsync(emailSetup);
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
        /// 
        [HttpPost, Route("Delete")]
        public async Task<ApiResponse<bool>> Delete(DeleteRequest request)
        {
            try
            {
                var result = await _emailSetupService.DeleteAsync(request);
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