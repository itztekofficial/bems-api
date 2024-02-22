using Core.Models.Response;

namespace Main.Services.Contracts.Admin
{
    public interface IAdminDashBoardService
    {
        Task<AdminDashBoardResponse> GetAdminDashBoardData();
    }
}