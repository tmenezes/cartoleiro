using System;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler
{
    public abstract class RodadaCrawler
    {
        protected IWebDriver WebDriver { get; set; }
        public int Numero { get; set; }

        protected RodadaCrawler(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public abstract Rodada ObterRodada(int numero);

        public override string ToString()
        {
            return string.Format("Numero: {0}", Numero);
        }
    }

    public class GloboEsporteRodadaCrawler : RodadaCrawler
    {
        public GloboEsporteRodadaCrawler(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        public override Rodada ObterRodada(int numero)
        {
            var rodada = new Rodada(numero);

            var liJogos = WebDriver.FindElements(By.TagName("li"));

            foreach (var liJogo in liJogos)
            {
                var jogo = GetJogo(liJogo);

                rodada.Jogos.Add(jogo);
            }

            return rodada;
        }

        private static Jogo GetJogo(IWebElement liJogo)
        {
            //var divInfo = liJogo.FindElement(By.CssSelector(".placar-jogo-informacoes"));
            var divJogo = liJogo.FindElement(By.CssSelector(".placar-jogo-equipes"));

            var spansJogo = divJogo.FindElements(By.CssSelector(".placar-jogo-equipes-item"));

            var mandante = spansJogo[0].FindElement(By.CssSelector(".placar-jogo-equipes-nome")).Text;
            var imgMandante = spansJogo[0].FindElement(By.TagName("img")).GetAttribute("src");
            var placarMandante = spansJogo[1].FindElement(By.CssSelector(".placar-jogo-equipes-placar-mandante")).Text;

            var visitante = spansJogo[2].FindElement(By.CssSelector(".placar-jogo-equipes-nome")).Text;
            var imgVisitante = spansJogo[2].FindElement(By.TagName("img")).GetAttribute("src");
            var placarVisitante = spansJogo[1].FindElement(By.CssSelector(".placar-jogo-equipes-placar-visitante")).Text;

            var jogo = new Jogo(CrawlerHelper.GetClube(mandante), CrawlerHelper.GetClube(visitante));
            jogo.PlacarMandante = string.IsNullOrWhiteSpace(placarMandante) ? 0 : Convert.ToInt32(placarMandante);
            jogo.PlacarVisitante = string.IsNullOrWhiteSpace(placarVisitante) ? 0 : Convert.ToInt32(placarVisitante);

            return jogo;
        }
    }
}