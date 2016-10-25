using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;


namespace Danny.Infrastructure.Caching
{
    public class AspNetCacheManager:ICacheManager
    {
        public T Get<T>(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            return (T) HttpRuntime.Cache.Get(key);
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, TimeSpan.Zero);
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            HttpRuntime.Cache.Remove(key);
        }

        public void Clear()
        {
            var cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}
