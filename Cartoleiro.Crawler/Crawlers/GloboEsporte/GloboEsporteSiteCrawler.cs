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
        private readonly Uri _uriBase;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;
        public bool SuportaClubes { get; private set; }
        public bool SuportaJogadores { get; private set; }


        public GloboEsporteSiteCrawler(Uri uriBase)
        {
            _uriBase = uriBase;
        }


        public IEnumerable<Clube> CarregarClubes()
        {
            IList<Clube> clubes = new List<Clube>();

            var driver = new PhantomJSDriver();
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

                    OnObjetoCarregado(new CrawlingInfo(trsClubes.Count(), clubes.Count, clube.ToString()));
                }
            }

            driver.Quit();

            return clubes;
        }

        public IEnumerable<Jogador> CarregarJogadores()
        {
            yield break;
        }


        protected virtual void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            if (handler != null) handler(this, e);
        }
    }
}
