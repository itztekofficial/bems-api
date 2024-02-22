namespace Repositories.Contracts.Admin;
public interface IAdminDashBoardRepository
{
    Task<AdminDashBoardResponse> GetAdminDashBoardData();
}
