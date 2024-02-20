using Admin.Repositories.Contracts;
using Admin.Services.Contracts;
using Core.Models.Response;

namespace Admin.Services
{
    /// <summary>
    /// AdminDashBoardService
    /// </summary>
    public class AdminDashBoardService : IAdminDashBoardService
    {
        private readonly IAdminDashBoardRepository _adminDashBoard;

        /// <summary>
        /// AdminDashBoardService
        /// </summary>
        /// <param name="adminDashBoard"></param>
        public AdminDashBoardService(IAdminDashBoardRepository adminDashBoard)
        {
            _adminDashBoard = adminDashBoard;
        }

        /// <summary>
        /// Get Admin DashBoard Data
        /// </summary>
        /// <returns></returns>
        public async Task<AdminDashBoardResponse> GetAdminDashBoardData()
        {
            return await _adminDashBoard.GetAdminDashBoardData();
        }

    }
}