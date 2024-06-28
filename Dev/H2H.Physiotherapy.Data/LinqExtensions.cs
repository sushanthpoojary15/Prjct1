using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace H2H.Physiotherapy.Data
{
    public static class LinqExtensions
    {
        public static List<T> ToList<T>(this DataTable dataTable) where T : class
        {
            List<T> data = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }

            return data;
        }

        private static T GetItem<T>(DataRow row) where T : class
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                foreach (PropertyInfo propertyInfo in temp.GetProperties())
                {
                    if (propertyInfo.Name.Equals(column.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        propertyInfo.SetValue(obj, DBNull.Value.Equals(row[column.ColumnName]) ? null : row[column.ColumnName], null);
                        break;
                    }
                    else
                        continue;
                }
            }

            return obj;
        }

        public static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }
}
