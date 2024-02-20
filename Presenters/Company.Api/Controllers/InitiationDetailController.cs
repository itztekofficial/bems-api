using Company.Services.Contracts;
using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Middlewares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Company.Api.Controllers
{
    /// <summary>
    /// InitiationDetailController
    /// </summary>
    public class InitiationDetailController : BaseController
    {
        private readonly ILogger<InitiationDetailController> _logger;
        private readonly IInitiationDetailService _initiationDetailService;

        /// <summary>
        /// Initiation Detail Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="initiationDetailService"></param>
        public InitiationDetailController(ILogger<InitiationDetailController> logger, IInitiationDetailService initiationDetailService)
        {
            _logger = logger;
            _initiationDetailService = initiationDetailService;
            _logger.LogInformation("Initiation Detail Controller Initialized");
        }

        /// <summary>
        /// Get All Initiation Detail
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<InitiationDetail>>> GetAll()
        {
            try
            {
                var result = await _initiationDetailService.GetAllAsync();
                return new ApiResponse<IEnumerable<InitiationDetail>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<InitiationDetail>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get Initiation Detail
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<InitiationResponse>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _initiationDetailService.GetByIdAsync(request.Id);
                return new ApiResponse<InitiationResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<InitiationResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="initiation"></param>
        /// <returns></returns>
        [HttpPost, Route("Create"), DisableRequestSizeLimit]
        public async Task<ApiResponse<string>> Create([FromForm] InitiationDetail initiation)
        {
            try
            {
                var result = await _initiationDetailService.CreateAsync(initiation);
                if (!string.IsNullOrEmpty(result))
                {
                    var folderName = Path.Combine("Resources", "Requests");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName, result);
                    if (!Directory.Exists(pathToSave))
                        Directory.CreateDirectory(pathToSave);

                    if (initiation.PartyProfileSheet != null && initiation.PartyProfileSheet.Length > 0)
                    {
                        string fileNamePartyProfileSheet = ContentDispositionHeaderValue.Parse(initiation.PartyProfileSheet.ContentDisposition).FileName.Trim('"').Replace("/", "");
                        var fullPathPartyProfileSheet = Path.Combine(pathToSave, fileNamePartyProfileSheet);
                        using var streamPartyProfileSheet = new FileStream(fullPathPartyProfileSheet, FileMode.Create);
                        initiation.PartyProfileSheet.CopyTo(streamPartyProfileSheet);
                        streamPartyProfileSheet.Close();
                    }

                    if (initiation.DraftAgreement != null && initiation.DraftAgreement.Length > 0)
                    {
                        string fileNameDraftAgreement = ContentDispositionHeaderValue.Parse(initiation.DraftAgreement.ContentDisposition).FileName.Trim('"').Replace("/", "");
                        var fullPathDraftAgreement = Path.Combine(pathToSave, fileNameDraftAgreement);
                        using var streamDraftAgreement = new FileStream(fullPathDraftAgreement, FileMode.Create);
                        initiation.DraftAgreement.CopyTo(streamDraftAgreement);
                        streamDraftAgreement.Close();
                    }

                    if (initiation.InitiationAttachments != null && initiation.InitiationAttachments.Count > 0)
                    {
                        initiation.InitiationAttachments.ForEach(inputFile =>
                        {
                            if (inputFile.Length > 0)
                            {
                                string fileNameAttachments = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"').Replace("/", "");
                                var fullPathAttachments = Path.Combine(pathToSave, fileNameAttachments);
                                using var streamAttachments = new FileStream(fullPathAttachments, FileMode.Create);
                                inputFile.CopyTo(streamAttachments);
                                streamAttachments.Close();
                            }
                        });
                    }
                }

                return new ApiResponse<string>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<string>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="initiation"></param>
        /// <returns></returns>
        [HttpPost, Route("Update"), DisableRequestSizeLimit]
        public async Task<ApiResponse<bool>> Update([FromForm] InitiationDetail initiation)
        {
            try
            {
                var result = await _initiationDetailService.UpdateAsync(initiation);
                if (result && !string.IsNullOrEmpty(initiation.RefId))
                {
                    var folderName = Path.Combine("Resources", "Requests");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName, initiation.RefId);
                    if (!Directory.Exists(pathToSave))
                        Directory.CreateDirectory(pathToSave);

                    if (initiation.PartyProfileSheet != null && initiation.PartyProfileSheet.Length > 0)
                    {
                        string fileNamePartyProfileSheet = ContentDispositionHeaderValue.Parse(initiation.PartyProfileSheet.ContentDisposition).FileName.Trim('"').Replace("/", "");
                        var fullPathPartyProfileSheet = Path.Combine(pathToSave, fileNamePartyProfileSheet);
                        using var streamPartyProfileSheet = new FileStream(fullPathPartyProfileSheet, FileMode.Create);
                        initiation.PartyProfileSheet.CopyTo(streamPartyProfileSheet);
                        streamPartyProfileSheet.Close();
                    }

                    if (initiation.DraftAgreement != null && initiation.DraftAgreement.Length > 0)
                    {
                        string fileNameDraftAgreement = ContentDispositionHeaderValue.Parse(initiation.DraftAgreement.ContentDisposition).FileName.Trim('"').Replace("/", "");
                        var fullPathDraftAgreement = Path.Combine(pathToSave, fileNameDraftAgreement);
                        using var streamDraftAgreement = new FileStream(fullPathDraftAgreement, FileMode.Create);
                        initiation.DraftAgreement.CopyTo(streamDraftAgreement);
                        streamDraftAgreement.Close();
                    }

                    if (initiation.InitiationAttachments != null && initiation.InitiationAttachments.Count > 0)
                    {
                        initiation.InitiationAttachments.ForEach(inputFile =>
                        {
                            if (inputFile.Length > 0)
                            {
                                string fileNameAttachments = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"').Replace("/", "");
                                var fullPathAttachments = Path.Combine(pathToSave, fileNameAttachments);
                                using var streamAttachments = new FileStream(fullPathAttachments, FileMode.Create);
                                inputFile.CopyTo(streamAttachments);
                                streamAttachments.Close();
                            }
                        });
                    }

                    if (initiation.AttachmentLegalUser != null && initiation.AttachmentLegalUser.Count > 0)
                    {
                        initiation.AttachmentLegalUser.ForEach(inputFile =>
                        {
                            if (inputFile.Length > 0)
                            {
                                string fNameAttachmentLegalUser = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"').Replace("/", "");
                                var fullPathLegalUser = Path.Combine(pathToSave, fNameAttachmentLegalUser);
                                using var streamLegalUser = new FileStream(fullPathLegalUser, FileMode.Create);
                                inputFile.CopyTo(streamLegalUser);
                                streamLegalUser.Close();
                            }
                        });
                    }

                    if (initiation.ReplyAttachment != null && initiation.ReplyAttachment.Count > 0)
                    {
                        initiation.ReplyAttachment.ForEach(inputFile =>
                        {
                            if (inputFile.Length > 0)
                            {
                                string fNameReplyAttachment = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"').Replace("/", "");
                                var fullPathReplyAttachment = Path.Combine(pathToSave, fNameReplyAttachment);
                                using var streamReplyAttachment = new FileStream(fullPathReplyAttachment, FileMode.Create);
                                inputFile.CopyTo(streamReplyAttachment);
                                streamReplyAttachment.Close();
                            }
                        });
                    }

                    if (initiation.PreExecutionAttachments != null && initiation.PreExecutionAttachments.Count > 0)
                    {
                        initiation.PreExecutionAttachments.ForEach(inputFile =>
                        {
                            if (inputFile.Length > 0)
                            {
                                string fNamePreExecutionAttachment = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"').Replace("/", "");
                                var fullPathPreExecutionAttachment = Path.Combine(pathToSave, fNamePreExecutionAttachment);
                                using var streamPreExecutionAttachment = new FileStream(fullPathPreExecutionAttachment, FileMode.Create);
                                inputFile.CopyTo(streamPreExecutionAttachment);
                                streamPreExecutionAttachment.Close();
                            }
                        });
                    }

                    if (initiation.AttachmentExecution != null && initiation.AttachmentExecution.Count > 0)
                    {
                        initiation.AttachmentExecution.ForEach(inputFile =>
                        {
                            if (inputFile.Length > 0)
                            {
                                string fNameAttachmentExecution = ContentDispositionHeaderValue.Parse(inputFile.ContentDisposition).FileName.Trim('"').Replace("/", "");
                                var fullPathAttachmentExecution = Path.Combine(pathToSave, fNameAttachmentExecution);
                                using var streamAttachmentExecution = new FileStream(fullPathAttachmentExecution, FileMode.Create);
                                inputFile.CopyTo(streamAttachmentExecution);
                                streamAttachmentExecution.Close();
                            }
                        });
                    }

                    if (initiation.FinalAgreementFile != null && initiation.FinalAgreementFile.Length > 0)
                    {
                        string fNameFinalAgreementFile = ContentDispositionHeaderValue.Parse(initiation.FinalAgreementFile.ContentDisposition).FileName.Trim('"').Replace("/", "");
                        var fullFinalAgreementFile = Path.Combine(pathToSave, fNameFinalAgreementFile);
                        using var streamFinalAgreementFile = new FileStream(fullFinalAgreementFile, FileMode.Create);
                        initiation.FinalAgreementFile.CopyTo(streamFinalAgreementFile);
                        streamFinalAgreementFile.Close();
                    }
                }
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
                var result = await _initiationDetailService.DeleteAsync(request.Id);
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
        /// UpdateStatus
        /// </summary>
        /// <param name="updateStatus"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateStatus")]
        public async Task<ApiResponse<bool>> UpdateStatus(InitiationUpdateStatus updateStatus)
        {
            try
            {
                var result = await _initiationDetailService.UpdateStatusAsync(updateStatus);
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
        /// UpdateStatus
        /// </summary>
        /// <param name="beforeExecutionModel"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateBeforeExecutionSteps")]
        public async Task<ApiResponse<bool>> UpdateBeforeExecutionStepsAsync(BeforeExecutionModel beforeExecutionModel)
        {
            try
            {
                var result = await _initiationDetailService.UpdateBeforeExecutionStepsAsync(beforeExecutionModel);
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
        /// SharedWithCustomer
        /// </summary>
        /// <param name="sharedWithCustomer"></param>
        /// <returns></returns>
        [HttpPost, Route("SharedWithCustomer")]
        public async Task<ApiResponse<bool>> SharedWithCustomer(SharedWithCustomerModel sharedWithCustomer)
        {
            try
            {
                var result = await _initiationDetailService.SharedWithCustomerAsync(sharedWithCustomer);
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
        /// UpdateStatus
        /// </summary>
        /// <param name="sharedWithLegalAcknowlege"></param>
        /// <returns></returns>
        [HttpPost, Route("SharedWithLegalAcknowlege")]
        public async Task<ApiResponse<bool>> SharedWithLegalAcknowlege(SharedWithLegalAcknowlegeModel sharedWithLegalAcknowlege)
        {
            try
            {
                var result = await _initiationDetailService.SharedWithLegalAcknowlegeAsync(sharedWithLegalAcknowlege);
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
        /// UpdateLegalUser
        /// </summary>
        /// <param name="legaluserModel"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateLegalUser")]
        public async Task<ApiResponse<bool>> UpdateLegalUser(UpdateLegaluserModel legaluserModel)
        {
            try
            {
                var result = await _initiationDetailService.UpdateLegalUserAsync(legaluserModel);
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
        public IActionResult DownloadFile(DownloadRequest requestString)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Requests");
                var pathToFolder = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullPath = Path.Combine(pathToFolder, requestString.InitiationId, requestString.Id);

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