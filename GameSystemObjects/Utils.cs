using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GameSystemObjects
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            var type = typeof(T);
            var properties = type
                .GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
                .ToList();
            var typeHandlers = properties
                .Select(info => new { info, typeHandlerAttribute = (TypeHandlerAttribute)Attribute.GetCustomAttribute(info, typeof(TypeHandlerAttribute)) })
                .Where(x => x.typeHandlerAttribute != null)
                .ToDictionary(x => x.info, x => x.typeHandlerAttribute);

            var dataTable = new DataTable();
            foreach (var info in properties)
            {
                typeHandlers.TryGetValue(info, out var typeHandlerAttribute);

                var propertyType = typeHandlerAttribute?.ValueType
                    ?? Nullable.GetUnderlyingType(info.PropertyType)
                    ?? info.PropertyType;

                if (propertyType.IsEnum)
                    propertyType = Enum.GetUnderlyingType(propertyType);

                dataTable.Columns.Add(new DataColumn(info.Name, propertyType));
            }

            foreach (var entity in list)
            {
                var ii = 0;
                var values = new object[dataTable.Columns.Count];
                foreach (var info in properties)
                {
                    var value = info.GetValue(entity);
                    if (typeHandlers.ContainsKey(info))
                    {
                        var param = new SqlParameter();
                        var typeHandler = typeHandlers[info].TypeHandler;

                        typeHandler.SetValue(param, value);
                        values[ii++] = param.Value;
                    }
                    else
                    {
                        values[ii++] = value;
                    }
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static DataTable Empty<T>()
        {
            var type = typeof(T);
            var properties = type
                .GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
                .ToList();

            var dataTable = new DataTable();
            foreach (var info in properties)
            {
                var propertyType = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;

                if (propertyType.IsEnum)
                    propertyType = Enum.GetUnderlyingType(propertyType);

                dataTable.Columns.Add(new DataColumn(info.Name, propertyType));
            }

            return dataTable;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class TypeHandlerAttribute : Attribute
    {
        public TypeHandlerAttribute(Type handlerType, Type valueType)
        {
            TypeHandler = (SqlMapper.ITypeHandler)Activator.CreateInstance(handlerType);
            ValueType = valueType;
        }

        public SqlMapper.ITypeHandler TypeHandler { get; }

        public Type ValueType { get; }
    }
}
