using System;
using System.Linq;

namespace XCommon.Patterns.Repository.Entity
{
    public static class FilterBaseExtension
    {
        public static bool IdValid<T>(this T filtro)
            where T : FilterBase
        {
            return filtro.Id != null && filtro.Id.Value != Guid.Empty;
        }

        public static bool IdsValid<T>(this T filtro)
            where T : FilterBase
        {
            return filtro.Ids != null && filtro.Ids.Where(c => c != Guid.Empty).Count() > 0;
        }

        public static bool IsNull(this string valor)
        {
            return string.IsNullOrEmpty(valor);
        }

        public static bool IsNotNull(this string valor)
        {
            return !string.IsNullOrEmpty(valor);
        }
    }
}
