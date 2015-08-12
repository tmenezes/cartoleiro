using System.Web.Mvc;
using Cartoleiro.Core.Confronto;
using Cartoleiro.Core.Confronto.Indicador;
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

            var medidorDeConfronto = new MedidorDeConfronto(mandante, visitante, CartoleiroApp.CartolaDataSource);
            var resultadoDoConfronto = medidorDeConfronto.MedirConfronto();

            return PartialView("_ConfrontoResult", resultadoDoConfronto);
        }
    }
}