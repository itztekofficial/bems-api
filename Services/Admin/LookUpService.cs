using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;
using Core.Util;
using Omu.ValueInjecter;

namespace Admin.Services
{
    /// <summary>
    /// LookUpService
    /// </summary>
    public class LookUpService : ILookUpService
    {
        //private ILookUpRepository _lookupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LookUpService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get LookUps
        /// </summary>
        /// <param name="lookTypeId"></param>
        /// <returns></returns>
        public async Task<List<LookUpResponse>> GetAllAsync()
        {
            List<LookUpResponse> lookupResponses = new();

            try
            {
                var result = await _unitOfWork.LookUps.GetAllAsync();
                if (result != null)
                {
                    var clone = new List<LookUpResponse>();
                    clone.InjectFrom(result);

                    result.All(detail =>
                    {
                        var dc = new LookUpResponse();
                        dc.InjectFrom(detail);
                        clone.Add(dc);
                        return true;
                    });
                    lookupResponses = clone;
                }
                return lookupResponses;
            }
            catch (Exception ex)
            {
                Log.WriteLog("LookUpService", "Get", ex.Message);
                throw;
            }
        }


        /// <summary>
        /// GetByLookTypeIdAsync
        /// </summary>
        /// <param name="lookTypeId"></param>
        /// <returns></returns>
        public async Task<List<LookUpResponse>> GetAllByLookTypeIdAsync(int lookTypeId)
        {
            List<LookUpResponse> lookupResponses = new();

            try
            {
                var result = await _unitOfWork.LookUps.GetAllByLookTypeIdAsync(lookTypeId);
                if (result != null)
                {
                    var clone = new List<LookUpResponse>();
                    clone.InjectFrom(result);

                    result.All(detail =>
                    {
                        var dc = new LookUpResponse();
                        dc.InjectFrom(detail);
                        clone.Add(dc);
                        return true;
                    });
                    lookupResponses = clone;
                }
                return lookupResponses;
            }
            catch (Exception ex)
            {
                Log.WriteLog("LookUpService", "Get", ex.Message);
                throw;
            }
        }
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LookUpModel> GetByIdAsync(int id)
        {
            return await _unitOfWork.LookUps.GetByIdAsync(id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(LookUpModel entity)
        {
            return await _unitOfWork.LookUps.CreateAsync(entity);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(LookUpModel entity)
        {
            return await _unitOfWork.LookUps.UpdateAsync(entity);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(DeleteRequest request)
        {
            return await _unitOfWork.LookUps.DeleteAsync(request.Id, request.IsActive, request.UpdatedById);
        }
    }
}