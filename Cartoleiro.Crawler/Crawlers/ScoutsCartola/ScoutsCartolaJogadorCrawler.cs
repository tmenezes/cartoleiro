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
            Clube = CrawlerHelper.GetClube(nomeClube);

            return this;
        }

        public override Jogador ObterJogador(IWebDriver webDriver)
        {
            int tentativas = 1;
            while (tentativas < 3)
            {
                try
                {
                    //WebDriver.Navigate().GoToUrl(Pagina);
                    webDriver.Navigate().GoToUrl(Pagina);

                    var divAtleta = webDriver.FindElement(By.Id("athletebox"));
                    var divAtletaInfo = divAtleta.FindElement(By.Id("info"));

                    var divValores = divAtletaInfo.FindElement(By.Id("bloco1"));
                    var spansInfo = divAtletaInfo.FindElement(By.Id("bloco2")).FindElements(By.TagName("span"));

                    var posicao = GetPosicao(spansInfo.First().Text);
                    var status = GetStatus(spansInfo[1].Text);
                    var scouts = GetScouts(divAtleta);

                    var jogador = new Jogador(Nome, Clube, posicao)
                                  {
                                      Preco = GetPreco(divValores),
                                      Pontuacao = GetPontuacao(divValores),
                                      Jogos = GetQuantidadeDeJogos(divValores),
                                      Status = status,
                                      Scouts = scouts,
                                  };

                    return jogador;
                }
                catch (Exception)
                {
                }
                finally
                {
                    tentativas++;
                }
            }
            throw new Exception("Não conseguiu obter o jogador");
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

        private int GetQuantidadeDeJogos(IWebElement divValores)
        {
            var paragrafos = divValores.FindElements(By.TagName("p"));

            var spanJogos = paragrafos.First().FindElement(By.TagName("span"));

            return string.IsNullOrWhiteSpace(spanJogos.Text) ? 0 : Convert.ToInt32(spanJogos.Text);
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

        private Scouts GetScouts(IWebElement divAtleta)
        {
            try
            {
                var divScoutsPositivos = divAtleta.FindElement(By.Id("scoutsPositivos"));
                var divScoutsNegativos = divAtleta.FindElement(By.Id("scoutsNegativos"));

                var itensScoutsPositivos = divScoutsPositivos.FindElements(By.TagName("p"))
                                                             .Select(p => Convert.ToInt32(p.FindElement(By.TagName("span")).Text))
                                                             .ToList();
                var itensScoutsNegativos = divScoutsNegativos.FindElements(By.TagName("p"))
                                                             .Select(p => p.FindElement(By.TagName("span")).Text)
                                                             .Select(i => string.IsNullOrWhiteSpace(i) ? 0 : Convert.ToInt32(i))
                                                             .ToList();

                var scouts = new Scouts
                             {
                                 FaltasSofridas = itensScoutsPositivos[0],
                                 Assistencias = itensScoutsPositivos[1],
                                 FinalizacoesNaTrave = itensScoutsPositivos[2],
                                 FinalizacoesDefendidas = itensScoutsPositivos[3],
                                 FinalizacoesFora = itensScoutsPositivos[4],
                                 Gols = itensScoutsPositivos[5],
                                 RoubadasDeBola = itensScoutsPositivos[6],

                                 PassesErrados = itensScoutsNegativos[0],
                                 Impedimentos = itensScoutsNegativos[1],
                                 PenaltisPerdidos = itensScoutsNegativos[2],
                                 FaltasCometidas = itensScoutsNegativos[3],
                                 GolsContra = itensScoutsNegativos[4],
                                 CartoesAmarelo = itensScoutsNegativos[5],
                                 CartoesVermelho = itensScoutsNegativos[6]
                             };

                return scouts;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}