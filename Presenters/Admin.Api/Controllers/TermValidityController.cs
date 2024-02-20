﻿using Admin.Services.Contracts;
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
    ///TermValidityController
    /// </summary>
    public class TermValidityController : BaseController
    {
        private readonly ILogger<TermValidityController> _logger;
        private readonly ITermValidityService _termValidityService;

        /// <summary>
        /// TermValidityController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="termvalidityService"></param>
        public TermValidityController(ILogger<TermValidityController> logger, ITermValidityService termvalidityService)
        {
            _logger = logger;
            _termValidityService = termvalidityService;
            _logger.LogInformation("TermValidityController Initialized");
        }

        /// <summary>
        /// Get All TermValidity
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<TermValidity>>> GetAll()
        {
            try
            {
                var res = await _termValidityService.GetAllAsync();
                return new ApiResponse<IEnumerable<TermValidity>>()
                {
                    Status = EnumStatus.Success,
                    Data = res,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<TermValidity>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<TermValidity>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _termValidityService.GetByIdAsync(request.Id);
                return new ApiResponse<TermValidity>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<TermValidity>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="TermValidity"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(TermValidity TermValidity)
        {
            try
            {
                var result = await _termValidityService.CreateAsync(TermValidity);
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
        /// <param name="TermValidity"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(TermValidity TermValidity)
        {
            try
            {
                var result = await _termValidityService.UpdateAsync(TermValidity);
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
                var result = await _termValidityService.DeleteAsync(request);
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
        /// Download TermValidity  
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllExport")]
        public IActionResult GetAllExport()
        {
            try
            {
                var result = _termValidityService.GetAllAsync();
                var res = result.Result.ToList();

                List<TermValidity> data = new();
                var ndata = res.Select(x => new {x.Name, x.Sequence, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();

                DataTable dt = CreateDataTable(ndata);
                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("TermValidity");

                        worksheet.Range("A1:G1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                       // worksheet.Cell("A1").Value = "Code";
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