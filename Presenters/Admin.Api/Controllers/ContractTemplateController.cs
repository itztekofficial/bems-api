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
    ///ContractTemplateController
    /// </summary>
    public class ContractTemplateController : BaseController
    {
        private readonly ILogger<ContractTemplateController> _logger;
        private readonly IContractTemplateService _contractTemplateService;

        /// <summary>
        /// ContractTemplate Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="contractTemplateService"></param>
        public ContractTemplateController(ILogger<ContractTemplateController> logger, IContractTemplateService contractTemplateService)
        {
            _logger = logger;
            _contractTemplateService = contractTemplateService;
            _logger.LogInformation("ContractTemplateController Initialized");
        }

        /// <summary>
        /// Get All ContractTemplate
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<ContractTemplate>>> GetAll()
        {
            try
            {
                var res = await _contractTemplateService.GetAllAsync();
                return new ApiResponse<IEnumerable<ContractTemplate>>()
                {
                    Status = EnumStatus.Success,
                    Data = res,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<ContractTemplate>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get ContractTemplate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<ContractTemplate>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _contractTemplateService.GetByIdAsync(request.Id);
                return new ApiResponse<ContractTemplate>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<ContractTemplate>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="ContractTemplate"></param>
        /// <returns></returns>
        [HttpPost, Route("Create"), DisableRequestSizeLimit]
        public async Task<ApiResponse<bool>> Create([FromForm] ContractTemplate ContractTemplate)
        {
            try
            {
                var folderName = Path.Combine("Resources", "ContractTemplateFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (ContractTemplate.File != null && ContractTemplate.File.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(ContractTemplate.File.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using var streamFile = new FileStream(fullPath, FileMode.Create);
                    ContractTemplate.File.CopyTo(streamFile);
                    streamFile.Close();
                }

                var result = await _contractTemplateService.CreateAsync(ContractTemplate);
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
        /// <param name="ContractTemplate"></param>
        /// <returns></returns>
        [HttpPost, Route("Update"), DisableRequestSizeLimit]
        public async Task<ApiResponse<bool>> Update([FromForm] ContractTemplate ContractTemplate)
        {
            try
            {
                var folderName = Path.Combine("Resources", "ContractTemplateFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                if (ContractTemplate.File != null && ContractTemplate.File.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(ContractTemplate.File.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using var streamFile = new FileStream(fullPath, FileMode.Create);
                    ContractTemplate.File.CopyTo(streamFile);
                    streamFile.Close();
                }

                var result = await _contractTemplateService.UpdateAsync(ContractTemplate);
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
                var result = await _contractTemplateService.DeleteAsync(request);
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
                var folderName = Path.Combine("Resources", "ContractTemplateFiles");
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