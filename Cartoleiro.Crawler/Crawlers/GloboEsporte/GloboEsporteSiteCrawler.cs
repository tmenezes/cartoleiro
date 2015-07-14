using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace Cartoleiro.Crawler.Crawlers.GloboEsporte
{
    public class GloboEsporteSiteCrawler : ISiteCrawler
    {
        private const string URL_CARTOLA = "http://globoesporte.globo.com/futebol/brasileirao-serie-a";
        private readonly Uri _uriBase;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;
        public bool SuportaClubes { get; private set; }
        public bool SuportaJogadores { get; private set; }
        public bool SuportaRodadas { get; private set; }


        public GloboEsporteSiteCrawler()
            : this(new Uri(URL_CARTOLA))
        {
        }

        public GloboEsporteSiteCrawler(Uri uriBase)
        {
            _uriBase = uriBase;
        }


        public IEnumerable<Clube> CarregarClubes()
        {
            var clubes = new List<Clube>();

            var driver = CrawlerHelper.GetWebDriver();
            driver.Navigate().GoToUrl(_uriBase);

            var divClubes = driver.FindElementByXPath(@"//div[@class='tabela tabela-sem-jogos-por-grupo']");
            var tableClubes = divClubes.FindElement(By.TagName("table"));
            var tableCampeonato = divClubes.FindElement(By.TagName("div")).FindElement(By.TagName("table"));

            if (tableClubes != null && tableCampeonato != null)
            {
                var trsClubes = tableClubes.FindElements(By.TagName("tr")).Skip(1).ToList();
                var trsCampeonato = tableCampeonato.FindElements(By.TagName("tr")).Skip(1).ToList();

                for (int i = 0; i < trsClubes.Count; i++)
                {
                    var trClube = trsClubes[i];
                    var trCampeonato = trsCampeonato[i];

                    var clube = new GloboEsporteClubeCrawler(driver).MapearElementoHtml(trClube)
                                                                    .ObterClube(trCampeonato);
                    clubes.Add(clube);

                    OnObjetoCarregado(new CrawlingInfo(trsClubes.Count(), clubes.Count, clube));
                }
            }

            driver.Quit();

            return clubes;
        }

        public IEnumerable<Jogador> CarregarJogadores()
        {
            yield break;
        }

        public IEnumerable<Rodada> CarregarRodadas()
        {
            return CarregarRodadas(int.MaxValue);
        }

        public IEnumerable<Rodada> CarregarRodadas(int quantidade)
        {
            var rodadas = new List<Rodada>();

            var driver = CrawlerHelper.GetWebDriver();
            driver.Navigate().GoToUrl(_uriBase);

            var asideJogos = driver.FindElement(By.TagName("aside"));

            var templateUrlRodadas = string.Concat("http://globoesporte.globo.com", asideJogos.GetAttribute("data-url-pattern-navegador-jogos"), "{0}/jogos.html");
            var totalRodadas = Convert.ToInt32(asideJogos.FindElement(By.TagName("nav"))
                                                         .FindElements(By.TagName("span"))[1]
                                                         .GetAttribute("data-rodadas-length"));

            for (int i = 1; i <= totalRodadas; i++)
            {
                var urlRodada = string.Format(templateUrlRodadas, i);
                driver.Navigate().GoToUrl(urlRodada);

                var rodada = new GloboEsporteRodadaCrawler(driver).ObterRodada(i);
                rodadas.Add(rodada);


                OnObjetoCarregado(new CrawlingInfo(totalRodadas, rodadas.Count, rodada));

                if (rodadas.Count >= quantidade)
                    break;
            }

            driver.Quit();

            return rodadas;
        }


        protected virtual void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            if (handler != null) handler(this, e);
        }
    }
}
