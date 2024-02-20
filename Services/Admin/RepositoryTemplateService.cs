using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// RepositoryTemplate
    /// </summary>
    public class RepositoryTemplateService : IRepositoryTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get RepositoryTemplate
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RepositoryTemplate>> GetAllAsync()
        {
            return await _unitOfWork.RepositoryTemplates.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RepositoryTemplate> GetByIdAsync(int id)
        {
            return await _unitOfWork.RepositoryTemplates.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="RepositoryTemplates"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(RepositoryTemplate repositoryTemplate)
        {
            return await _unitOfWork.RepositoryTemplates.CreateAsync(repositoryTemplate);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="RepositoryTemplates"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(RepositoryTemplate repositoryTemplate)
        {
            return await _unitOfWork.RepositoryTemplates.UpdateAsync(repositoryTemplate);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.RepositoryTemplates.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}