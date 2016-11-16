using System;

namespace XCommon.Application.Cache
{
    public interface ICache
    {
        T Get<T>();

        T Get<T>(string key);

        void Put<T>(T value);

        void Put<T>(string key, T value);

        void Put<T>(T value, DateTime absoluteExpiration);

        void Put<T>(string key, T value, DateTime absoluteExpiration);

        void Put<T>(T value, TimeSpan slidingExpiration);

        void Put<T>(string key, T value, TimeSpan slidingExpiration);

        void Remove<T>();

        void Remove<T>(string key);

        void Clear();
    }
}
