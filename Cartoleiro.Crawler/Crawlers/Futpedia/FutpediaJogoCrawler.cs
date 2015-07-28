using System;
using System.Globalization;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.Futpedia.Json;
using Jogo = Cartoleiro.Core.Cartola.Jogo;

namespace Cartoleiro.Crawler.Crawlers.Futpedia
{
    public class FutpediaJogoCrawler
    {
        public static Jogo ObterJogo(JogoHistorico jogoHistorico, Clube clube, HistoricoJogos historicoJogos)
        {
            var equipe = jogoHistorico.GetEquipe(historicoJogos);
            var equipeAdversaria = jogoHistorico.GetEquipeAdversaria(historicoJogos);

            ValidarMudancaDeNomeDeClube(clube, equipe);

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

        static void ValidarMudancaDeNomeDeClube(Clube clube, Equipe equipe)
        {
            var mudouDeNome = clube.Nome != equipe.NomePopular;
            if (mudouDeNome)
            {
                equipe.NomePopular = clube.Nome;
            }
        }
    }
}