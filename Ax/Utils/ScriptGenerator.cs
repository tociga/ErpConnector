using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace ErpConnector.Ax.Utils
{
    public class ScriptGenerator
    {
        Type _type;
        List<PropertyInfo> _properties;
        public ScriptGenerator(Type type)
        {
            _type = type;
            _properties = new List<PropertyInfo>();
            var p = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].PropertyType.IsValueType || p[i].PropertyType == typeof(String))
                {
                    _properties.Add(p[i]);
                    //nameLookup[p[i].Name] = i;
                }
            }
        }

        public string CreateScript(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CREATE TABLE " +tableName+ "(");
            for(int i = 0; i<_properties.Count;i++)
            {                
                sb.AppendLine("[" + _properties[i].Name + "] " + TypeToDBType(_properties[i].PropertyType) + " NULL");
                if (i < _properties.Count -1)
                {
                    sb.Append(",");
                }
            }
            sb.AppendLine(") ON [PRIMARY]");
            sb.AppendLine("GO");
            return sb.ToString();
        }

        private string TypeToDBType(Type type)
        {
            Type underLyingType =  Nullable.GetUnderlyingType(type);
            if (underLyingType != null)
            {
                type = underLyingType;
            }

            if (type == typeof(String))
            {
                return "[nvarchar](500)";
            }
            else if (type == typeof(int))
            {
                return "[int]";
            }
            else if (type == typeof(long))
            {
                return "[bigint]";
            }
            else if (type == typeof(decimal))
            {
                return "[numeric] (32,16)";
            }
            else if (type == typeof(DateTimeOffset) || type == typeof(DateTime))
            {
                return "[datetime]";
            }
            else if (type.IsEnum)
            {
                return "[int]";
            }
            else
            {
                return "[nvarchar](500)";
            }
        }
    }
}
