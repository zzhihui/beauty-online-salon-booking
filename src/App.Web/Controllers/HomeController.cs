using System.Configuration;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
         
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "JoinUs" });
            //return View();
        }

        public ActionResult Home()
        {
            return PartialView();
        }
    }
}
