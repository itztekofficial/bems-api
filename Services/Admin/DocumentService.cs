using Core.DataModel;
using Core.Models.Request;
using Main.Services.Contracts.Admin;
using Repositories.Contracts;

namespace Main.Services.Admin
{
    /// <summary>
    /// DocumentService
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Documents
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _unitOfWork.Documents.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Document> GetByIdAsync(int id)
        {
            return await _unitOfWork.Documents.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Document entity)
        {
            return await _unitOfWork.Documents.CreateAsync(entity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Document entity)
        {
            return await _unitOfWork.Documents.UpdateAsync(entity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.Documents.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}