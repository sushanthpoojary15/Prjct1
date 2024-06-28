using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections;
using H2H.Physiotherapy.Data.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;


namespace H2H.Physiotherapy.Data.Features.DatastoreManangement
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IDatabaseHandler _databaseHandler;
        private readonly IParamManager _paramManager;

        public DatabaseManager(IDatabaseHandler databaseHandler, IParamManager paramManager)
        {
            _databaseHandler = databaseHandler;
            _paramManager = paramManager;
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
        {
            return _paramManager.CreateParameter(name, value, dbType);
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection)
        {
            return _paramManager.CreateParameter(name, value, dbType, parameterDirection);
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
        {
            return _paramManager.CreateParameter(name, size, value, dbType);
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection)
        {
            return _paramManager.CreateParameter(name, size, value, dbType, parameterDirection);
        }

        public IDbDataParameter CreateParameter(string name, object value, SqlDbType sqlDbType)
        {
            return _paramManager.CreateParameter(name, value, sqlDbType);
        }

        //public async Task<IEnumerable<T>> GetAllColumns<T>(string commandText, IDbDataParameter[] parameters = null, string connectionString = null) where T : class
        //{

        //    using (var connection = _databaseHandler.CreateConnection())
        //    {
        //        await connection.OpenAsync();
        //        var command = _databaseHandler.CreateCommand(commandText, connection);
        //        if (parameters != null)
        //            command.Parameters.AddRange(parameters);
        //        var dataAdapter = _databaseHandler.CreateAdapter(command);
        //        var dataSet = new DataSet();
        //        dataAdapter.Fill(dataSet);

        //        return dataSet.Tables[0].ToList<T>();
        //    }
        //}

        public async Task<IEnumerable<T>> GetAllColumns<T>(string commandText, IDbDataParameter[] parameters = null, string connectionString = null) where T : class
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                await connection.OpenAsync();

                var dynamicParameters = new DynamicParameters();
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        dynamicParameters.Add(param.ParameterName, param.Value, param.DbType, param.Direction, param.Size, param.Precision, param.Scale);
                    }
                }
                var result = await connection.QueryAsync<T>(commandText, dynamicParameters);
                return result;
            }
        }



        public async Task<IEnumerable<T>> Select<T>(string commandText, IDbDataParameter[] parameters = null) /*where T : class*/
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                await connection.OpenAsync();

                var dynamicParameters = new DynamicParameters();
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        dynamicParameters.Add(param.ParameterName, param.Value, param.DbType, param.Direction, param.Size, param.Precision, param.Scale);
                    }
                }
                var result = await connection.QueryAsync<T>(commandText, dynamicParameters);


                return result;
            }
        }


        public async Task<object> GetScalarValue(string commandText, IDbDataParameter[] parameters = null)
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                await connection.OpenAsync();
                var command = _databaseHandler.CreateCommand(commandText, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                return await command.ExecuteScalarAsync();
            }
        }


        public async Task<object> Insert(string commandText, IDbDataParameter[] parameters = null)
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                await connection.OpenAsync();
                var command = _databaseHandler.CreateCommand(commandText, connection);

                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                return await command.ExecuteScalarAsync();
            }
        }

        public async Task Update(string commandText, IDbDataParameter[] parameters = null)
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                await connection.OpenAsync();
                var command = _databaseHandler.CreateCommand(commandText, connection);

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(string commandText, IDbDataParameter[] parameters = null)
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                await connection.OpenAsync();
                var command = _databaseHandler.CreateCommand(commandText, connection);

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task InsertOrUpdateWithTransaction(string commandText, IDbDataParameter[] parameters = null)
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                SqlTransaction transaction = null;
                await connection.OpenAsync();
                var command = _databaseHandler.CreateCommand(commandText, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                transaction = await Task.Run(
                        () => (SqlTransaction)connection.BeginTransaction(IsolationLevel.ReadUncommitted)
                );

                try
                {
                    command.Transaction = transaction;
                    await command.ExecuteNonQueryAsync();
                    await Task.Run(() => transaction.Commit());
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        // throw new Exception($"{ex1.Message}; Transaction failed, rollback not successfull : {(ex1.InnerException != null ? ex1.InnerException.Message : "")}");
                        throw new Exception($"{ex1.Message}");
                    }

                    var msg = ex.Message.Split('\'');
                    //throw new Exception($"{ex.Message}; Transaction failed : {(ex.InnerException != null ? ex.InnerException.Message : "")}");
                    throw new Exception($"{ex.Message}");

                }
            }
        }

        public async Task<DataSet> GetAllData(string commandText, IDbDataParameter[] parameters = null)
        {
            using (var connection = _databaseHandler.CreateConnection())
            {
                await connection.OpenAsync();
                var command = _databaseHandler.CreateCommand(commandText, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                var dataAdapter = _databaseHandler.CreateAdapter(command);
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        public async Task<IEnumerable<T>> GetAllColumnsFromDataTable<T>(DataTable dataTable) where T : class
        {
            return dataTable.ToList<T>();
        }




    }
}
