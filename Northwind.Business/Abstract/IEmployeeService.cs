using Northwind.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Northwind.Business.Abstract
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll(Expression<Func<Employee, bool>> filter = null);

        List<Employee> GetList();

        void CreateOrUptade(Employee model);
        void DeleteById(int id);
    }
}
