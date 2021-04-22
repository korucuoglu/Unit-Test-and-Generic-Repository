using Data;
using Northwind.Business.Abstract;
using Northwind.DataAccesLayer.Concrete;
using Northwind.Entities.Models;
using Nortwind.Cache.RedisCache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Northwind.Business
{
    public class Cachefonksiyon
    {
        // DefaultCacheProvider provider = new DefaultCacheProvider();

        RedisCacheManagerTwo provider = new RedisCacheManagerTwo();
        
        public void CacheTemizle()
        {

            // provider.Remove(Enums.CacheKey.Employee.ToString());

        }
        public void CacheOlustur()
        {

            RedisCacheManagerTwo provider = new RedisCacheManagerTwo();

            GenericManager<Employee> genericManager = new GenericManager<Employee>(new GenericRepository<Employee>());

            var list = genericManager.GetList();


            provider.StoreList<Employee>("Employe", list, TimeSpan.MaxValue);


        }
        public object EmployeeGetir()
        {

            object value = null;
            try
            {
                var employees = provider.GetList<Employee>(Enums.CacheKey.Employee.ToString());

                if (employees != null)
                {
                    value = employees;
                }


            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);
            }

            return value;
        }

        public object CacheOku(string key)
        {
            if (provider.IsKeyExists(key))
            {
                return provider.GetStrings(key);
            }
            return null;

        }


    }
}


