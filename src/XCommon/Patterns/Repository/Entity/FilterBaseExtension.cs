using System;
using System.Linq;

namespace XCommon.Patterns.Repository.Entity
{
    public static class FilterBaseExtension
    {
        public static bool IdValid<T>(this T filtro)
            where T : FilterBase
        {
            return filtro.Key != null && filtro.Key.Value != Guid.Empty;
        }

        public static bool IdsValid<T>(this T filtro)
            where T : FilterBase
        {
            return filtro.Keys != null && filtro.Keys.Where(c => c != Guid.Empty).Count() > 0;
        }        
    }
}
