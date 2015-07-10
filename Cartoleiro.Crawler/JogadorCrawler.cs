using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler
{
    public abstract class JogadorCrawler
    {
        protected IWebDriver WebDriver { get; set; }
        public string Pagina { get; set; }
        public string Nome { get; set; }

        protected JogadorCrawler(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public abstract JogadorCrawler MapearElementoHtml(IWebElement elementoHtml);
        public abstract Jogador ObterJogador(IWebDriver webDriver);

        public override string ToString()
        {
            return string.Format("Nome: {0}, Pagina: {1}", Nome, Pagina);
        }
    }
}