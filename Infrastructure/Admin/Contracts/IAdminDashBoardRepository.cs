using Core.Models.Response;

namespace Admin.Repositories.Contracts;
public interface IAdminDashBoardRepository
{
    Task<AdminDashBoardResponse> GetAdminDashBoardData();
}
