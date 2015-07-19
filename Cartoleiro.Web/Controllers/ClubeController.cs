using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cartoleiro.Web.AppCode;
using Cartoleiro.Web.AppCode.Extensions;
using Cartoleiro.Web.AppCode.MvcHelpers;

namespace Cartoleiro.Web.Controllers
{
    public class ClubeController : Controller
    {
        // GET: Clube
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalhe(string id)
        {
            var clube = CartoleiroApp.CartolaDataSource.Clubes.FirstOrDefault(c => c.GetNomeNormalizado() == id);
            if (clube == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var titulares = clube.JogadoresTitulares(CartoleiroApp.CartolaDataSource);
            var elenco = clube.Elenco(CartoleiroApp.CartolaDataSource);

            ViewData.SetDescricaoDoClube("uma descricao qualquer...");
            ViewData.SetTitularesDoClube(titulares);
            ViewData.SetElencoDoClube(elenco);

            return View(clube);
        }
    }
}