using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this List<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }
    }
}
