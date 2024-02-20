using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Middlewares;

namespace Admin.Api.Controllers
{
    /// <summary>
    ///ActivityLogController
    /// </summary>
    public class ActivityLogController : BaseController
    {
        private readonly ILogger<ActivityLogController> _logger;
        private readonly IActivityLogService _activityLogService;

        /// <summary>
        /// ActivityLogController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="activityLogService"></param>
        public ActivityLogController(ILogger<ActivityLogController> logger, IActivityLogService activityLogService)
        {
            _logger = logger;
            _activityLogService = activityLogService;
            _logger.LogInformation("ActivityLogController Initialized");
        }

        /// <summary>
        /// Get All ActivityLog
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<ActivityLog>>> GetAll()
        {
            try
            {
                var result = await _activityLogService.GetAllAsync();
                return new ApiResponse<IEnumerable<ActivityLog>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<ActivityLog>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get ActivityLog
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<ActivityLog>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _activityLogService.GetByIdAsync(request.Id);
                return new ApiResponse<ActivityLog>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<ActivityLog>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="activityLog"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(ActivityLog activityLog)
        {
            try
            {
                var result = await _activityLogService.CreateAsync(activityLog);
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
        /// <param name="activityLog"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(ActivityLog activityLog)
        {
            try
            {
                var result = await _activityLogService.UpdateAsync(activityLog);
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
        [HttpPost, Route("Delete")]
        public async Task<ApiResponse<bool>> Delete(ValueRequest request)
        {
            try
            {
                var result = await _activityLogService.DeleteAsync(request.Id);
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