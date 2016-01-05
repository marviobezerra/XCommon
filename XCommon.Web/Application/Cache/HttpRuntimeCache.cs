using XCommon.Application.Cache;
using System;
using System.Web;

namespace XCommon.Web.Application.Cache
{
    public class HttpRuntimeCache : ICache
    {
        public T Get<T>()
        {
            return Get<T>(null);
        }

        public T Get<T>(object key)
        {
            return (T)HttpRuntime.Cache.Get(key.BuildFullKey<T>());
        }

        public T GetOrPut<T>(Func<T> ifEmpty)
        {
            return GetOrPut<T>(null, ifEmpty);
        }

        public T GetOrPut<T>(object key, Func<T> ifEmpty)
        {
            var result = (T)HttpRuntime.Cache.Get(key.BuildFullKey<T>());

            if (result != null)
                return result;

            result = ifEmpty();
            Put<T>(key, result);
            return result;
        }

        public void Put<T>(T instance)
        {
            Put(null, instance);
        }

        public void Put<T>(object key, T instance)
        {
            HttpRuntime.Cache.Insert(key.BuildFullKey<T>(), instance);
        }

        public void Put<T>(T instance, DateTime absoluteExpiration)
        {
            Put(null, instance, absoluteExpiration);
        }

        public void Put<T>(object key, T instance, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key.BuildFullKey<T>(), instance, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public void Put<T>(T instance, TimeSpan slidingExpiration)
        {
            Put(null, instance, slidingExpiration);
        }

        public void Put<T>(object key, T instance, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key.BuildFullKey<T>(), instance, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public void Remove<T>()
        {
            Remove<T>(null);
        }

        public void Remove<T>(object key)
        {
            HttpRuntime.Cache.Remove(key.BuildFullKey<T>());
        }

        public void Clear()
        {

        }
    }
}
