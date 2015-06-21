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

            var posicao = GetPosicao(spansInfo.First().Text);
            var status = GetStatus(spansInfo[1].Text);

            var jogador = new Jogador(Nome, Clube, posicao)
                          {
                              Preco = GetPreco(divValores),
                              Pontuacao = GetPontuacao(divValores),
                              Status = status
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

        private Posicao GetPosicao(string posicao)
        {
            if (posicao.ToLower() == "meia")
            {
                return Posicao.MeioCampo;
            }

            if (posicao.ToLower() == "lateral")
            {
                return Posicao.Lateral;
            }

            if (posicao.ToLower() == "goleiro")
            {
                return Posicao.Goleiro;
            }

            if (posicao.ToLower() == "atacante")
            {
                return Posicao.Atacante;
            }

            if (posicao.ToLower() == "zagueiro")
            {
                return Posicao.Zagueiro;
            }

            if (posicao.ToLower() == "técnico")
            {
                return Posicao.Tecnico;
            }

            return Posicao.MeioCampo;
        }

        private Status GetStatus(string status)
        {
            if (status.ToLower() == "barrado")
            {
                return Status.Barrado;
            }

            if (status.ToLower() == "contundido")
            {
                return Status.Contundido;
            }

            if (status.ToLower() == "dúvida" || status.ToLower() == "nulo")
            {
                return Status.Duvida;
            }

            if (status.ToLower() == "provável")
            {
                return Status.Provavel;
            }

            if (status.ToLower() == "suspenso")
            {
                return Status.Suspenso;
            }

            if (status.ToLower() == "vendido")
            {
                return Status.Vendido;
            }
            
            return Status.Provavel;
        }
    }
}