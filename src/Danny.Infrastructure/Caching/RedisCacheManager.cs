using System;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Danny.Infrastructure.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        public T Get<T>(string key)
        {
            // 连接字符串配置格式:
            //<add name = "RedisConnectionString" connectionString = "127.0.0.1:6379" />
            using (var redisClient = ConnectionMultiplexer.Connect(ConfigurationManager
                .ConnectionStrings["RedisConnectionString"].ConnectionString))
            {
                var db = redisClient.GetDatabase();
                var data = db.StringGet(key);
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            using (var redisClient = ConnectionMultiplexer.Connect(ConfigurationManager
                .ConnectionStrings["RedisConnectionString"].ConnectionString))
            {
                var db = redisClient.GetDatabase();
                var data = JsonConvert.SerializeObject(value);
                db.StringSet(key, data, absoluteExpiration - DateTime.Now);
            }
        }

        public void Remove(string key)
        {
            using (var redisClient = ConnectionMultiplexer.Connect(ConfigurationManager
                .ConnectionStrings["RedisConnectionString"].ConnectionString))
            {
                var db = redisClient.GetDatabase();
                db.KeyDelete(key);
            }
        }

        public void Clear()
        {
            using (var redisClient = ConnectionMultiplexer.Connect(ConfigurationManager
                .ConnectionStrings["RedisConnectionString"].ConnectionString))
            {
                foreach (var ep in redisClient.GetEndPoints())
                {
                    var server = redisClient.GetServer(ep);
                    var db = redisClient.GetDatabase();
                    var keys = server.Keys(0, "*").ToList();
                    foreach (var key in keys)
                        db.KeyDelete(key);
                }
            }
        }
    }
}