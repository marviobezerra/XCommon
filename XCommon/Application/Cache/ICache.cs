using System;

namespace XCommon.Application.Cache
{
    public interface ICache
    {
        T Get<T>();
        T GetOrPut<T>(Func<T> ifEmpty);
        T Get<T>(object key);
        T GetOrPut<T>(object key, Func<T> ifEmpty);
        void Put<T>(T instance);
        void Put<T>(object key, T instance);
        void Put<T>(T instance, DateTime absoluteExpiration);
        void Put<T>(object key, T instance, DateTime absoluteExpiration);
        void Put<T>(T instance, TimeSpan slidingExpiration);
        void Put<T>(object key, T instance, TimeSpan slidingExpiration);
        void Remove<T>();
        void Remove<T>(object key);
        void Clear();
    }
}
