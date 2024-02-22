using Core.DataModel;
using Core.Models.Request;

namespace Main.Services.Admin
{
    /// <summary>
    /// EmailSetupService
    /// </summary>
    public class EmailSetupService : IEmailSetupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailSetupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get EmailSetup
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmailSetup>> GetAllAsync()
        {
            return await _unitOfWork.EmailSetups.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmailSetup> GetByIdAsync(int id)
        {
            return await _unitOfWork.EmailSetups.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="EmailSetup"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(EmailSetup emailSetup)
        {
            return await _unitOfWork.EmailSetups.CreateAsync(emailSetup);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="EmailSetup"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(EmailSetup emailSetup)
        {
            return await _unitOfWork.EmailSetups.UpdateAsync(emailSetup);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.EmailSetups.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}