using Admin.Repositories.Contracts;
using Core.DataModel;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Repositories.Admin
{
    /// <summary>
    /// DepartmentRepository
    /// </summary>
    public class DepartmentRepository : IDepartmentRepository
    {
        #region ===[ Private Members ]===================================
        private readonly SqlConnection _sqlConnection;
        private readonly IDbTransaction _dbTransaction;
        private readonly ILogger<DepartmentRepository> _logger;
        #endregion

        #region ===[ Constructor ]=======================================
        public DepartmentRepository(ILogger<DepartmentRepository> logger, SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _logger = logger;
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
            _logger.LogInformation("DepartmentRepository Initialized");
        }
        #endregion

        #region ===[ IDepartmentRepository Methods ]==================================================

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getAll");

            return await _sqlConnection.QueryAsync<Department>("usp_Department", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "getById");
            param.Add("Id", id);

            var res = await _sqlConnection.QueryFirstOrDefaultAsync<Department>("usp_Department", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<bool> CreateAsync(Department department)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "insert");
            param.Add("Id", department.Id);
            param.Add("CompanyId", department.CompanyId == 0 ? 1 : department.CompanyId);
            param.Add("Name", department.Name);
            param.Add("Sequence", department.Sequence);
            param.Add("IsActive", department.IsActive);
            param.Add("CreatedById", department.CreatedById);
            param.Add("CreateDate", DateTime.UtcNow);
            param.Add("ModifiedById", department.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Department", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "update");
            param.Add("Id", department.Id);
            param.Add("CompanyId", department.CompanyId == 0 ? 1 : department.CompanyId);
            param.Add("Name", department.Name);
            param.Add("Sequence", department.Sequence);
            param.Add("IsActive", department.IsActive);
            param.Add("ModifiedById", department.UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow);

            var res = await _sqlConnection.ExecuteAsync("usp_Department", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int Id, bool IsActive, int UpdatedById)
        {
            var param = new DynamicParameters();
            param.Add("ActionType", "delete");
            param.Add("Id", Id);
            param.Add("IsActive", IsActive);
            param.Add("ModifiedById", UpdatedById);
            param.Add("ModifyDate", DateTime.UtcNow); ;

            var res = await _sqlConnection.ExecuteAsync("usp_Department", param, transaction: _dbTransaction, null, commandType: CommandType.StoredProcedure);
            _dbTransaction.Commit();

            return res > 0;
        }
        #endregion
    }
}