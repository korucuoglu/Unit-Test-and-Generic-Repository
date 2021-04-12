using Northwind.DataAccesLayer.Abstract;
using Northwind.Entities.Models;

namespace Northwind.DataAccesLayer.Concrete
{
    public class EmployeDAL : GenericRepository<Employee>, IEmployee
    {

    }
}
