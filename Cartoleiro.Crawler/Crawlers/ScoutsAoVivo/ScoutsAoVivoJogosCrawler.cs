using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler.Crawlers.ScoutsAoVivo
{
    public class ScoutsAoVivoJogosCrawler
    {
        public IEnumerable<string> CarregarIdsPartidas()
        {
            var idsPartidas = new List<string>();

            var driver = CrawlerHelper.GetWebDriver();
            driver.Navigate().GoToUrl("http://scoutsaovivo.appspot.com/index.php");

            var ulListaJogos = driver.FindElementById("games-list");
            var lisJogos = ulListaJogos.FindElements(By.TagName("li"));

            foreach (var li in lisJogos)
            {
                var linkJogo = li.FindElement(By.TagName("a"));
                var href = linkJogo.GetAttribute("href");

                var posicaoId = href.IndexOf("match=", StringComparison.Ordinal) + 6;
                var idPartida = href.Substring(posicaoId);
                
                idsPartidas.Add(idPartida);
            }

            driver.Quit();

            return idsPartidas;
        }
    }
}
