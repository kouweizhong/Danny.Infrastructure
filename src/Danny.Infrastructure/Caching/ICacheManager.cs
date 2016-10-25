using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
