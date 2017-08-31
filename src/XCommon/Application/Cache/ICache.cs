using System;

namespace XCommon.Application.Cache
{
    public interface ICache
    {
        TEntity Get<TEntity>();

        TEntity Get<TEntity>(string key);

        TEntity GetAndRemove<TEntity>(string key);

		void Put<TEntity>(TEntity value);

        void Put<TEntity>(string key, TEntity value);

        void Put<TEntity>(TEntity value, DateTime absoluteExpiration);

        void Put<TEntity>(string key, TEntity value, DateTime absoluteExpiration);

        void Put<TEntity>(TEntity value, TimeSpan slidingExpiration);

        void Put<TEntity>(string key, TEntity value, TimeSpan slidingExpiration);

        void Remove<TEntity>();

        void Remove<TEntity>(string key);

        void Clear();
    }
}
