using Core.Models.Response;

namespace Admin.Services.Contracts
{
    public interface IAdminDashBoardService
    {
        Task<AdminDashBoardResponse> GetAdminDashBoardData();
    }
}