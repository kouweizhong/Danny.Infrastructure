using System;
using Danny.Infrastructure.Caching;

namespace Danny.Infrastructure.Session
{
    public class CacheSessionManager:ISessionManager
    {
        private readonly ICacheManager _cacheManager;

        public CacheSessionManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public T Get<T>(string key)
        {
            var value=_cacheManager.Get<T>(key);
            if (value==null)
            {
                return default(T);
            }

            return value;
        }

        public void Set(string key, object data, int expires)
        {
            _cacheManager.Set(key,data,DateTime.Now.AddMinutes(expires));
        }

        public void Remove(string key)
        {
            _cacheManager.Remove(key);
        }
    }
}
