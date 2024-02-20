using Admin.Services;
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
    ///RepositoryTemplateController
    /// </summary>
    public class RepositoryTemplateController : BaseController
    {
        private readonly ILogger<RepositoryTemplateController> _logger;
        private readonly IRepositoryTemplateService _repositoryTemplateService;

        /// <summary>
        /// RepositoryTemplate Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repositoryTemplateService"></param>
        public RepositoryTemplateController(ILogger<RepositoryTemplateController> logger, IRepositoryTemplateService repositoryTemplateService)
        {
            _logger = logger;
            _repositoryTemplateService = repositoryTemplateService;
            _logger.LogInformation("RepositoryTemplateController Initialized");
        }

        /// <summary>
        /// Get All RepositoryTemplate
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<RepositoryTemplate>>> GetAll()
        {
            try
            {
                var res = await _repositoryTemplateService.GetAllAsync();
                return new ApiResponse<IEnumerable<RepositoryTemplate>>()
                {
                    Status = EnumStatus.Success,
                    Data = res,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<RepositoryTemplate>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get RepositoryTemplate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<RepositoryTemplate>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _repositoryTemplateService.GetByIdAsync(request.Id);
                return new ApiResponse<RepositoryTemplate>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<RepositoryTemplate>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="RepositoryTemplate"></param>
        /// <returns></returns>
        [HttpPost, Route("Create"), DisableRequestSizeLimit]
        public async Task<ApiResponse<bool>> Create([FromForm] RepositoryTemplate RepositoryTemplate)
        {
            try
            {
                var folderName = Path.Combine("Resources", "RepositoryTemplateFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (RepositoryTemplate.File != null && RepositoryTemplate.File.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(RepositoryTemplate.File.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using var streamFile = new FileStream(fullPath, FileMode.Create);
                    RepositoryTemplate.File.CopyTo(streamFile);
                    streamFile.Close();
                }

                var result = await _repositoryTemplateService.CreateAsync(RepositoryTemplate);
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
        /// <param name="RepositoryTemplate"></param>
        /// <returns></returns>
        [HttpPost, Route("Update"), DisableRequestSizeLimit]
        public async Task<ApiResponse<bool>> Update([FromForm] RepositoryTemplate RepositoryTemplate)
        {
            try
            {
                var folderName = Path.Combine("Resources", "RepositoryTemplateFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (RepositoryTemplate.File != null && RepositoryTemplate.File.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(RepositoryTemplate.File.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using var streamFile = new FileStream(fullPath, FileMode.Create);
                    RepositoryTemplate.File.CopyTo(streamFile);
                    streamFile.Close();
                }

                var result = await _repositoryTemplateService.UpdateAsync(RepositoryTemplate);
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
                var result = await _repositoryTemplateService.DeleteAsync(request);
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
                var folderName = Path.Combine("Resources", "RepositoryTemplateFiles");
                var pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullPath = Path.Combine(pathToFolder, requestString.Id);

                var fileBytes = System.IO.File.ReadAllBytes(fullPath);
                new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(fullPath), out var contentType);

                return File(fileContents: fileBytes.ToArray(), contentType: contentType ?? "application/octet-stream");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExportTemplate");
                return BadRequest(ex);
            }
        }

    }
}