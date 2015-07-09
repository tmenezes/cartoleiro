using System.Web.Mvc;
using Cartoleiro.Web.AppCode;

namespace Cartoleiro.Web.Controllers
{
    public class ScoutsAoVivoController : Controller
    {
        // GET: ScoutsAoVivo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScoutsPartida(string idPartida)
        {
            var scouts = ScoutsOnLineFacade.ObterScoutsOnLine(idPartida);
            var scoutsMatch = (scouts != null)
                ? scouts.ScoutsMatch
                : null;

            return PartialView("_ScoutsPartida", scoutsMatch);
        }
    }
}