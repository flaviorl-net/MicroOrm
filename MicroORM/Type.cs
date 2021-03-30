using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MicroORM
{
    public class Type
    {
        public static IEnumerable<PropertyEntity> GetProperties<TEntity>() =>
            typeof(TEntity).GetProperties()
                .Select(x => new PropertyEntity
                {
                    Name = GetAttributeOrNameProperty(x),
                    PropertyInfo = x
                });

        private static string GetAttributeOrNameProperty(PropertyInfo property)
        {
            var attribute = property.GetCustomAttributes<ColumnAttribute>(false).FirstOrDefault();

            if (attribute == null)
            {
                return property.Name.ToLower().Trim();
            }
            else
            {
                return attribute.Name.ToLower().Trim();
            }
        }

        public static TEntity CreateEntity<TEntity>(IDataReader reader)
        {
            var entity = GetInstanceType<TEntity>();
            var properties = GetProperties<TEntity>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                foreach (PropertyEntity prop in properties)
                {
                    if (reader.GetName(i).ToLower().Trim().Equals(prop.Name))
                    {
                        SetValue(entity, reader, prop);
                        break;
                    }
                }
            }
            return entity;
        }

        private static void SetValue<TEntity>(TEntity entity, IDataReader reader, PropertyEntity property) =>
            property.PropertyInfo.SetValue(entity, reader[property.Name] != DBNull.Value ? reader[property.Name] : null);

        public static IEnumerable<string> GetNamesPropertyType<TEntity>() =>
            typeof(TEntity).GetProperties().Select(x => x.Name);
        
        public static TEntity GetInstanceType<TEntity>() =>
            (TEntity)Activator.CreateInstance(typeof(TEntity));
        

    }
}
