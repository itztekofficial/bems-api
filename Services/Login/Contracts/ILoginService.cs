﻿using Core.Models.Request;
using Core.Models.Response;

namespace Login.Services.Contracts
{
    public interface ILoginService
    {
        Task<LoginResponse> GetUserDetails(LoginRequest loginModel);

        Task<LoginDetailResponse> GetUserDetailById(LoginMultiUserRequest request);

        Task<ValidateUserMultiRoleResponse> ValidateUserDetailById(int id);

        Task<UserResponse> GetUserDetailById(int id);

        Task<ForgotPasswordResponse> GetUserDetailsByEmail(string emailId);

        Task<bool> LogoutUser(int id);
    }
}