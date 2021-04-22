using Northwind.Business.Abstract;
using Northwind.Entities.Models;
using ServiceStack.Redis;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Northwind.MVC.Controllers
{


    public class HomeController : Controller
    {
        public static List<Employee> employee = new List<Employee>();
        RedisClient redisClient = new RedisClient(new RedisEndpoint("localhost", 6379));

        private IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {

            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            if (redisClient.SearchKeys("Emp*").Count > 0) // Cache'de varsa oradan gelsin.
            {
                List<string> allKeys = redisClient.SearchKeys("Emp*");
                foreach (string key in allKeys)
                {
                    employee.Add(redisClient.Get<Employee>(key));
                }
            }
            else // Cache'de yoksa doldurulsun.
            {
                employee.AddRange(new Employee[] {

                new Employee(){ EmployeeID=1, City="1Şehir", Address="1"},
                new Employee(){ EmployeeID=2, City="2Şehir", Address="2"},
                new Employee(){ EmployeeID=3, City="3Şehir", Address="3"},
                new Employee(){ EmployeeID=4, City="4Şehir", Address="4"},
                new Employee(){ EmployeeID=5, City="5Şehir", Address="5"},
                new Employee(){ EmployeeID=6, City="6Şehir", Address="6"}
            });

                foreach (var data in employee)
                {
                    var emp = redisClient.As<Employee>();
                    emp.SetValue("Emp" + data.EmployeeID, data);
                }
            }

            return View(employee);

        }

        public ActionResult Details(int id)
        {
            var data = redisClient.Get<Employee>($"Emp{id}");
            data = data == null ? new Employee() : data;
            return View(data);

        }


    }
}