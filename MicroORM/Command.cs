using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MicroORM
{
    public class Command
    {
        public static DbCommand CreateCommand<TEntity>(DbConnection conn, string query = null, object param = null, string schema = "dbo")
        {
            var propertiesParam = Param.GetProperties(param);

            var querySql = string.IsNullOrWhiteSpace(query)
                ? Query.GetQuerySQL<TEntity>(propertiesParam, schema)
                : query;

            return GetCommand(conn, querySql, propertiesParam);
        }

        private static DbCommand GetCommand(DbConnection conn,
            string query,
            IEnumerable<PropertyParam> propertiesParam)
        {
            var command = conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = query;

            CreateParameters(command, propertiesParam);

            return command;
        }

        private static void CreateParameters(DbCommand command, IEnumerable<PropertyParam> PropertiesParam)
        {
            command.Parameters.Clear();
            command.Parameters.AddRange(GetSqlParameter(PropertiesParam, command));
        }

        private static DbParameter[] GetSqlParameter(IEnumerable<PropertyParam> PropertiesParam, DbCommand command)
        {
            var parameter = new List<DbParameter>();

            foreach (PropertyParam item in PropertiesParam)
            {
                var param = command.CreateParameter();
                param.ParameterName = item.NameParam;
                param.Value = item.Value;

                parameter.Add(param);
            }

            return parameter.ToArray();
        }

    }
}
