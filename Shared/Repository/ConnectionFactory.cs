namespace Shared.Repository
{
    using Shared.Models.DB;
    using Shared.Repository.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="ConnectionFactory" />.
    /// </summary>
    public class ConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Defines the _connectionDict.
        /// </summary>
        private readonly IDictionary<DatabaseConnectionName, string> _connectionDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionDict">The connectionDict<see cref="IDictionary{DatabaseConnectionName, string}"/>.</param>
        public ConnectionFactory(IDictionary<DatabaseConnectionName, string> connectionDict)
        {
            _connectionDict = connectionDict;
        }

        /// <summary>
        /// The CreateDbConnection.
        /// </summary>
        /// <param name="connectionName">The connectionName<see cref="DatabaseConnectionName"/>.</param>
        /// <returns>The <see cref="IDbConnection"/>.</returns>
        public IDbConnection CreateDbConnection(DatabaseConnectionName connectionName)
        {
            if (_connectionDict.TryGetValue(connectionName, out string connectionString))
            {
                return new SqlConnection(connectionString);
            }

            throw new ArgumentNullException();
        }
    }

    public enum DatabaseType
    {
        BackUpDB = 0
    }

    public class DbConnectionData
    {
        public string DBConnection { get; set; }
    }

    public class Database
    {
        public DatabaseType DatabaseType { get; set; }
    }
}