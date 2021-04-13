using Northwind.DataAccesLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Northwind.Business.Abstract
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IRepository<T> repository; 
        
        // yapıyı generic yapabilmek için Generic Repository ile haberleştirdik.
        // readonly tanımlamak bize performansta fayda sağlar. 

        public GenericManager(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public void CreateOrUptade(T model)
        {
            repository.CreateOrUptade(model);
        }

        public void DeleteById(int id)
        {
            repository.DeleteById(id);
        }

        public void Dispose()
        {
            // Bu bize IGenericService'den türettiğimiz Disposable'dan geldi.
            // Biz bu sayede nesneyi öldürebilir ve daha performanslı bir kullanım sağlar.


            Dispose(true);
            GC.SuppressFinalize(this);


        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
        }



        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return repository.GetAll(filter);
        }

        public List<T> GetList()
        {
            return repository.GetList();
        }
    }
}
