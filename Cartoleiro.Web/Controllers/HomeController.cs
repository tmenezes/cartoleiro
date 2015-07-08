using System.Web.Mvc;

namespace Cartoleiro.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }

        public ActionResult KeepAlive()
        {
            return PartialView("Error");
        }
    }
}