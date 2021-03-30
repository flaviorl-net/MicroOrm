using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MicroORM
{
    public class Reader
    {
        public static TEntity CreateObject<TEntity>(DbCommand command)
        {
            command.Connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Type.CreateEntity<TEntity>(reader);
                }
            }

            return Type.GetInstanceType<TEntity>();
        }

        public static IEnumerable<TEntity> CreateListObject<TEntity>(DbCommand command)
        {
            var list = new List<TEntity>();

            command.Connection.Open();
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(Type.CreateEntity<TEntity>(reader));
                }
            }

            return list;
        }

    }
}
