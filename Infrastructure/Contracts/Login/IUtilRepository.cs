using Core.DataModel;
using Core.Models.Request;

namespace Repositories.Contracts.Login
{
    public interface IUtilRepository
    {
        Task<bool> SaveOTP(Otp otp);

        Task<bool> ValidateOTP(ValidateOTPRequest request);
    }
}