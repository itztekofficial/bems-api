using ClosedXML.Excel;
using Company.Services.Contracts;
using Core.Models.Request;
using Core.Models.Response;
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
    /// RequestApprovalController
    /// </summary>
    public class RequestApprovalController : BaseController
    {
        private readonly ILogger<RequestApprovalController> _logger;
        private readonly IRequestApprovalService _requestApprovalService;

        /// <summary>
        /// RequestApprovalController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="requestApprovalService"></param>
        public RequestApprovalController(ILogger<RequestApprovalController> logger, IRequestApprovalService requestApprovalService)
        {
            _logger = logger;
            _requestApprovalService = requestApprovalService;
            _logger.LogInformation("RequestApprovalController Initialized");
        }

        /// <summary>
        /// Get Approval List
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetRequestApproval")]
        public async Task<ApiResponse<IEnumerable<ApprovalResponse>>> GetApprovalList(ApprovalRequest approvalRequest)
        {
            try
            {
                //GetHeaderData();
                //var model = new FormParmData()
                //{
                //    CompanyId = CompanyId
                //};

                var result = await _requestApprovalService.GetApprovalListAsync(approvalRequest);
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
        /// ExportContract  
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("ExportContract")]
        public IActionResult ExportContract(ApprovalRequest approvalRequest)
        {
            try
            {
                var response = _requestApprovalService.GetApprovalListAsync(approvalRequest);
                var res = response.Result.ToList();

                var ndata = res.Select(x => new
                {
                    ContractId = x.RefId,
                    x.CustomerName,
                    x.Entity,
                    x.EntityType,
                    x.CustomerType,
                    x.Agreement,
                    x.PaymentTerm,
                    RequestStatus = (x.RequestStatus == "Negotiation" && x.PendingReply > 0) ? "Negotiation (Pending Approval)" : x.RequestStatus,
                    x.RequestBy,
                    x.RequestOn
                }).ToList();

                DataTable dt = CreateDataTable(ndata);
                var workbook = new XLWorkbook();
                if (dt.Rows.Count > 0)
                {
                    var worksheet = workbook.Worksheets.Add("Contract");

                    worksheet.Range("A1:H1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                    worksheet.Cell("A1").Value = "Contract Id";
                    worksheet.Cell("B1").Value = "Customer Name";
                    worksheet.Cell("C1").Value = "Entity";
                    worksheet.Cell("D1").Value = "Entity Type";
                    worksheet.Cell("E1").Value = "Customer Type";
                    worksheet.Cell("F1").Value = "Agreement";
                    worksheet.Cell("G1").Value = "Payment Term";
                    worksheet.Cell("H1").Value = "Request Status";
                    worksheet.Cell("I1").Value = "Request By";
                    worksheet.Cell("J1").Value = "Request On";

                    // worksheet.Style.Font.SetBold();
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("B1").Style.Font.Bold = true;
                    worksheet.Cell("C1").Style.Font.Bold = true;
                    worksheet.Cell("D1").Style.Font.Bold = true;
                    worksheet.Cell("E1").Style.Font.Bold = true;
                    worksheet.Cell("F1").Style.Font.Bold = true;
                    worksheet.Cell("G1").Style.Font.Bold = true;
                    worksheet.Cell("H1").Style.Font.Bold = true;
                    worksheet.Cell("I1").Style.Font.Bold = true;
                    worksheet.Cell("J1").Style.Font.Bold = true;

                    worksheet.Range("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("C1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("F1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("G1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("H1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("I1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("J1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(2, 1).InsertData(dt.Rows);
                    worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    worksheet.Columns(1, 10).AdjustToContents();
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

    }
}