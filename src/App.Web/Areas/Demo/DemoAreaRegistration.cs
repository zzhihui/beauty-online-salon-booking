using System.Web.Mvc;

namespace App.Web.Areas.Demo
{
    public class DemoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Demo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Demo_default",
                "Demo/{controller}/{action}/{id}",
                 new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "App.Web.Areas.Demo.Controllers", "App.Web.Areas.Demo.Controllers.Admin", "App.Web.Areas.Demo.Controllers.Client" }
            );
        }
    }
}