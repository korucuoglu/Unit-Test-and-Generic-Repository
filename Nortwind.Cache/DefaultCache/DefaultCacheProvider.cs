using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nortwind.Cache
{
    public class DefaultCacheProvider : CacheProvider
    {
        // [1] Cache ile ilgili bazı sınıfları kullanacağımız için bu projeye System.RuntimeCaching'i dahil ettik.

        ObjectCache _cache;
        CacheItemPolicy _policy;

        public DefaultCacheProvider()
        {

            Trace.WriteLine("Cache başladı."); // Debug-Relase modda çalışır. Çıktıya log atar. 

            _cache = MemoryCache.Default;

            _policy = new CacheItemPolicy()
            {
                Priority = CacheItemPriority.NotRemovable, // silinmemesi için
                AbsoluteExpiration = DateTime.Now.AddHours(1), // 1 saat sonra yıkılır. ve otomatik olarak RemovedCallbak çalışır. bunu yazmazsak süresiz olur.
                RemovedCallback = RemovedCallback // cache yıkıldığında çalışır. 


            };
        }

        private void RemovedCallback(CacheEntryRemovedArguments arguments) // Cache yıkıldığında çalışır.
        {
            Trace.WriteLine("_________Cache Yıkıldı_________");
            Trace.WriteLine("Key:" + arguments.CacheItem.Key);
            Trace.WriteLine("Value:" + arguments.CacheItem.Value);
            Trace.WriteLine("RemovedReason" + arguments.RemovedReason);
        }

        public override object Get(string key)
        {

            object item;
            try
            {

                item = _cache.Get(key);

                if (item == null)
                {
                    Trace.WriteLine("Cache Get Sırasında Hata");
                    throw new Exception("Cache Get Sırasında Hata Oluştu");
                }
            }
            catch (Exception e)
            {

                item = null;
                Trace.WriteLine("Cache Get Sırasında Hata", e.Message);
                throw new Exception("Cache Get Sırasında Hata Oluştu", e);
            }

            return item;
        }

        public override bool IsExist(string key)
        {
            return _cache.Any(i => i.Key == key); // cahce varsa true döndürür.
        }

        public override void Remove(string key)
        {
            if (IsExist(key))
            {
                _cache.Remove(key);

            }


        }

        public override void Set(string key, object value)
        {
            _cache.Set(key, value, _policy);
        }
    }
}
