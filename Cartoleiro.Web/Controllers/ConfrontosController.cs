using System.Web.Mvc;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Confronto.Indicador;
using Cartoleiro.Core.Confronto.Probabilidade;
using Cartoleiro.Web.AppCode;

namespace Cartoleiro.Web.Controllers
{
    public class ConfrontosController : Controller
    {
        // GET: Confrontos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnalisarConfronto(string id)
        {
            var mandante = ModelUtils.GetMandande(id);
            var visitante = ModelUtils.GetVisitante(id);

            if (mandante == null || visitante == null)
            {
                return PartialView("_ConfrontoResult", null);
            }

            var calculadorDeIndicadores = new CalculadorDeIndicadores(mandante, visitante, CartoleiroApp.CartolaDataSource);
            var resultadoDoConfronto = calculadorDeIndicadores.CalcularConfronto();

            return PartialView("_ConfrontoResult", resultadoDoConfronto);
        }

        public ActionResult DetalheConfronto(string id)
        {
            var mandante = ModelUtils.GetMandande(id);
            var visitante = ModelUtils.GetVisitante(id);

            if (mandante == null || visitante == null)
            {
                return PartialView("_DetalheConfrontoResult", null);
            }

            var jogo = new Jogo(0, mandante, visitante);
            var resultadoDeProbabilidade = new ProbabilidadeDeResultado(jogo);

            return PartialView("_DetalheConfrontoResult", resultadoDeProbabilidade);
        }
    }
}