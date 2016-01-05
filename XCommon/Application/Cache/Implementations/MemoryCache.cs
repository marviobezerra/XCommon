using System;
using System.Collections;
using System.Collections.Generic;

namespace XCommon.Application.Cache.Implementations
{
    public class MemoryCache : ICache
    {
        readonly static object _syncRoot = new object();
        readonly Dictionary<string, object> _applicationState = new Dictionary<string, object>();

        public T Get<T>()
        {
            return Get<T>(null);
        }

        public T Get<T>(object key)
        {
            lock (_syncRoot)
                return (T)_applicationState[key.BuildFullKey<T>()];
        }

        public T GetOrPut<T>(Func<T> ifEmpty)
        {
            return GetOrPut<T>(null, ifEmpty);
        }

        public T GetOrPut<T>(object key, Func<T> ifEmpty)
        {
            var result = Get<T>(key);

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
            lock (_syncRoot)
                _applicationState[key.BuildFullKey<T>()] = instance;
        }

        public void Put<T>(T instance, DateTime absoluteExpiration)
        {
            Put(null, instance);
        }

        public void Put<T>(object key, T instance, DateTime absoluteExpiration)
        {
            lock (_syncRoot)
                _applicationState[key.BuildFullKey<T>()] = instance;
        }

        public void Put<T>(T instance, TimeSpan slidingExpiration)
        {
            Put(null, instance);
        }

        public void Put<T>(object key, T instance, TimeSpan slidingExpiration)
        {
            lock (_syncRoot)
                _applicationState[key.BuildFullKey<T>()] = instance;
        }

        public void Remove<T>()
        {
            Remove<T>(null);
        }

        public void Remove<T>(object key)
        {
            lock (_syncRoot)
                _applicationState.Remove(key.BuildFullKey<T>());
        }

        public void Clear()
        {
            lock (_syncRoot)
                _applicationState.Clear();
        }
    }
}
