using System.Collections.Generic;
using System.Linq;

namespace MicroORM
{
    public class Param
    {
        public static IEnumerable<PropertyParam> GetProperties(object param)
        {
            if (param == null)
                return new List<PropertyParam>();

            return param.GetType().GetProperties()
                .Select(x => new PropertyParam
                {
                    Name = x.Name,
                    NameParam = string.Concat("@", x.Name),
                    Value = x.GetValue(param)
                });
        }

    }

    public static IEnumerable<PropertyParam> GetPropertiesFromEntity<TEntity>(TEntity entity)
    {
        if (entity == null)
            return new List<PropertyParam>();

        return typeof(TEntity).GetProperties()
            .Select(x => new PropertyParam
            {
                Name = x.Name,
                NameParam = string.Concat("@", x.Name),
                Value = x.GetValue(entity)
            });
    }
}
