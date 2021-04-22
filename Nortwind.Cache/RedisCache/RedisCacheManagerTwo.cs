using Northwind.Entities.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace Nortwind.Cache.RedisCache
{
    public class RedisCacheManagerTwo
    {
        private readonly RedisEndpoint _redisEndpoint;
        public RedisCacheManagerTwo()
        {
           
            _redisEndpoint = new RedisEndpoint("localhost", 6379);
        }

        public bool IsKeyExists(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                if (redisClient.ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void SetStrings(string key, string value)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                redisClient.SetValue(key, value);
            }
        }

        public string GetStrings(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                return redisClient.GetValue(key);
            }
        }

        public bool StoreList<T>(string key, List<T> value, TimeSpan timeout)
        {
            try
            {
                using (var redisClient = new RedisClient(_redisEndpoint))
                {

                    for (int i = 0; i < value.Count; i++)
                    {
                        redisClient.As<T>().SetValue(key+i.ToString(), value[i], timeout);
                    }

                    
                }
                return true;
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

        public long Increment(string key)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                return client.Increment(key, 1);
            }
        }

        public long Decrement(string key)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                return client.Decrement(key, 1);
            }
        }

        public void Set(List<Employee> employee)
        {

            using (var client = new RedisClient(_redisEndpoint))
            {
                foreach (var item in employee)
                {
                    var news = client.As<Employee>();
                    news.SetValue("flag" + item.EmployeeID, item);
                }
            }

            
        }
    }

}
