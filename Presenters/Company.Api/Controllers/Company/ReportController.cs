using ClosedXML.Excel;
using Core.Models.Request;
using Core.Util;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middlewares;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Api.Controllers
{
    /// <summary>
    /// ReportController
    /// </summary>
    public class ReportController : BaseController
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService _reportService;

        /// <summary>
        /// ReportController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="reportService"></param>
        public ReportController(ILogger<ReportController> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
            _logger.LogInformation("ReportController Initialized");
        }

        /// <summary>
        /// Get All Contracts
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllContracts")]
        public async Task<ApiResponse<IEnumerable<ApprovalResponse>>> GetAllContractsAsync(ReportRequest reportRequest)
        {
            try
            {
                var result = await _reportService.GetAllContractsAsync(reportRequest);
                return new ApiResponse<IEnumerable<ApprovalResponse>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<ApprovalResponse>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// ExportAllContracts  
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("ExportAllContracts")]
        public IActionResult ExportAllContracts(ReportRequest reportRequest)
        {
            try
            {
                var response = _reportService.GetExportAllContractsAsync(reportRequest);
                var res = response.Result.ToList();
                DataTable dt = CreateDataTable(res);

                var workbook = new XLWorkbook();
                if (dt.Rows.Count > 0)
                {
                    int maxColCount = dt.Columns.Count;
                    string maxCol = UtilFunction.ColumnName(maxColCount);
                    var worksheet = workbook.Worksheets.Add("Contracts");
                    worksheet.Range("A1:" + maxCol + "1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);

                    int colIndex = 1;
                    foreach (DataColumn col in dt.Columns)
                    {
                        string colHeaderName = GetHeaderName(col.ColumnName);
                        string colName = UtilFunction.ColumnName(colIndex) + "1";
                        worksheet.Cell(colName).Value = colHeaderName;
                        worksheet.Cell(colName).Style.Font.Bold = true;
                        worksheet.Range(colName).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        colIndex++;
                    }

                    worksheet.Cell(2, 1).InsertData(dt.Rows);

                    worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    worksheet.Columns(1, maxColCount).AdjustToContents();
                }
                MemoryStream ms = new();
                workbook.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);

                return File(
                fileContents: ms.ToArray(),
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        static string GetHeaderName(string columnName)
        {
            return (columnName == "DLNumber") ? "DL Number"
                : (columnName == "PANNumber") ? "PAN Number"
                : (columnName == "GSTNumber") ? "GST Number"
                : (columnName == "DLAddress") ? "DL Address"
                : (columnName == "DLAddress1") ? "DL Address1"
                : (columnName == "DLCountry") ? "DL Country"
                : (columnName == "DLOtherCountry") ? "DL Other Country"
                : (columnName == "DLState") ? "DL State"
                : (columnName == "DLOtherState") ? "DL Other State"
                : (columnName == "DLCity") ? "DL City"
                : (columnName == "DLOtherCity") ? "DL Other City"
                : (columnName == "DLPinNo") ? "DL Pin No"
                : UtilFunction.AddSpacesToText(columnName);
        }
    }
}