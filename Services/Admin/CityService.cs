using Core.DataModel;
using Core.Models.Request;
using Main.Services.Contracts.Admin;
using Repositories.Contracts;

namespace Main.Services.Admin
{
    /// <summary>
    /// CityService
    /// </summary>
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get city
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _unitOfWork.Citys.GetAllAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<City> GetByIdAsync(int id)
        {
            return await _unitOfWork.Citys.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(City city)
        {
            return await _unitOfWork.Citys.CreateAsync(city);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(City city)
        {
            return await _unitOfWork.Citys.UpdateAsync(city);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.Citys.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}