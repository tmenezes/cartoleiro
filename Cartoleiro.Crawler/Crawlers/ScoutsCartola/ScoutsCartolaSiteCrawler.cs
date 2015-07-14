using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler.Crawlers.ScoutsCartola
{
    public class ScoutsCartolaSiteCrawler : ISiteCrawler
    {
        private readonly Uri _uriBase;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;

        public bool SuportaClubes { get { return true; } }
        public bool SuportaJogadores { get { return true; } }
        public bool SuportaRodadas { get; private set; }


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

            var driver = CrawlerHelper.GetWebDriver();
            driver.Navigate().GoToUrl(new Uri(_uriBase, "/jogador"));

            var divJogadores = driver.FindElementById("boxAlfabetica");
            var linksDeJogadores = divJogadores.FindElements(By.TagName("a")).ToList();

            if (quantidade < int.MaxValue)
                linksDeJogadores = linksDeJogadores.Take(quantidade).ToList();


            if (linksDeJogadores.Any())
            {
                var jogadoresEncontrados = GetJogadoresEncontrados(linksDeJogadores, driver);

                var driversDeJogadores = Enumerable.Range(0, 6).Select(i => CrawlerHelper.GetWebDriver()).ToList();

                //foreach (var jogadorCrawler in jogadoresEncontrados)
                //Parallel.ForEach(jogadoresEncontrados, new ParallelOptions { MaxDegreeOfParallelism = 20 }, jogadorCrawler =>
                Parallel.For(0, jogadoresEncontrados.Count, new ParallelOptions { MaxDegreeOfParallelism = 5 }, i =>
                {
                    var jogador = jogadoresEncontrados[i].ObterJogador(driversDeJogadores[i % 5]);

                    jogadores.Add(jogador);

                    OnObjetoCarregado(new CrawlingInfo(jogadoresEncontrados.Count, jogadores.Count, jogador));
                });

                driversDeJogadores.ForEach(driverJogador => driver.Quit());
            }

            driver.Quit();


            return jogadores;
        }

        public IEnumerable<Rodada> CarregarRodadas()
        {
            yield break;
        }


        private IList<JogadorCrawler> GetJogadoresEncontrados(List<IWebElement> linksDeJogadores, IWebDriver driver)
        {
            var jogadoresEncontrados = new List<JogadorCrawler>(linksDeJogadores.Count);

            foreach (var link in linksDeJogadores)
            {
                var jogador = new ScoutsCartolaJogadorCrawler(driver).MapearElementoHtml(link);

                jogadoresEncontrados.Add(jogador);

                OnObjetoCarregado(new CrawlingInfo(linksDeJogadores.Count, jogadoresEncontrados.Count, jogador));
            }

            return jogadoresEncontrados;
        }

        protected virtual void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            if (handler != null) handler(this, e);
        }
    }
}