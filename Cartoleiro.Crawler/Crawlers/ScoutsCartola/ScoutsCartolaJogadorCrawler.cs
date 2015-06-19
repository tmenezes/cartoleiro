using System;
using System.Globalization;
using System.Linq;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler.Crawlers.ScoutsCartola
{
    public class ScoutsCartolaJogadorCrawler : JogadorCrawler
    {
        private CultureInfo _cultura = CultureInfo.GetCultureInfo("en-US");

        public string Pagina { get; set; }
        public string Nome { get; set; }
        public Clube Clube { get; set; }

        public ScoutsCartolaJogadorCrawler(IWebDriver webDriver)
            : base(webDriver)
        {
        }


        public override JogadorCrawler MapearElementoHtml(IWebElement elementoHtml)
        {
            var nomeJogador = elementoHtml.Text.Substring(0, elementoHtml.Text.IndexOf('(')).Trim();
            var nomeClube = elementoHtml.Text.Substring(elementoHtml.Text.IndexOf('(')).Replace("(", "").Replace(")", "").Trim();

            Pagina = elementoHtml.GetAttribute("href");
            Nome = nomeJogador;
            Clube = CrawlerRuntimeHelper.GetClube(nomeClube);

            return this;
        }

        public override Jogador ObterJogador()
        {
            WebDriver.Navigate().GoToUrl(Pagina);

            var divAtletaInfo = WebDriver.FindElement(By.Id("athletebox"))
                                         .FindElement(By.XPath(@"//div[@id=""info""]"));

            var divValores = divAtletaInfo.FindElement(By.XPath(@"div[@id=""bloco1""]"));
            var spansInfo = divAtletaInfo.FindElements(By.XPath(@"div[@id=""bloco2""]//span"));

            var teste = spansInfo.Select(i => i.Text).Aggregate((um, outro) => um + "-" + outro);

            var jogador = new Jogador(Nome + teste, Clube, Posicao.Atacante)
                          {
                              Preco = GetPreco(divValores),
                              Pontuacao = GetPontuacao(divValores)
                          };

            return jogador;
        }

        private Preco GetPreco(IWebElement divValores)
        {
            var paragrafos = divValores.FindElements(By.TagName("p"));

            var spanPrecoAtual = paragrafos[1].FindElement(By.TagName("span"));
            var precoAtual = Convert.ToDouble(spanPrecoAtual.Text.Split(' ')[1], _cultura);

            return new Preco(precoAtual, 0);
        }

        private Pontuacao GetPontuacao(IWebElement divValores)
        {
            var paragrafos = divValores.FindElements(By.TagName("p"));

            var spanPontuacaoAtual = paragrafos[3].FindElement(By.TagName("span"));
            var spanPontuacaoMedia = paragrafos[4].FindElement(By.TagName("span"));

            var pontuacaoAtual = Convert.ToDouble(spanPontuacaoAtual.Text, _cultura);
            var pontuacaoMedia = Convert.ToDouble(spanPontuacaoMedia.Text, _cultura);

            return new Pontuacao(pontuacaoMedia, pontuacaoAtual);
        }
    }
}