using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Northwind.Business.Abstract
{
    public interface IGenericService<T>: IDisposable
    {
        // IDisposable bir Interface'dir. Biz bu sayede Inferface'yi Interface'ye kalıtım vermiş olduk. 

        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        List<T> GetList();

        void CreateOrUptade(T model);
        void DeleteById(int id);


    }
}
