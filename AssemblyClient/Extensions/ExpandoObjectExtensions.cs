using System.Linq;
using System.Dynamic;
using System.Collections.Generic;

namespace AssemblyClient
{
    public static class ExpandoObjectExtensions
    {
        public static string ToParams(this ExpandoObject me)
        {
            var x = (IDictionary<string, object>)me;
            var valueArgs = x.Where(v => v.Value != null);
            var args = valueArgs.Select(AsQueryParam).ToArray();
            var result = string.Join("&", args);
            return result;
        }

        private static string AsQueryParam(KeyValuePair<string, object> kvp)
        {
            var result = $"{kvp.Key}=";
            if (kvp.Value is bool?)
            {
                var boolValue = kvp.Value as bool?;
                result += boolValue.HasValue && boolValue.Value ? "1" : "0";
            }
            else
            {
                result += $"{kvp.Value}";
            }

            return result;
        }

        public static object V(this ExpandoObject me, string propertyName)
        {
            return ((IDictionary<string, object>)me)[propertyName];
        }

        public static T V<T>(this ExpandoObject me, string propertyName)
        {
            return (T)((IDictionary<string, object>)me)[propertyName];
        }

        public static bool HasKey(this ExpandoObject me, string propertyName)
        {
            return ((IDictionary<string, object>)me).ContainsKey(propertyName);
        }
    }
}
