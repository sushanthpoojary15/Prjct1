using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Data.Abstractions
{
    public interface IDatabaseManager
    {
        IDbDataParameter CreateParameter(string name, object value, DbType dbType);
        IDbDataParameter CreateParameter(string name, object value, SqlDbType sqlDbType);
        IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection);
        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType);
        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection);
        Task<IEnumerable<T>> GetAllColumns<T>(string commandText, IDbDataParameter[] parameters = null, string connectionString = null) where T : class;

        Task<IEnumerable<T>> Select<T>(string commandText, IDbDataParameter[] parameters = null);
        // Task<IEnumerable<T>> Select<T>(string commandText, Func<IDataReader, T> selector, IDbDataParameter[] parameters = null) /*where T : class*/;
        Task<object> GetScalarValue(string commandText, IDbDataParameter[] parameters = null);
        Task<object> Insert(string commandText, IDbDataParameter[] parameters = null);
        Task Update(string commandText, IDbDataParameter[] parameters = null);
        Task Delete(string commandText, IDbDataParameter[] parameters = null);
        Task InsertOrUpdateWithTransaction(string commandText, IDbDataParameter[] parameters = null);
        Task<DataSet> GetAllData(string commandText, IDbDataParameter[] parameters = null);
        Task<IEnumerable<T>> GetAllColumnsFromDataTable<T>(DataTable dataTable) where T : class;

    }
}
