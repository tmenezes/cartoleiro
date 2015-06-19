using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.Web.Models;
using Cartoleiro.Web.Models.EscaladorModels;

namespace Cartoleiro.Web.Controllers
{
    public class EscaladorController : Controller
    {
        // GET: Escalador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Escalar()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Escalar(EscaladorViewModel escaladorViewModel)
        {
            if (!this.ModelState.IsValid)
                return View(escaladorViewModel);

            var escalador = new EscaladorDeTime(CartoleiroApp.CartolaDataSource)
                .ComEsquema(escaladorViewModel.EsquemaTatico)
                .ComPatrimonio(escaladorViewModel.Patrimonio)
                .DistribuirProporcionalNaPosicao(escaladorViewModel.ProporcionalNaPosicao);

            if (escaladorViewModel.PosicaoEmFoco.HasValue)
            {
                escalador.ComFocoNaPosicao(escaladorViewModel.PosicaoEmFoco.Value);
            }

            var analisadorBuilder = new AnalisadorBuilder();
            if (escaladorViewModel.AnalisadorPontuacaoMedia)
            {
                analisadorBuilder.PontuacaoMedia();
            }
            if (escaladorViewModel.AnalisadorUltimaPontuacao)
            {
                analisadorBuilder.UltimaPontuacao();
            }
            escalador.ComAnalisadores(analisadorBuilder.Analisadores);

            var time = escalador.MontarTime();
            ViewData.SetTimeEscalado(time);

            return View("Index", escaladorViewModel);
        }
    }
}