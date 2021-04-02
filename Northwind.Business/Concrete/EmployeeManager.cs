using Northwind.Business.Abstract;
using Northwind.DataAccesLayer.Concrete;
using Northwind.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Northwind.Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private EmployeDAL _employeDal;
        
        public EmployeeManager(EmployeDAL employeDAL)
        {
            _employeDal = employeDAL;
        }

        public void CreateOrUptade(Employee model)
        {
            _employeDal.CreateOrUptade(model);
        }

        public void DeleteById(int id)
        {
            _employeDal.DeleteById(id);
        }

        public IEnumerable<Employee> GetAll(Expression<Func<Employee, bool>> filter = null)
        {
            return _employeDal.GetAll(filter);
        }

        public List<Employee> GetList()
        {
            return _employeDal.GetList();        
        }
    }
}
