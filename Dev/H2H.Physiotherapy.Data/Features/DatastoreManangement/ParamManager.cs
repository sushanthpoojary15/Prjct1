
using System;
using System.Data;
using H2H.Physiotherapy.Data.Abstractions;
using Microsoft.Data.SqlClient;

namespace H2H.Physiotherapy.Data.Features.DatastoreManangement
{
    public class ParamManager : IParamManager
    {
        public IDbDataParameter CreateParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = parameterDirection,
                Value = value ?? DBNull.Value
            };
        }

        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = parameterDirection,
                Value = value ?? DBNull.Value
            };
        }

        public IDbDataParameter CreateParameter(string name, object value, SqlDbType sqlDbType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            return new SqlParameter
            {
                SqlDbType = sqlDbType,
                ParameterName = name,
                Direction = parameterDirection,
                Value = value ?? DBNull.Value
            };
        }
    }
}
