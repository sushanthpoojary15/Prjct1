using System.Data.Common;
using System.Data;

namespace H2H.Physiotherapy.Data.Abstractions
{
    public interface IDatabaseHandler
    {

        DbConnection CreateConnection();
        void CloseConnection(DbConnection dbConnection);
        DbCommand CreateCommand(string commandText, DbConnection connection);
        IDataAdapter CreateAdapter(DbCommand dbCommand);
        IDbDataParameter CreateParameter(DbCommand command);
    }
}
