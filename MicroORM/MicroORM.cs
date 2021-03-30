using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MicroORM
{
    public static class MicroORM
    {
        public static TEntity Get<TEntity>(this IDbConnection conn, object param = null) =>
            Reader.CreateObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, null, param));

        public static TEntity QueryFirst<TEntity>(this IDbConnection conn, string query, object param = null) =>
            Reader.CreateObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, query, param));

        public static IEnumerable<TEntity> GetAll<TEntity>(this IDbConnection conn, object param = null) =>
            Reader.CreateListObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, null, param));

        public static IEnumerable<TEntity> Query<TEntity>(this IDbConnection conn, string query, object param = null) =>
            Reader.CreateListObject<TEntity>(Command.CreateCommand<TEntity>((DbConnection)conn, query, param));

    }
}
