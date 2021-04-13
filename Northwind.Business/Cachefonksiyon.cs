using Northwind.Business.Abstract;
using Northwind.DataAccesLayer.Concrete;
using Northwind.Entities.Models;
using Nortwind.Cache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace Northwind.Business
{
    public class Cachefonksiyon
    {
        DefaultCacheProvider provider = new DefaultCacheProvider();

        public void CacheTemizle()
        {

            provider.Remove(Enums.CacheKey.Employee.ToString());

        }

        public void CacheOlustur()
        {

            #region Employee
            object employeeCache = null;
            try
            {
                GenericManager<Employee> genericManager = new GenericManager<Employee>(new GenericRepository<Employee>());

                var kategori = genericManager.GetList();

                if (kategori != null)
                    employeeCache = kategori;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Employee.ToString(), employeeCache);
            #endregion

        }

        public object EmployeeGetir()
        {

            object value = null;
            try
            {
                var employees = (List<Employee>)provider.Get(Enums.CacheKey.Employee.ToString());

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
    }
}


