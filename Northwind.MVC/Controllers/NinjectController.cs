using Ninject;
using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.DataAccesLayer.Concrete;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Northwind.MVC.Controllers
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {

        private readonly IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBllBindings();
        }

        private void AddBllBindings()
        {
            ninjectKernel.Bind<IEmployeeService>()
                .To<EmployeeManager>()
                .WithConstructorArgument("employee",
                    new EmployeDAL());

        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}
