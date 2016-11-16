using System;
using System.Collections.Generic;
using XCommon.Util;

namespace XCommon.Application.Cache.Implementations
{
    public class CacheInMemory : ICache
    {
        public CacheInMemory()
        {
            LockObject = new object();
            Cache = new Dictionary<string, Pair<DateTime, object>>();
        }

        private static object LockObject { get; set; }

        private Dictionary<string, Pair<DateTime, object>> Cache { get; set; }

        public void Clear()
        {
            Cache.Clear();
        }

        public T Get<T>()
            => Get<T>(null);

        public T Get<T>(string key)
        {
            string fullKey = key.BuildFullKey<T>();
            Pair<DateTime, object> value;

            lock (LockObject)
            {
                if (Cache.TryGetValue(fullKey, out value))
                {
                    if (DateTime.Now > value.Item1)
                    {
                        Cache.Remove(fullKey);
                        return default(T);
                    }

                    return (T)value.Item2;
                }
            }

            return default(T);
        }

        public void Put<T>(T value)
            => Put(null, value, DateTime.MaxValue);

        public void Put<T>(string key, T value)
            => Put(key, value, DateTime.MaxValue);

        public void Put<T>(T value, TimeSpan slidingExpiration)
            => Put(null, value, DateTime.Now.Add(slidingExpiration));

        public void Put<T>(T value, DateTime absoluteExpiration)
            => Put(null, value, absoluteExpiration);

        public void Put<T>(string key, T value, TimeSpan slidingExpiration)
            => Put(key, value, DateTime.Now.Add(slidingExpiration));

        public void Put<T>(string key, T value, DateTime absoluteExpiration)
        {
            lock (LockObject)
                Cache[key.BuildFullKey<T>()] = new Pair<DateTime, object>(absoluteExpiration, value);
        }

        public void Remove<T>()
            => Remove<T>(null);

        public void Remove<T>(string key)
        {
            lock (LockObject)
                Cache.Remove(key.BuildFullKey<T>());
        }
    }
}
