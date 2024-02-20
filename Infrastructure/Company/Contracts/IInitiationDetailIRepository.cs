using Core.Models.Response;
using Domain;

namespace Company.Repositories.Contracts;
public interface IInitiationDetailRepository : IRepository<InitiationDetail>
{
    new Task<string> CreateAsync(InitiationDetail initiationDetail);
    new Task<InitiationResponse> GetByIdAsync(int id);
    Task<bool> UpdateStatusAsync(InitiationUpdateStatus updateStatus);
    Task<bool> UpdateBeforeExecutionStepsAsync(BeforeExecutionModel beforeExecutionModel);
    Task<bool> SharedWithCustomerAsync(SharedWithCustomerModel sharedWithCustomer);
    Task<bool> SharedWithLegalAcknowlegeAsync(SharedWithLegalAcknowlegeModel sharedWithLegal);
    Task<bool> UpdateLegalUserAsync(UpdateLegaluserModel legaluserModel);
}
