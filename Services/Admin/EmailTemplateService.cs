using Core.DataModel;
using Core.Models.Request;
using Main.Services.Contracts.Admin;
using Repositories.Contracts;

namespace Main.Services.Admin
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

        public Task<bool> CreateAsync(EmailTemplate emailTemplate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(DeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmailTemplate>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmailTemplate> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(EmailTemplate emailTemplate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get EmailTemplate
        /// </summary>
        /// <returns></returns>
        //public async Task<IEnumerable<EmailTemplate>> GetAllAsync()
        //{
        //    return await _unitOfWork.EmailTemplates.GetAllAsync();
        //}

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<EmailTemplate> GetByIdAsync(int id)
        //{
        //    return await _unitOfWork.EmailTemplates.GetByIdAsync(id);
        //}

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        //public async Task<bool> CreateAsync(EmailTemplate emailTemplate)
        //{
        //    return await _unitOfWork.EmailTemplates.CreateAsync(emailTemplate);
        //}

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="EmailTemplate"></param>
        /// <returns></returns>
        //public async Task<bool> UpdateAsync(EmailTemplate emailTemplate)
        //{
        //    return await _unitOfWork.EmailTemplates.UpdateAsync(emailTemplate);
        //}

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //public async Task<bool> DeleteAsync(DeleteRequest request)
        //{
        //    return await _unitOfWork.EmailTemplates.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        //}
    }
}