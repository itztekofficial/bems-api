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
    /// CountryController
    /// </summary>
    public class CountryController : BaseController
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;

        /// <summary>
        /// CountryController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="countryService"></param>
        public CountryController(ILogger<CountryController> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
            _logger.LogInformation("CountryController Initialized");
        }

        /// <summary>
        /// Get All Department
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<Country>>> GetAll()
        {
            try
            {
                var result = await _countryService.GetAllAsync();
                return new ApiResponse<IEnumerable<Country>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<Country>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get Country
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<Country>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _countryService.GetByIdAsync(request.Id);
                return new ApiResponse<Country>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<Country>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(Country country)
        {
            try
            {
                var result = await _countryService.CreateAsync(country);
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
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(Country country)
        {
            try
            {
                var result = await _countryService.UpdateAsync(country);
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
                var result = await _countryService.DeleteAsync(request);
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
        /// Download Country
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllExport")]
        public IActionResult GetAllExport()
        {
            try
            {
                var result = _countryService.GetAllAsync();
                var res = result.Result.ToList();
                List<Country> data = new();
                var ndata = res.Select(x => new { x.Code, x.Name, x.Sequence, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("Country");
                        worksheet.Range("A1:H1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "Code";
                        worksheet.Cell("B1").Value = "Name";
                        worksheet.Cell("C1").Value = "Sequence";
                        worksheet.Cell("D1").Value = "Active";
                        worksheet.Cell("E1").Value = "CreatedBy";
                        worksheet.Cell("F1").Value = "CreatedDate";
                        worksheet.Cell("G1").Value = "UpdatedBy";
                        worksheet.Cell("H1").Value = "UpdatedDate";

                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("B1").Style.Font.Bold = true;
                        worksheet.Cell("C1").Style.Font.Bold = true;
                        worksheet.Cell("D1").Style.Font.Bold = true;
                        worksheet.Cell("E1").Style.Font.Bold = true;
                        worksheet.Cell("F1").Style.Font.Bold = true;
                        worksheet.Cell("G1").Style.Font.Bold = true;
                        worksheet.Cell("H1").Style.Font.Bold = true;

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