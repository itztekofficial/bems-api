using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// TermValidityService
    /// </summary>
    public class TermValidityService : ITermValidityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TermValidityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get TermValidity
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TermValidity>> GetAllAsync()
        {
            return await _unitOfWork.TermValiditys.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TermValidity> GetByIdAsync(int id)
        {
            return await _unitOfWork.TermValiditys.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="termValidity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(TermValidity termValidity)
        {
            return await _unitOfWork.TermValiditys.CreateAsync(termValidity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="termValidity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TermValidity termValidity)
        {
            return await _unitOfWork.TermValiditys.UpdateAsync(termValidity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.TermValiditys.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}