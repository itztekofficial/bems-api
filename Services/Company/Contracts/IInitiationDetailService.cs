using Core.Models.Response;
using Domain;

namespace Company.Services.Contracts
{
    public interface IInitiationDetailService
    {
        Task<IEnumerable<InitiationDetail>> GetAllAsync();
        Task<InitiationResponse> GetByIdAsync(int id);
        Task<string> CreateAsync(InitiationDetail initiationDetail);
        Task<bool> UpdateAsync(InitiationDetail initiationDetail);
        Task<bool> UpdateStatusAsync(InitiationUpdateStatus updateStatus);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateBeforeExecutionStepsAsync(BeforeExecutionModel beforeExecutionModel);
        Task<bool> SharedWithCustomerAsync(SharedWithCustomerModel sharedWithCustomer);
        Task<bool> SharedWithLegalAcknowlegeAsync(SharedWithLegalAcknowlegeModel sharedWithLegal);
        Task<bool> UpdateLegalUserAsync(UpdateLegaluserModel legaluserModel);
    }
}