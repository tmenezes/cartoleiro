﻿using System;
using System.Web.Mvc;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.Web.AppCode;
using Cartoleiro.Web.AppCode.MvcHelpers;
using Cartoleiro.Web.Models.EscaladorModels;

namespace Cartoleiro.Web.Controllers
{
    public class EscaladorController : Controller
    {
        // GET: Escalador
        public ActionResult Index()
        {
            return CartoleiroApp.CampeonatoIniciado
                ? View(new EscaladorViewModel())
                : View("AguardandoInicioDoCampeonato");
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
            if (!CartoleiroApp.CampeonatoIniciado)
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
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
            if (escaladorViewModel.MediaMaiorQue.HasValue)
            {
                escalador.ComMediaMaiorQue(escaladorViewModel.MediaMaiorQue.Value);
            }
            if (escaladorViewModel.JogosMaiorQue.HasValue)
            {
                escalador.ComQtdeJogosMaiorQue(escaladorViewModel.JogosMaiorQue.Value);
            }

            var analisadores = GetAnalisadores(escaladorViewModel);
            escalador.ComAnalisadores(analisadores);

            try
            {
                var time = escalador.MontarTime();
                ViewData.SetTimeEscalado(time);
            }
            catch (Exception ex)
            {
                ViewData.SetErro(ex.Message);
            }

            return View("Index", escaladorViewModel);
        }


        // privados
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
            if (escaladorViewModel.AnalisadorScoutsPorPosicao)
            {
                analisadorBuilder.ScoutsPorPosicao();
            }
            if (escaladorViewModel.AnalisadorScoutsPositivos)
            {
                analisadorBuilder.ScoutsPositivos();
            }
            if (escaladorViewModel.AnalisadorScoutsNegativos)
            {
                analisadorBuilder.ScoutsNegativos();
            }

            if (escaladorViewModel.AnalisadorUltimos5Jogos)
            {
                analisadorBuilder.Ultimos5Jogos();
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
            if (escaladorViewModel.AnalisadorGolsContra)
            {
                analisadorBuilder.GolsContra();
            }
            if (escaladorViewModel.AnalisadorSaldoDeGols)
            {
                analisadorBuilder.SaldoDeGols();
            }

            if (escaladorViewModel.AnalisadorPesoDoClube)
            {
                analisadorBuilder.PesoDoClube();
            }
            if (escaladorViewModel.AnalisadorAproveitamentoPorMando)
            {
                analisadorBuilder.AproveitamentoNoMando();
            }
            if (escaladorViewModel.AnalisadorGolsProPorMando)
            {
                analisadorBuilder.GolsProPorMando();
            }
            if (escaladorViewModel.AnalisadorHistoricoNoConfronto)
            {
                analisadorBuilder.HistoricoDoConfronto();
            }

            return analisadorBuilder.Analisadores;
        }
    }
}