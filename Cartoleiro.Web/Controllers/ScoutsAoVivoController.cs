using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Web.AppCode.Extensions;
using Cartoleiro.Web.AppCode.ScoutsAoVivo;

namespace Cartoleiro.Web.Controllers
{
    public class ScoutsAoVivoController : Controller
    {
        // GET: ScoutsAoVivo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScoutsJogo(string id)
        {
            var scouts = ScoutsAoVivoFacade.ObterScoutsAoVivo(id);
            var scoutsMatch = (scouts != null)
                ? scouts.ScoutsMatch
                : null;

            return PartialView("_ScoutsPartida", scoutsMatch);
        }

        public ActionResult ScoutsResultados()
        {
            var scouts = ScoutsAoVivoFacade.Scouts;
            if (scouts == null)
            {
                Task.Factory.StartNew(() => ScoutsAoVivoFacade.ObterScoutsAoVivo(Campeonato.Rodadas.ProximaRodada.Jogos.First().GetIdJogo()));
            }

            var scoutsResults = (scouts != null)
                ? scouts.FixtureMatches
                : null;

            return PartialView("_ListaJogos", scoutsResults);
        }
    }
}