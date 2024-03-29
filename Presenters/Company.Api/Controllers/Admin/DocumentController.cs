﻿using ClosedXML.Excel;
using Core.DataModel;
using Core.Models.Request;
using Core.ViewModel;
using Main.Services.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middlewares;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// DocumentController
    /// </summary>
    public class DocumentController : BaseController
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentService _DocumentService;

        /// <summary>
        /// DocumentController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="DocumentService"></param>
        public DocumentController(ILogger<DocumentController> logger, IDocumentService DocumentService)
        {
            _logger = logger;
            _DocumentService = DocumentService;
            _logger.LogInformation("DocumentController Initialized");
        }

        /// <summary>
        /// Get All Document
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<Document>>> GetAll()
        {
            try
            {
                var result = await _DocumentService.GetAllAsync();
                return new ApiResponse<IEnumerable<Document>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<Document>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get Document
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<Document>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _DocumentService.GetByIdAsync(request.Id);
                return new ApiResponse<Document>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<Document>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(Document Document)
        {
            try
            {
                var result = await _DocumentService.CreateAsync(Document);
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
        /// <param name="Document"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(Document Document)
        {
            try
            {
                var result = await _DocumentService.UpdateAsync(Document);
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
                var result = await _DocumentService.DeleteAsync(request);
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
        /// Download Document
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllExport")]
        public IActionResult GetAllExport()
        {
            try
            {
                var result = _DocumentService.GetAllAsync();
                var res = result.Result.ToList();
                List<Document> data = new();
                var ndata = res.Select(x => new { x.Name, x.Sequence, Active = x.ActiveStr, x.CreatedBy, 
                    CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("Department");
                        worksheet.Range("A1:G1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "Name";
                        worksheet.Cell("B1").Value = "Sequence";
                        worksheet.Cell("C1").Value = "Active";
                        worksheet.Cell("D1").Value = "CreatedBy";
                        worksheet.Cell("E1").Value = "CreatedDate";
                        worksheet.Cell("F1").Value = "UpdatedBy";
                        worksheet.Cell("G1").Value = "UpdatedDate";

                        // worksheet.Style.Font.SetBold();
                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("B1").Style.Font.Bold = true;
                        worksheet.Cell("C1").Style.Font.Bold = true;
                        worksheet.Cell("D1").Style.Font.Bold = true;
                        worksheet.Cell("E1").Style.Font.Bold = true;
                        worksheet.Cell("F1").Style.Font.Bold = true;
                        worksheet.Cell("G1").Style.Font.Bold = true;

                        worksheet.Cell(2, 1).InsertData(dt.Rows);
                        worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Columns(1, 8).AdjustToContents();

                        worksheet.Range("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("F1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("G1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        break;
                    }

                    MemoryStream ms = new();
                    workbook.SaveAs(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(
                    fileContents: ms.ToArray(),
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    );
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}