using Core.Models.Request;
using Core.Models.Response;

namespace Admin.Services.Contracts
{
    public interface ISettingsService
    {
        #region "usp_Settings"

        Task<MyProfileResponse> GetUserDetailsById(int uId);
        Task<string> GetUserPWDById(int uId);
        Task<bool> UpdateUserPWDById(PWDChangeRequest request);

        #endregion "usp_Settings"
    }
}