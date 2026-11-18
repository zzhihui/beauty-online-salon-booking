using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "App.Web.Controllers" }

            );

            routes.MapRoute(
                "Customer",                                           
                "List",                            
                new { controller = "ManageCustomer", action = "List" }  
            );

            //routes.MapRoute(
            //    "Customer",
            //    "Edit/{id}",
            //    new { controller = "ManageCustomer", action = "Edit" }
            //);
        }
    }
}
