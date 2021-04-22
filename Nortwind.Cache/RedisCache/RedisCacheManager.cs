using Northwind.Entities.Models;
using ServiceStack.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Data
{
    public class RedisCacheManager : CacheHelper, ICacheManager
    {
        private static IDatabase _db;
        private static readonly string host = "localhost";
        private static readonly int port = 6379;
        

        public RedisCacheManager()
        {
            CreateRedisDB();
        }

        private static IDatabase CreateRedisDB()
        {
            if (null == _db)
            {
                ConfigurationOptions option = new ConfigurationOptions();
                option.Ssl = false;
                option.EndPoints.Add(host, port);
                var connect = ConnectionMultiplexer.Connect(option);
                _db = connect.GetDatabase();
            }

            return _db;
        }

        public void Clear()
        {
            var server = _db.Multiplexer.GetServer(host, port);
            foreach (var item in server.Keys())
                _db.KeyDelete(item);
        }

        public T Get<T>(string key)
        {
            var rValue = _db.SetMembers(key);
            if (rValue.Length == 0)
                return default(T);

            var result = Deserialize<T>(rValue.ToStringArray());
            return result;
        }

        public bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        public bool Remove(string key)
        {
            return _db.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var server = _db.Multiplexer.GetServer(host, port);
            foreach (var item in server.Keys(pattern: "*" + pattern + "*"))
                _db.KeyDelete(item);
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var entryBytes = Serialize(data);
            _db.SetAdd(key, entryBytes);

            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            if (cacheTime > 0)
             _db.KeyExpire(key, expiresIn);
        }

      

    }
}


/// <summary>
/// 
/// </summary>
/// <param name="key"></param>
/// <param name="value"></param>

/*

public void SetStrings(string key, string value)
{
using (var redisClient = new RedisClient(_redisEndpoint))
{
    redisClient.SetValue(key, value);
}
}

public string GetStrings(string key, string value)
{
using (var redisClient = new RedisClient(_redisEndpoint))
{
    return redisClient.GetValue(key);
}
}

public void StoreList<T>(string key, T value, TimeSpan timeout)
{
try
{
    using (var redisClient = new RedisClient(_redisEndpoint))
    {
        redisClient.As<T>().SetValue(key, value, timeout);
    }
}
catch (Exception)
{
    throw;
}
}

public T GetList<T>(string key)
{
T result;

using (var client = new RedisClient(_redisEndpoint))
{
    var wrapper = client.As<T>();

    result = wrapper.GetValue(key);
}

return result;
}
}
}

*/

