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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detalhe(string id)
        {
            var clube = CartoleiroApp.CartolaDataSource.Clubes.FirstOrDefault(c => c.GetNomeNormalizado() == id);
            if (clube == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var descricao = CartoleiroApp.GetDescricaoDoClube(clube);
            var titulares = clube.JogadoresTitulares(CartoleiroApp.CartolaDataSource);
            var elenco = clube.Elenco(CartoleiroApp.CartolaDataSource);

            ViewData.SetDescricaoDoClube(descricao);
            ViewData.SetTitularesDoClube(titulares);
            ViewData.SetElencoDoClube(elenco);

            return View(clube);
        }
    }
}