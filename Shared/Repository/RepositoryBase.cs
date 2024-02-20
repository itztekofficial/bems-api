namespace Shared.Repository
{
    using Shared.Models.DB;
    using Shared.Repository.Contracts;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="RepositoryBase" />.
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// Gets the BackUpDBConnection.
        /// </summary>
        public IDbConnection backUpDBConnection { get; private set; }

        /// <summary>
        /// Defines the DbConnectionFactory.
        /// </summary>
        public IDbConnectionFactory DbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The dbConnectionFactory<see cref="IDbConnectionFactory"/>.</param>
        public RepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
            this.backUpDBConnection = dbConnectionFactory.CreateDbConnection(DatabaseConnectionName.BackUpDB);
            DbConnectionFactory = dbConnectionFactory;
        }
    }
}