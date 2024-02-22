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
    /// CityController
    /// </summary>
    public class CityController : BaseController
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityService _cityService;

        /// <summary>
        /// CountryController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cityService"></param>
        public CityController(ILogger<CityController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
            _logger.LogInformation("CityController Initialized");
        }

        /// <summary>
        /// Get All City
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAll")]
        public async Task<ApiResponse<IEnumerable<City>>> GetAll()
        {
            try
            {
                var result = await _cityService.GetAllAsync();
                return new ApiResponse<IEnumerable<City>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<IEnumerable<City>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get City
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("GetById")]
        public async Task<ApiResponse<City>> GetById(ValueRequest request)
        {
            try
            {
                var result = await _cityService.GetByIdAsync(request.Id);
                return new ApiResponse<City>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<City>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public async Task<ApiResponse<bool>> Create(City city)
        {
            try
            {
                var result = await _cityService.CreateAsync(city);
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
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public async Task<ApiResponse<bool>> Update(City city)
        {
            try
            {
                var result = await _cityService.UpdateAsync(city);
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
                var result = await _cityService.DeleteAsync(request);
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
        /// Download City
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllExport")]
        public IActionResult GetAllExport()
        {
            try
            {
                var result = _cityService.GetAllAsync();
                var res = result.Result.ToList();
                List<City> data = new();
                var ndata = res.Select(x => new { x.State, x.Code, x.Name, x.Sequence, Active = x.ActiveStr, x.CreatedBy, CreatedDate = x.CreateDateStr, x.UpdatedBy, UpdatedDate = x.ModifyDateStr }).ToList();
                DataTable dt = CreateDataTable(ndata);

                if (dt.Rows.Count > 0)
                {
                    var workbook = new XLWorkbook();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var worksheet = workbook.Worksheets.Add("City");
                        worksheet.Range("A1:I1").Style.Fill.BackgroundColor = XLColor.FromArgb(193, 255, 209);
                        worksheet.Cell("A1").Value = "State Name";
                        worksheet.Cell("B1").Value = "City Code";
                        worksheet.Cell("C1").Value = "City Name";
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