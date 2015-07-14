using System.Linq;
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

        [HttpPost]
        public ActionResult Index(FiltroJogadorViewModel filtro)
        {
            var clube = CartoleiroApp.CartolaDataSource.Clubes.FirstOrDefault(c => c.Nome == filtro.NomeClube);
            if (clube == null)
            {
                return View(filtro);
            }

            var jogadores = CartoleiroApp.CartolaDataSource.Jogadores.Where(j => j.Clube == clube)
                                                                     .OrderByDescending(j => j.Jogos)
                                                                     .ThenByDescending(j => j.Pontuacao.Media).ToList();
            jogadores = (filtro.SomenteProvaveis)
                ? jogadores.Where(j => j.Status == Status.Provavel).ToList()
                : jogadores.ToList();

            ViewData["JOGADORES"] = jogadores;

            return View(filtro);
        }

        public ActionResult Detalhe(int id, string detalhe)
        {
            var jogador = CartoleiroApp.CartolaDataSource.Jogadores.FirstOrDefault(j => j.Id == id);

            return View(jogador);
        }
    }
}