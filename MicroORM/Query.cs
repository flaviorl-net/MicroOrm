using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MicroORM
{
    public class Query
    {
        public static string GetQuerySQL<TEntity>(IEnumerable<PropertyParam> PropertiesParam)
        {
            string select = GetSelectSQL<TEntity>();
            string where = GetWhereSQL(PropertiesParam);

            return string.Concat(select, where);
        }

        private static string GetSelectSQL<TEntity>() =>
            $" select {GetColumnsSQL<TEntity>()} from {GetTableSQL<TEntity>()} with(nolock) ";

        private static string GetWhereSQL(IEnumerable<PropertyParam> PropertiesParam)
        {
            string where = PropertiesParam.Count() == 0 ? string.Empty : " where ";
            return $"{where}{string.Join(" and ", GetConditionalWhere(PropertiesParam))}";
        }

        private static string GetColumnsSQL<TEntity>() => string.Join(", ", Type.GetNamesPropertyType<TEntity>());
        
        private static string GetTableSQL<TEntity>() => typeof(TEntity).GetTypeInfo().Name;
        
        private static IEnumerable<string> GetConditionalWhere(IEnumerable<PropertyParam> PropertiesParam) =>
            PropertiesParam.Select(x => x.Name + " = " + x.NameParam);

    }
}
