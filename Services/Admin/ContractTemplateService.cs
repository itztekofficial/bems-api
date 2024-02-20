using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// ContractTemplate
    /// </summary>
    public class ContractTemplateService : IContractTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get ContractTemplate
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ContractTemplate>> GetAllAsync()
        {
            return await _unitOfWork.ContractTemplates.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ContractTemplate> GetByIdAsync(int id)
        {
            return await _unitOfWork.ContractTemplates.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="ContractTemplate"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ContractTemplate contractTemplate)
        {
            return await _unitOfWork.ContractTemplates.CreateAsync(contractTemplate);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="ContractTemplate"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ContractTemplate contractTemplate)
        {
            return await _unitOfWork.ContractTemplates.UpdateAsync(contractTemplate);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.ContractTemplates.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}