using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Northwind.DataAccesLayer.Abstract
{
    public interface IRepository<T>: IDisposable where T:class
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        List<T> GetList();
        void CreateOrUptade(T model);
        void DeleteById(int id);

        T GetById(int id);

    }
}
