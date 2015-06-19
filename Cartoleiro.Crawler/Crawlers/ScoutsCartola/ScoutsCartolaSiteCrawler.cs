using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Cartoleiro.Crawler.Crawlers.ScoutsCartola
{
    public class ScoutsCartolaSiteCrawler : ISiteCrawler
    {
        private const string URL_JOGADORES = "/jogador";
        private const string ID_DIV_JOGADORES = "boxAlfabetica";

        private readonly Uri _uriBase;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;

        public bool SuportaClubes { get { return true; } }
        public bool SuportaJogadores { get { return true; } }


        public ScoutsCartolaSiteCrawler(Uri uriBase)
        {
            _uriBase = uriBase;
        }


        public IEnumerable<Clube> CarregarClubes()
        {
            yield break;
        }

        public IEnumerable<Jogador> CarregarJogadores()
        {
            return CarregarJogadores(int.MaxValue);
        }

        public IEnumerable<Jogador> CarregarJogadores(int quantidade)
        {
            IList<Jogador> jogadores = new List<Jogador>();

            var driver = new PhantomJSDriver();
            driver.Navigate().GoToUrl(new Uri(_uriBase, URL_JOGADORES));

            var divJogadores = driver.FindElementById("boxAlfabetica");
            var linksDeJogadores = divJogadores.FindElements(By.TagName("a")).ToList();

            if (linksDeJogadores.Any())
            {
                var jogadoresEncontrados = linksDeJogadores.Select(a => new ScoutsCartolaJogadorCrawler(driver).MapearElementoHtml(a));
                jogadoresEncontrados = (quantidade < int.MaxValue)
                    ? jogadoresEncontrados.Take(quantidade).ToList()
                    : jogadoresEncontrados.ToList();

                foreach (var jogadorCrawler in jogadoresEncontrados)
                {
                    var jogador = jogadorCrawler.ObterJogador();

                    jogadores.Add(jogador);

                    OnObjetoCarregado(new CrawlingInfo(jogadoresEncontrados.Count(), jogadores.Count, jogador.ToString()));
                }
            }

            driver.Quit();

            return jogadores;
        }

        protected virtual void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            if (handler != null) handler(this, e);
        }
    }
}