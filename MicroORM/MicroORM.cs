using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MicroORM
{
    public static class MicroORM
    {
        public static TEntity Get<TEntity>(this IDbConnection conn, object param = null, string schema = "dbo") =>
            Reader.CreateObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, null, param, schema));

        public static TEntity QueryFirst<TEntity>(this IDbConnection conn, string query, object param = null, string schema = "dbo") =>
            Reader.CreateObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, query, param, schema));

        public static IEnumerable<TEntity> GetAll<TEntity>(this IDbConnection conn, object param = null, string schema = "dbo") =>
            Reader.CreateListObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, null, param, schema));

        public static IEnumerable<TEntity> Query<TEntity>(this IDbConnection conn, string query, object param = null, string schema = "dbo") =>
            Reader.CreateListObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, query, param, schema));

    }
}
