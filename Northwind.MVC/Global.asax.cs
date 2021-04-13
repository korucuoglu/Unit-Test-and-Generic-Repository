using Northwind.Business;
using Northwind.MVC.Controllers;
using Nortwind.Cache;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Northwind.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            CacheProvider.Instance = new DefaultCacheProvider(); // Cache'i oluþturmamýzý saðlar. 
            Cachefonksiyon fonksiyon = new Cachefonksiyon();

            // fonksiyon.CacheTemizle();
            fonksiyon.CacheOlustur();

        }
    }
}
