using Core.Models.Request;
using Core.Models.Response;

namespace Admin.Repositories.Contracts
{
    public interface ICompanyRepository
    {
        Task<List<CompanyResponse>> GetList();

        Task<int> Save(CompanyRequest Company);

        Task<CompanyResponse> GetCompanyDetail(string Id);

        Task<int> EmailMobileExistValid(string Type, string val);
    }
}