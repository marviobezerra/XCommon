namespace XCommon.Application.Cache
{
    public static class CacheUtils
    {
        public static string BuildFullKey<T>(this object userKey)
        {
            if (userKey == null)
                return typeof(T).FullName;

            return typeof(T).FullName + userKey;
        }
    }
}
