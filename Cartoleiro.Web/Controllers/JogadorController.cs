using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Web.AppCode;
using Cartoleiro.Web.Models.JogadorModels;

namespace Cartoleiro.Web.Controllers
{
    public class JogadorController : Controller
    {
        // GET: Jogador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Filtrar(FiltroJogadorViewModel filtro)
        {
            var clube = CartoleiroApp.CartolaDataSource.Clubes.FirstOrDefault(c => c.Nome == filtro.NomeClube);
            if (clube == null)
            {
                return View("Index", filtro);
            }

            var jogadores = CartoleiroApp.CartolaDataSource.Jogadores.Where(j => j.Clube == clube)
                                                                     .OrderByDescending(j => j.Jogos)
                                                                     .ThenByDescending(j => j.Pontuacao.Media).ToList();
            jogadores = (filtro.SomenteProvaveis)
                ? jogadores.Where(j => j.Status == Status.Provavel).ToList()
                : jogadores.ToList();

            ViewData["JOGADORES"] = jogadores;

            return View("Index", filtro);
        }
    }
}