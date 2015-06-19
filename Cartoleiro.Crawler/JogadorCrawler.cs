using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler
{
    public abstract class JogadorCrawler
    {
        protected IWebDriver WebDriver { get; set; }

        protected JogadorCrawler(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public abstract JogadorCrawler MapearElementoHtml(IWebElement elementoHtml);
        public abstract Jogador ObterJogador();
    }
}