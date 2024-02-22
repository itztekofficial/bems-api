using Core.Models.Request;
using Core.Models.Response;

namespace Main.Services.Admin
{
    /// <summary>
    /// CompanyService
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <returns></returns>
        public async Task<List<CompanyResponse>> GetList()
        {
            List<CompanyResponse> CompanyResponses = new();
            try
            {
                var result = await _companyRepository.GetList();
                if (result != null)
                {
                    CompanyResponses = result;
                }
                return CompanyResponses;
            }
            catch (Exception ex)
            {
                Log.WriteLog("CompanyService", "GetList", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        public async Task<int> Save(CompanyRequest Company)
        {
            try
            {
                return await _companyRepository.Save(Company);
            }
            catch (Exception ex)
            {
                Log.WriteLog("CompanyService", "Save", ex.Message);
                return 3;
            }
        }

        /// <summary>
        /// GetCompanyDetail
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CompanyResponse> GetCompanyDetail(string Id)
        {
            CompanyResponse CompanyResponses = new();
            try
            {
                var result = await _companyRepository.GetCompanyDetail(Id);
                if (result != null)
                {
                    CompanyResponses = result;
                }
                return CompanyResponses;
            }
            catch (Exception ex)
            {
                Log.WriteLog("CompanyService", "GetCompanyDetail", ex.Message);
                throw;
            }
        }

        public async Task<int> EmailMobileExistValid(string Type, string val)
        {
            try
            {
                return await _companyRepository.EmailMobileExistValid(Type, val);
            }
            catch (Exception ex)
            {
                Log.WriteLog("CompanyService", "Save", ex.Message);
                return 2;
            }
        }
    }
}