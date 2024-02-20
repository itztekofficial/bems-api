using Core.DataModel;
using Core.Models.Request;

namespace Login.Services.Contracts
{
    public interface IUtilService
    {
        Task<bool> SaveOTP(Otp otp);

        Task<bool> ValidateOTP(ValidateOTPRequest request);
    }
}