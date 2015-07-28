using System;
using System.Globalization;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.Futpedia.Json;

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
                Campeonato = GetTipoCampeonato(jogoHistorico, historicoJogos)
            };

            return jogo;
        }


        private static TipoCampeonato GetTipoCampeonato(JogoHistorico jogo, HistoricoJogos historicoJogos)
        {
            var nomeCampeonato = jogo.GetCampeonato(historicoJogos).Nome;

            if (nomeCampeonato == "Taça Brasil")
                return TipoCampeonato.TacaBrasil;

            if (nomeCampeonato == "Torneio Roberto Gomes Pedrosa")
                return TipoCampeonato.TorneioRobertoGomesPedrosa;

            if (nomeCampeonato == "Campeonato Brasileiro")
                return TipoCampeonato.CampeonatoBrasileiro;

            if (nomeCampeonato == "Taça Libertadores")
                return TipoCampeonato.TacaLibertadores;

            if (nomeCampeonato == "Copa do Brasil")
                return TipoCampeonato.CopaDoBrasil;

            if (nomeCampeonato == "Mundial de Clubes")
                return TipoCampeonato.MundialDeClubes;

            if (nomeCampeonato == "Campeonato Paulista")
                return TipoCampeonato.CampeonatoPaulista;

            if (nomeCampeonato == "Torneio Rio-São Paulo")
                return TipoCampeonato.TorneioRioSaoPaulo;

            if (nomeCampeonato == "Campeonato Carioca")
                return TipoCampeonato.CampeonatoCarioca;

            return TipoCampeonato.Outro;
        }

        private static void ValidarMudancaDeNomeDeClube(Clube clube, Equipe equipe)
        {
            var mudouDeNome = clube.Nome != equipe.NomePopular;
            if (mudouDeNome)
            {
                equipe.NomePopular = clube.Nome;
            }
        }
    }
}