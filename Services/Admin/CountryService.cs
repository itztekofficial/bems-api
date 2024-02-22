using Core.DataModel;
using Core.Models.Request;
using Main.Services.Contracts.Admin;
using Repositories.Contracts;

namespace Main.Services.Admin
{
    /// <summary>
    /// CountryService
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Country
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _unitOfWork.Countries.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Country> GetByIdAsync(int id)
        {
            return await _unitOfWork.Countries.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Country cont)
        {
            return await _unitOfWork.Countries.CreateAsync(cont);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Country cont)
        {
            return await _unitOfWork.Countries.UpdateAsync(cont);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.Countries.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}