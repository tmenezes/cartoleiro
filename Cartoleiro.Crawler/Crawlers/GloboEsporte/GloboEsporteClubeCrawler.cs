using System;
using System.Linq;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium;

namespace Cartoleiro.Crawler.Crawlers.GloboEsporte
{
    class GloboEsporteClubeCrawler : ClubeCrawler
    {
        private const int PONTOS = 0;
        private const int JOGOS = 1;
        private const int VITORIAS = 2;
        private const int EMPATES = 3;
        private const int DERROTAS = 4;
        private const int GOLS_PRO = 5;
        private const int GOLS_CONTRA = 6;
        private const int SALDO_GOLS = 7;
        private const int APROVEITAMENTO = 8;
        private const int ULTIMOS_JOGOS = 9;

        public GloboEsporteClubeCrawler(IWebDriver webDriver)
            : base(webDriver)
        {
        }

        public override ClubeCrawler MapearElementoHtml(IWebElement elementoHtml)
        {
            var tds = elementoHtml.FindElements(By.TagName("td"));

            Posicao = Convert.ToInt32(tds[0].Text);
            Nome = tds[1].FindElement(By.TagName("a")).GetAttribute("title");

            return this;
        }

        public override Clube ObterClube(IWebElement dadosExtras)
        {
            var campeonato = GetCampeonato(dadosExtras);
            campeonato.Posicao = Posicao;

            return new Clube(Nome)
                   {
                       Campeonato = campeonato
                   };
        }

        private Campeonato GetCampeonato(IWebElement dadosExtras)
        {
            var dadosCampeonato = dadosExtras.Text.Split(' ')
                                                  .Take(8)
                                                  .Select(i => string.IsNullOrWhiteSpace(i) ? 0 : Convert.ToInt32(i))
                                                  .ToArray();
            var campeonato = new Campeonato()
                             {
                                 Pontos = dadosCampeonato[PONTOS],
                                 Jogos = dadosCampeonato[JOGOS],
                                 Vitorias = dadosCampeonato[VITORIAS],
                                 Empates = dadosCampeonato[EMPATES],
                                 Derrotas = dadosCampeonato[DERROTAS],
                                 GolsPro = dadosCampeonato[GOLS_PRO],
                                 GolsContra = dadosCampeonato[GOLS_CONTRA],
                                 SaldoDeGol = dadosCampeonato[SALDO_GOLS],
                             };

            var ultimosJogos = dadosExtras.FindElement(By.CssSelector(".tabela-pontos-ultimos-jogos")).FindElements(By.TagName("span"));
            var vitoriasNosUltimos5Jogos = ultimosJogos.Count(i => i.GetAttribute("class").Contains("tabela-icone-v"));
            var empatesNosUltimos5Jogos = ultimosJogos.Count(i => i.GetAttribute("class").Contains("tabela-icone-e"));
            var derrotasNosUltimos5Jogos = ultimosJogos.Count(i => i.GetAttribute("class").Contains("tabela-icone-d"));

            campeonato.UltimosJogos = new UltimosJogos(vitoriasNosUltimos5Jogos, empatesNosUltimos5Jogos, derrotasNosUltimos5Jogos);

            return campeonato;
        }
    }
}
