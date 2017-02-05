namespace XCommon.Application.Cache.Implementations
{
    internal static class CacheUtils
    {
        internal static string BuildFullKey<T>(this object userKey)
        {
            if (userKey == null)
                return typeof(T).FullName;

            return typeof(T).FullName + userKey;
        }
    }
}
