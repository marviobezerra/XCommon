using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class SystemExtensions
    {
        public static IEnumerable<MethodInfo> GetMethods(this Type someType)
        {
            var t = someType;

            while (t != null)
            {
                var ti = t.GetTypeInfo();

                foreach (var m in ti.DeclaredMethods)
                    yield return m;

                t = ti.BaseType;
            }
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type someType)
        {
            var t = someType;

            while (t != null)
            {
                var ti = t.GetTypeInfo();

                foreach (var m in ti.DeclaredProperties)
                    yield return m;

                t = ti.BaseType;
            }
        }

        public static PropertyInfo GetProperty(this Type someType, string propertyName)
        {
            return someType.GetProperties().FirstOrDefault(c => c.Name == propertyName);
        }

        public static bool CheckIsInterface(this Type someType)
        {
            return someType.GetTypeInfo().IsInterface;
        }

        public static bool CheckIsAbstract(this Type someType)
        {
            return someType.GetTypeInfo().IsAbstract;
        }
    }
}
