using System.Web.Mvc;

namespace App.Web.Application.Filter
{
    public class AreaAuthAttribute: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var areaName = filterContext.RouteData.DataTokens["area"];
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary((new { controller = "Admin", action = "Login", area = areaName })));
            }
        }
    }
}