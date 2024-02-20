using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Middlewares;
using System.Net.Http.Headers;

namespace Admin.Api.Controllers
{
    /// <summary>
    ///UserHelpFileMappingController
    /// </summary>
    public class UserHelpFileMappingController : BaseController
    {
        private readonly ILogger<UserHelpFileMappingController> _logger;
        private readonly IUserHelpFileMappingService _userHelpFileMappingService;

        /// <summary>
        /// UserHelpFileMappingController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userHelpFileMappingService"></param>
        public UserHelpFileMappingController(ILogger<UserHelpFileMappingController> logger, IUserHelpFileMappingService userHelpFileMappingService)
        {
            _logger = logger;
            _userHelpFileMappingService = userHelpFileMappingService;
            _logger.LogInformation("UserHelpFileMappingController Initialized");
        }

        /// <summary>
        /// Get All UserHelpFileMapping
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<UserHelpFileMapping>>> GetAll()
        {
            try
            {
                var res = await _userHelpFileMappingService.GetAllAsync();
                return new ApiResponse<IEnumerable<UserHelpFileMapping>>()
                {
                    Status = EnumStatus.Success,
                    Data = res,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<UserHelpFileMapping>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get UserHelpFileMapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<UserHelpFileMapping>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _userHelpFileMappingService.GetByIdAsync(request.Id);
                return new ApiResponse<UserHelpFileMapping>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<UserHelpFileMapping>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="UserHelpFileMapping"></param>
        /// <returns></returns>
        [HttpPost, Route("Create"), DisableRequestSizeLimit]
        public async Task<ApiResponse<bool>> Create([FromForm] UserHelpFileMapping UserHelpFileMapping)
        {
            try
            {
                var folderName = Path.Combine("Resources", "HelpFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (UserHelpFileMapping.File != null && UserHelpFileMapping.File.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(UserHelpFileMapping.File.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using var streamFile = new FileStream(fullPath, FileMode.Create);
                    UserHelpFileMapping.File.CopyTo(streamFile);
                    streamFile.Close();
                }

                var result = await _userHelpFileMappingService.CreateAsync(UserHelpFileMapping);
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
        /// <param name="UserHelpFileMapping"></param>
        /// <returns></returns>
        [HttpPost, Route("Update"), DisableRequestSizeLimit]
        public async Task<ApiResponse<bool>> Update([FromForm] UserHelpFileMapping UserHelpFileMapping)
        {
            try
            {
                var folderName = Path.Combine("Resources", "HelpFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (UserHelpFileMapping.File != null && UserHelpFileMapping.File.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(UserHelpFileMapping.File.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using var streamFile = new FileStream(fullPath, FileMode.Create);
                    UserHelpFileMapping.File.CopyTo(streamFile);
                    streamFile.Close();
                }

                var result = await _userHelpFileMappingService.UpdateAsync(UserHelpFileMapping);
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
        public async Task<ApiResponse<bool>> Delete(DeleteRequest request)
        {
            try
            {
                var result = await _userHelpFileMappingService.DeleteAsync(request);
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
        /// DownloadFile
        /// </summary>
        /// <returns></returns>
        [HttpPost("DownloadFile")]
        public IActionResult DownloadFile(ValueRequestString requestString)
        {
            try
            {
                var folderName = Path.Combine("Resources", "HelpFiles");
                var pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullPath = Path.Combine(pathToFolder, requestString.Id);

                byte[]? fileBytes = System.IO.File.ReadAllBytes(fullPath);
                new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(fullPath), out var contentType);

                return File(fileContents: fileBytes.ToArray(), contentType: contentType ?? "application/octet-stream");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExportTemplate");
                return Content("File not availble");
            }
        }

    }
}