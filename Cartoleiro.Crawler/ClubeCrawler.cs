using System.Security.AccessControl;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler
{
    public abstract class ClubeCrawler
    {
        protected IWebDriver WebDriver { get; set; }
        public string Nome { get; set; }
        public int Posicao { get; set; }

        protected ClubeCrawler(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public abstract ClubeCrawler MapearElementoHtml(IWebElement elementoHtml);
        public abstract Clube ObterClube(IWebElement dadosExtras);

        public override string ToString()
        {
            return string.Format("Nome: {0}", Nome);
        }
    }
}