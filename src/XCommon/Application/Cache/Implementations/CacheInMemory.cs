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

        public TEntity Get<TEntity>()
            => Get<TEntity>(null);

        public TEntity Get<TEntity>(string key)
        {
            var fullKey = key.BuildFullKey<TEntity>();

            lock (LockObject)
            {
                if (Cache.TryGetValue(fullKey, out Pair<DateTime, object> value))
                {
                    if (DateTime.Now > value.Item1)
                    {
                        Cache.Remove(fullKey);
                        return default(TEntity);
                    }

                    return (TEntity)value.Item2;
                }
            }

            return default(TEntity);
        }

		public TEntity GetAndRemove<TEntity>(string key)
		{
			var result = Get<TEntity>(key);
			Remove<TEntity>(key);
			return result;
		}

		public void Put<TEntity>(TEntity value)
            => Put(null, value, DateTime.MaxValue);

        public void Put<TEntity>(string key, TEntity value)
            => Put(key, value, DateTime.MaxValue);

        public void Put<TEntity>(TEntity value, TimeSpan slidingExpiration)
            => Put(null, value, DateTime.Now.Add(slidingExpiration));

        public void Put<TEntity>(TEntity value, DateTime absoluteExpiration)
            => Put(null, value, absoluteExpiration);

        public void Put<TEntity>(string key, TEntity value, TimeSpan slidingExpiration)
            => Put(key, value, DateTime.Now.Add(slidingExpiration));

        public void Put<TEntity>(string key, TEntity value, DateTime absoluteExpiration)
        {
            lock (LockObject)
			{
				Cache[key.BuildFullKey<TEntity>()] = new Pair<DateTime, object>(absoluteExpiration, value);
			}
		}

        public void Remove<TEntity>()
            => Remove<TEntity>(null);

        public void Remove<TEntity>(string key)
        {
            lock (LockObject)
			{
				Cache.Remove(key.BuildFullKey<TEntity>());
			}
		}
    }
}
