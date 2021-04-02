using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.DataAccesLayer.Concrete;
using Northwind.Entities.Models;

namespace Northwind.Test
{
    [TestClass]
    public class DataAccesLayerTest
    {


        Mock<EmployeDAL> _mockEmployeDal; // [4] Dal'ın fake halini burada verdik. 
        List<Employee> _dbEmployees;

        [TestInitialize]
        public void Start()
        {

            _mockEmployeDal = new Mock<EmployeDAL>(); // [5] Dal'ın fake halini testin her başlangıcında çalıştırdık. 



            _dbEmployees = new List<Employee> { // [7] burada bir tane Emplloye listesi türettik. Biz veritabanı ile değil bu liste ile testlerimizi yapacağız. 
               
                new Employee { EmployeeID=1, City="İstanbul", Country="Turkey" },
                new Employee { EmployeeID=2, City="London", Country="UK" },
                new Employee { EmployeeID=3, City="Washington", Country="USA" },
                new Employee { EmployeeID=4, City="London", Country="UK" },

            };


            _mockEmployeDal.Setup(i => i.GetList()).Returns(_dbEmployees);

            // [8] Burada ise Mock aracılığıyla Setup yaptık ve EmployeDal'ın GetAll fonksiyonunu çağırdığımızda bize _dbEmployees listesini vermesini istedik. 


            ;
        }

        [TestMethod]
        public void Tum_SehirlerListelenir()
        {

            #region Arange

            /*
             * [1] Burada EmployeeManager() bizden bir Dal beklemekte. Fakat bizde bu yok. Buunun için Moq Frameworkü kurduk. 
             * [2] Moq ile biz fake bir veritabanı nesnesi yaratabilip onu atabiliyoruz. 
             * [3] Bunun için TestInıtialize attribute'sini kulllanarak biz Start() yazdık. Burada tüm testler başlamadan önce çalışacak kodlar yer alır. 
             * 
             */

            IEmployeeService employeService = new EmployeeManager(_mockEmployeDal.Object);

            // [6] Mock ile artık elimizde bir dal var ve biz bunu verdik. 


            #endregion


            #region Act

            IEnumerable<Employee> employees = employeService.GetList(); // [9] burada EmployeServis kısmındaki GetList'i çağırdık. 



            #endregion

            #region Assert

            Assert.AreEqual(4, employees.Count());
            // [10] Burada da employees listesinin 4 tane ürün vereceğini belirttik. Eğer bu koşul sağlanmazsa testimiz hata verir. 
            // [11] - Invalid setup on a non-overridable member hatası almıştık. Bunun için GenericRepository'de yer alan GetList ve GetAll metodunu virtual yaptık. 

            #endregion




        }


        [TestMethod]
        public void L_HarfiyleBaslayanSehirlerListelenir()
        {
            IEmployeeService employeService = new EmployeeManager(_mockEmployeDal.Object);
            List<Employee> employees = employeService.GetEmployeeByInitial("L");
            Assert.AreEqual(2, employees.Count());
        }


    }
}
