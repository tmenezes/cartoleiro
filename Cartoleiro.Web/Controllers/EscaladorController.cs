﻿using System.Web.Mvc;
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
            return View(new EscaladorViewModel());
        }

        // GET: Escalador/Escalar
        public ActionResult Escalar()
        {
            return RedirectToAction("Index");
        }

        // POST: Escalador/Escalar
        [HttpPost]
        public ActionResult Escalar(EscaladorViewModel escaladorViewModel)
        {
            if (!this.ModelState.IsValid)
                return View(escaladorViewModel);

            var escalador = new EscaladorDeTime(CartoleiroApp.CartolaDataSource)
                .ComEsquema(escaladorViewModel.EsquemaTatico)
                .ComPatrimonio(escaladorViewModel.Patrimonio)
                .DistribuirProporcionalNaPosicao(escaladorViewModel.ProporcionalNaPosicao)
                .SomenteProvaveis(escaladorViewModel.SomenteProvaveis);

            if (escaladorViewModel.PosicaoEmFoco.HasValue)
            {
                escalador.ComFocoNaPosicao(escaladorViewModel.PosicaoEmFoco.Value);
            }

            var analisadores = GetAnalisadores(escaladorViewModel);
            escalador.ComAnalisadores(analisadores);

            var time = escalador.MontarTime();
            ViewData.SetTimeEscalado(time);

            return View("Index", escaladorViewModel);
        }

        private static Analisadores GetAnalisadores(EscaladorViewModel escaladorViewModel)
        {
            var analisadorBuilder = new AnalisadorBuilder();

            if (escaladorViewModel.AnalisadorPontuacaoMedia)
            {
                analisadorBuilder.PontuacaoMedia();
            }
            if (escaladorViewModel.AnalisadorUltimaPontuacao)
            {
                analisadorBuilder.UltimaPontuacao();
            }
            if (escaladorViewModel.AnalisadorPontosNoCampeonato)
            {
                analisadorBuilder.PontosNoCampeonato();
            }
            if (escaladorViewModel.AnalisadorVitorias)
            {
                analisadorBuilder.Vitorias();
            }
            if (escaladorViewModel.AnalisadorGolsPro)
            {
                analisadorBuilder.GolsPro();
            }
            if (escaladorViewModel.AnalisadorSaldoDeGols)
            {
                analisadorBuilder.SaldoDeGols();
            }
            if (escaladorViewModel.AnalisadorUltimos5Jogos)
            {
                analisadorBuilder.Ultimos5Jogos();
            }

            return analisadorBuilder.Analisadores;
        }
    }
}