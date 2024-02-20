namespace Shared.Repository.Contracts
{
    using Shared.Models.DB;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="IDbConnectionFactory" />.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// The CreateDbConnection.
        /// </summary>
        /// <param name="connectionName">The connectionName<see cref="DatabaseConnectionName"/>.</param>
        /// <returns>The <see cref="IDbConnection"/>.</returns>
        IDbConnection CreateDbConnection(DatabaseConnectionName connectionName);
    }
}