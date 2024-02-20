using Company.Repositories.Contracts;
using Company.Services.Contracts;
using Core.Models.Response;
using Domain;

namespace Company.Services
{
    /// <summary>
    /// InitiationDetailService
    /// </summary>
    public class InitiationDetailService : IInitiationDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InitiationDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get InitiationDetail
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<InitiationDetail>> GetAllAsync()
        {
            return await _unitOfWork.InitiationDetails.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<InitiationResponse> GetByIdAsync(int id)
        {
            return await _unitOfWork.InitiationDetails.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="InitiationDetail"></param>
        /// <returns></returns>
        public async Task<string> CreateAsync(InitiationDetail initiationDetail)
        {
            return await _unitOfWork.InitiationDetails.CreateAsync(initiationDetail);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="InitiationDetail"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(InitiationDetail initiationDetail)
        {
            return await _unitOfWork.InitiationDetails.UpdateAsync(initiationDetail);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            return await _unitOfWork.InitiationDetails.DeleteAsync(id);
        }

        /// <summary>
        /// UpdateStatusAsync
        /// </summary>
        /// <param name="updateStatus"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStatusAsync(InitiationUpdateStatus updateStatus)
        {
            return await _unitOfWork.InitiationDetails.UpdateStatusAsync(updateStatus);
        }

        /// <summary>
        /// UpdateBeforeExecutionStepsAsync
        /// </summary>
        /// <param name="beforeExecutionModel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBeforeExecutionStepsAsync(BeforeExecutionModel beforeExecutionModel)
        {
            return await _unitOfWork.InitiationDetails.UpdateBeforeExecutionStepsAsync(beforeExecutionModel);
        }

        /// <summary>
        /// SharedWithCustomerAsync
        /// </summary>
        /// <param name="sharedWithCustomer"></param>
        /// <returns></returns>
        public async Task<bool> SharedWithCustomerAsync(SharedWithCustomerModel sharedWithCustomer)
        {
            return await _unitOfWork.InitiationDetails.SharedWithCustomerAsync(sharedWithCustomer);
        }

        /// <summary>
        /// SharedWithLegalAcknowlegeAsync
        /// </summary>
        /// <param name="sharedWithLegal"></param>
        /// <returns></returns>
        public async Task<bool> SharedWithLegalAcknowlegeAsync(SharedWithLegalAcknowlegeModel sharedWithLegal)
        {
            return await _unitOfWork.InitiationDetails.SharedWithLegalAcknowlegeAsync(sharedWithLegal);
        }

        /// <summary>
        /// UpdateLegalUserAsync
        /// </summary>
        /// <param name="legaluserModel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLegalUserAsync(UpdateLegaluserModel legaluserModel)
        {
            return await _unitOfWork.InitiationDetails.UpdateLegalUserAsync(legaluserModel);
        }
    }
}