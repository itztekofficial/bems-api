using Core.DataModel;
using Core.Models.Request;

namespace Login.Repositories.Contracts
{
    public interface IUtilRepository
    {
        Task<bool> SaveOTP(Otp otp);

        Task<bool> ValidateOTP(ValidateOTPRequest request);
    }
}