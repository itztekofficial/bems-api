using Core.Models.Request;
using Core.Models.Response;

namespace Admin.Services.Contracts
{
    public interface ICompanyService
    {
        Task<List<CompanyResponse>> GetList();

        Task<int> Save(CompanyRequest RType);

        Task<CompanyResponse> GetCompanyDetail(string Id);

        Task<int> EmailMobileExistValid(string Type, string val);
    }
}