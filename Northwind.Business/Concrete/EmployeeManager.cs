using Northwind.Business.Abstract;
using Northwind.DataAccesLayer.Abstract;
using Northwind.Entities.Models;

namespace Northwind.Business.Concrete
{
    public class EmployeeManager : GenericManager<Employee>, IEmployeeService // sınıftan sınıfa kalıtım verdiğimiz için implemente etmedik
    {
        IEmployee employee;

        public EmployeeManager(IEmployee employee):base(employee) // base(employee) diyerek kalıtım aldığımız yer olan GenericManager'ın ctor'una biz değişken yolladık.
        {
            this.employee = employee;
        }
    }



    //public class EmployeeManager : IEmployeeService
    //{
    //    private EmployeDAL _employeDal;
    //    private EFUnitOfWork _uow;

    //    public EmployeeManager(EmployeDAL employeDAL)
    //    {
    //        _employeDal = employeDAL;
    //    }

    //    public void CreateOrUptade(Employee model)
    //    {
    //        _employeDal.CreateOrUptade(model);
    //    }

    //    public void DeleteById(int id)
    //    {
    //        _employeDal.DeleteById(id);
    //    }

    //    public List<Employee> GetAll(Expression<Func<Employee, bool>> filter = null)
    //    {

    //        return _employeDal.GetAll(filter);
    //    }

    //    public List<Employee> GetEmployeeByInitial(string initial)
    //    {
    //        return _employeDal.GetList().Where(i => i.City.ToUpper().StartsWith(initial.ToUpper())).ToList();
    //    }

    //    public List<Employee> GetList()
    //    {
    //        return _employeDal.GetList();
    //    }
    //}
}
