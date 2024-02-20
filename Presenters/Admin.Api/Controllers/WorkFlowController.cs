using Admin.Services.Contracts;
using ClosedXML.Excel;
using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Middlewares;
using System.Data;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// WorkFlowController
    /// </summary>
    public class WorkFlowController : BaseController
    {
        private readonly ILogger<WorkFlowController> _logger;
        private readonly IWorkFlowService _WorkFlowService;

        /// <summary>
        /// WorkFlowController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="WorkFlowService"></param>
        public WorkFlowController(ILogger<WorkFlowController> logger, IWorkFlowService WorkFlowService)
        {
            _logger = logger;
            _WorkFlowService = WorkFlowService;
            _logger.LogInformation("WorkFlowController Initialized");
        }

        /// <summary>
        /// GetCombinedData 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetCombinedData")]
        public async Task<ApiResponse<WorkFlowCombinedDataResponse>> GetCombinedData()
        {
            try
            {
                var result = await _WorkFlowService.GetCombinedData();
                return new ApiResponse<WorkFlowCombinedDataResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<WorkFlowCombinedDataResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get All WorkFlow
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<WorkFlow>>> GetAll()
        {
            try
            {
                var result = await _WorkFlowService.GetAllAsync();
                return new ApiResponse<IEnumerable<WorkFlow>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<WorkFlow>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get WorkFlow
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<WorkFlow>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _WorkFlowService.GetByIdAsync(request.Id);
                return new ApiResponse<WorkFlow>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<WorkFlow>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="WorkFlow"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(WorkFlow WorkFlow)
        {
            try
            {
                var result = await _WorkFlowService.CreateAsync(WorkFlow);
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
        /// <param name="WorkFlow"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(WorkFlow WorkFlow)
        {
            try
            {
                var result = await _WorkFlowService.UpdateAsync(WorkFlow);
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
                var result = await _WorkFlowService.DeleteAsync(request);
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
        /// Download Department
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllExport")]
        public IActionResult GetAllExport()
        {
            try
            {
                var result = _WorkFlowService.GetAllAsync();
                var res = result.Result.ToList();
                List<WorkFlow> data = new();
                var ndata = res.Select(x => new { x.EntityId, x.RequestStatusId, x.RoleTypeId, x.ApproverRoleTypeId, x.Sequence, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("WorkFlow");
                        worksheet.Range("A1:J1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "Entity Id";
                        worksheet.Cell("B1").Value = "RequestStatus Id";
                        worksheet.Cell("C1").Value = "RoleType Id";
                        worksheet.Cell("D1").Value = "Approver RoleType Id";
                        worksheet.Cell("E1").Value = "Sequence";
                        worksheet.Cell("F1").Value = "Active";
                        worksheet.Cell("G1").Value = "CreatedBy";
                        worksheet.Cell("H1").Value = "CreatedDate";
                        worksheet.Cell("I1").Value = "UpdatedBy";
                        worksheet.Cell("J1").Value = "UpdatedDate";

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
                        worksheet.Range("H1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("I1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("J1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        break;
                    }

                    MemoryStream ms = new MemoryStream();
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