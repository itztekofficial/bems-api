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
    /// StateController
    /// </summary>
    public class StateController : BaseController
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;

        /// <summary>
        /// StateController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="stateService"></param>
        public StateController(ILogger<StateController> logger, IStateService stateService)
        {
            _logger = logger;
            _stateService = stateService;
            _logger.LogInformation("StateController Initialized");
        }

        /// <summary>
        /// Get All State
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<State>>> GetAll()
        {
            try
            {
                var result = await _stateService.GetAllAsync();
                return new ApiResponse<IEnumerable<State>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<State>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get State
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<State>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _stateService.GetByIdAsync(request.Id);
                return new ApiResponse<State>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<State>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(State state)
        {
            try
            {
                var result = await _stateService.CreateAsync(state);
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
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(State state)
        {
            try
            {
                var result = await _stateService.UpdateAsync(state);
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
                var result = await _stateService.DeleteAsync(request);
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
        /// Download State
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllExport")]
        public IActionResult GetAllExport()
        {
            try
            {
                var result = _stateService.GetAllAsync();
                var res = result.Result.ToList();
                List<State> data = new();
                var ndata = res.Select(x => new { x.Country, x.Code, x.Name, x.Sequence, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("State");
                        worksheet.Range("A1:I1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "Country";
                        worksheet.Cell("B1").Value = "Code";
                        worksheet.Cell("C1").Value = "Name";
                        worksheet.Cell("D1").Value = "Sequence";
                        worksheet.Cell("E1").Value = "Active";
                        worksheet.Cell("F1").Value = "CreatedBy";
                        worksheet.Cell("G1").Value = "CreatedDate";
                        worksheet.Cell("H1").Value = "UpdatedBy";
                        worksheet.Cell("I1").Value = "UpdatedDate";

                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("B1").Style.Font.Bold = true;
                        worksheet.Cell("C1").Style.Font.Bold = true;
                        worksheet.Cell("D1").Style.Font.Bold = true;
                        worksheet.Cell("E1").Style.Font.Bold = true;
                        worksheet.Cell("F1").Style.Font.Bold = true;
                        worksheet.Cell("G1").Style.Font.Bold = true;
                        worksheet.Cell("H1").Style.Font.Bold = true;
                        worksheet.Cell("I1").Style.Font.Bold = true;

                        worksheet.Cell(2, 1).InsertData(dt.Rows);
                        worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        worksheet.Columns(1, 9).AdjustToContents();

                        worksheet.Range("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("F1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("G1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("H1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("I1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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