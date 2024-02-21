using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Middlewares;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// StateController
    /// </summary>
    public class EmailTemplateController : BaseController
    {
        private readonly ILogger<EmailTemplateController> _logger;
        private readonly IEmailTemplateService _emailTemplateService;

        /// <summary>
        /// StateController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="emailTemplateService"></param>
        public EmailTemplateController(ILogger<EmailTemplateController> logger, IEmailTemplateService emailTemplateService)
        {
            _logger = logger;
            _emailTemplateService = emailTemplateService;
            _logger.LogInformation("EmailTemplateController Initialized");
        }

        /// <summary>
        /// Get All EmailTemplate
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<EmailTemplate>>> GetAll()
        {
            try
            {
                var result = await _emailTemplateService.GetAllAsync();
                return new ApiResponse<IEnumerable<EmailTemplate>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<EmailTemplate>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get EmailTemplate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<EmailTemplate>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _emailTemplateService.GetByIdAsync(request.Id);
                return new ApiResponse<EmailTemplate>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<EmailTemplate>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(EmailTemplate emailTemplate)
        {
            try
            {
                var result = await _emailTemplateService.CreateAsync(emailTemplate);
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
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(EmailTemplate emailTemplate)
        {
            try
            {
                var result = await _emailTemplateService.UpdateAsync(emailTemplate);
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
                var result = await _emailTemplateService.DeleteAsync(request);
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