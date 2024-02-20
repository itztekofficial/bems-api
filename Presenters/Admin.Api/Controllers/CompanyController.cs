using Admin.Services.Contracts;
using Core.Models.Request;
using Core.Models.Response;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// CompanyController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;

        /// <summary>
        /// CompanyController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="compnyservice"></param>
        public CompanyController(ILogger<CompanyController> logger, ICompanyService compnyservice)
        {
            _logger = logger;
            _companyService = compnyservice;
            _logger.LogInformation("CompanyController Initialized");
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<ApiResponse<bool>> Save(CompanyRequest company)
        {
            try
            {
                int result = await _companyService.Save(company);

                if (result == 1)
                {
                    return new ApiResponse<bool>()
                    {
                        Status = EnumStatus.Success,
                        Data = true,
                    };
                }
                else if (result == 2)
                { return new ApiResponse<bool>() { Data = false, Status = EnumStatus.Success, Message = "Email or mobile are already exist!" }; }
                else
                { return new ApiResponse<bool>() { Data = false, Status = EnumStatus.Error, Message = "Some error occured!" }; }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetList")]
        public async Task<ApiResponse<List<CompanyResponse>>> GetList()
        {
            try
            {
                var result = await _companyService.GetList();
                return new ApiResponse<List<CompanyResponse>>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<List<CompanyResponse>>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// GetCompanyDetail
        /// </summary>
        /// <param name="Req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCompanyDetail")]
        public async Task<ApiResponse<CompanyResponse>> GetCompanyDetail(ValueRequestString Req)
        {
            try
            {
                var result = await _companyService.GetCompanyDetail(Req.Id);
                return new ApiResponse<CompanyResponse>()
                {
                    Status = EnumStatus.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<CompanyResponse>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// EmailMobileExistValid
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EmailMobileExistValid")]
        public async Task<ApiResponse<bool>> EmailMobileExistValid(TypeValueInput val)
        {
            try
            {
                // type : email/mobile
                int result = await _companyService.EmailMobileExistValid(val.Type, val.Value);

                if (result == 2)
                {
                    return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = "Some error occured" };
                }
                else
                {
                    return new ApiResponse<bool>()
                    {
                        Status = EnumStatus.Success,
                        Data = (result == 1) ? true : false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiResponse<bool>() { Status = EnumStatus.Error, Message = ex.Message };
            }
        }
    }
}