using Core.DataModel;
using Core.Models.Request;
using Core.Models.Response;

namespace Login.Repositories.Contracts
{
    public interface ILoginRepository
    {
        Task<User> GetUserDetails(LoginRequest request);

        Task<LoginDetailResponse> GetUserDetailById(LoginMultiUserRequest request);

        Task<ValidateUserMultiRoleResponse> ValidateUserDetailById(int id);

        Task<User> GetUserDetailById(int id);

        Task<ForgotPasswordResponse> GetUserDetailsByEmail(string emailId);

        Task<bool> LogoutUser(int id);
    }
}