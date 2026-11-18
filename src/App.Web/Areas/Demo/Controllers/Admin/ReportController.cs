using System.Web.Mvc;
using App.Web.Controllers;

namespace App.Web.Areas.Demo.Controllers.Admin
{
    public class ReportController : BaseController
    {
        // GET: AsianShop/Report
        public ActionResult Index()
        {
            return View();
        }
    }
}