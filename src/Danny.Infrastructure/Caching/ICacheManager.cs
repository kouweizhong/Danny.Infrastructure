using System;

namespace Danny.Infrastructure.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        void Set(string key, object value, DateTime absoluteExpiration);

        void Remove(string key);

        void Clear();
    }
}
