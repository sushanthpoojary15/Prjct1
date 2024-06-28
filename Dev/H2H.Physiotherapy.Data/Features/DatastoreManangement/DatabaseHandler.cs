using H2H.Physiotherapy.Data.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;

namespace H2H.Physiotherapy.Data.Features.DatastoreManangement
{
    public class DatabaseHandler : IDatabaseHandler
    {

        private readonly string connectionString;

        public DatabaseHandler(IConfiguration configuration)
        {
            connectionString = configuration["PhysiotheraphyConfiguartions:ConnectionString"];
        }

        public void CloseConnection(DbConnection dbConnection)
        {
            if (!ReferenceEquals(dbConnection, null))
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        public IDataAdapter CreateAdapter(DbCommand dbCommand)
        {
            return new SqlDataAdapter((SqlCommand)dbCommand);
        }

        public DbCommand CreateCommand(string commandText, DbConnection connection)
        {
            return new SqlCommand
            {
                CommandText = commandText,
                Connection = (SqlConnection)connection,
                CommandType = CommandType.StoredProcedure
            };
        }

        public DbConnection CreateConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
                return new SqlConnection(connectionString);
            else
                return new SqlConnection(connectionString);
        }

        public IDbDataParameter CreateParameter(DbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
