using System.Web.Mvc;

namespace App.Web.Areas.JoinUs
{
    public class JoinUsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "JoinUs";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "JoinUs_default",
                "JoinUs/{controller}/{action}/{id}",
                 new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "App.Web.Areas.JoinUs.Controllers", "App.Web.Areas.JoinUs.Controllers.Admin", "App.Web.Areas.JoinUs.Controllers.Client" }
            );
        }
    }
}