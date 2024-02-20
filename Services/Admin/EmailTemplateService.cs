using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;

namespace Admin.Services
{
    /// <summary>
    /// EmailTemplate
    /// </summary>
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get EmailTemplate
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmailTemplate>> GetAllAsync()
        {
            return await _unitOfWork.EmailTemplates.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmailTemplate> GetByIdAsync(int id)
        {
            return await _unitOfWork.EmailTemplates.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(EmailTemplate emailTemplate)
        {
            return await _unitOfWork.EmailTemplates.CreateAsync(emailTemplate);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(EmailTemplate emailTemplate)
        {
            return await _unitOfWork.EmailTemplates.UpdateAsync(emailTemplate);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.EmailTemplates.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}