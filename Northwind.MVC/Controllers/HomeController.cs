using Northwind.Business;
using Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.MVC.Controllers
{
    public class HomeController : Controller
    {

        private IEmployeeService _employeeService;
       
        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
         
        }


        public ActionResult Index()
        {

            Cachefonksiyon cachefonksiyon = new Cachefonksiyon();

            var data = cachefonksiyon.EmployeeGetir();


            return View(data);
        }

        
    }
}