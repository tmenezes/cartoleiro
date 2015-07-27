using System;
using System.Globalization;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.ApiCartola.Json;
using Cartoleiro.Crawler.Crawlers.Futpedia.Json;
using Jogo = Cartoleiro.Core.Cartola.Jogo;
using Posicao = Cartoleiro.Core.Cartola.Posicao;

namespace Cartoleiro.Crawler.Crawlers.Futpedia
{
    public class FutpediaJogoCrawler
    {
        public static Jogo ObterJogo(JogoHistorico jogoHistorico, HistoricoJogos historicoJogos)
        {
            var equipe = jogoHistorico.GetEquipe(historicoJogos);
            var equipeAdversaria = jogoHistorico.GetEquipeAdversaria(historicoJogos);

            var clubeMandante = CrawlerHelper.GetClube(jogoHistorico.Participacao == "m" ? equipe.NomePopular : equipeAdversaria.NomePopular);
            var clubeVisitante = CrawlerHelper.GetClube(jogoHistorico.Participacao == "v" ? equipe.NomePopular : equipeAdversaria.NomePopular);


            var jogo = new Jogo(0, clubeMandante, clubeVisitante)
                          {
                              PlacarMandante = jogoHistorico.PlacarMandante,
                              PlacarVisitante = jogoHistorico.PlacarVisitante,

                              DataDoJogo = DateTime.ParseExact(jogoHistorico.Data, "yyyyMMdd", CultureInfo.InvariantCulture),
                              NomeDoCampeonato = jogoHistorico.GetCampeonato(historicoJogos).Nome
                          };

            return jogo;
        }
    }
}