using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Cartoleiro.Crawler.Crawlers.Futpedia.Json
{
    public class JogoHistorico
    {
        [JsonProperty("d")]
        public string Data { get; set; }

        [JsonProperty("a")]
        public int A { get; set; }

        [JsonProperty("c")]
        public int Campeonato { get; set; }

        [JsonProperty("ec")]
        public int EdicaoCampeonato { get; set; }

        [JsonProperty("p")]
        public string Participacao { get; set; }

        [JsonProperty("m")]
        public int PlacarMandante { get; set; }

        [JsonProperty("v")]
        public int PlacarVisitante { get; set; }

        [JsonProperty("e")]
        public int Equipe { get; set; }

        [JsonProperty("ea")]
        public int EquipeAdversaria { get; set; }


        public Equipe GetEquipe(HistoricoJogos historicoJogos)
        {
            return historicoJogos.Equipes.First(e => e.Value.EquipeId == Equipe).Value;
        }

        public Equipe GetEquipeAdversaria(HistoricoJogos historicoJogos)
        {
            return historicoJogos.Equipes.First(e => e.Value.EquipeId == EquipeAdversaria).Value;
        }

        public Campeonato GetCampeonato(HistoricoJogos historicoJogos)
        {
            return historicoJogos.Campeonatos.First(c => c.Value.CampeonatoId == Campeonato).Value;
        }
    }

    public class Equipe
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("escudo_p")]
        public string EscudoP { get; set; }

        [JsonProperty("organizacao_id")]
        public int OrganizacaoId { get; set; }

        [JsonProperty("equipe_id")]
        public int EquipeId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("nome_popular")]
        public string NomePopular { get; set; }

        [JsonProperty("sigla")]
        public string Sigla { get; set; }
    }

    public class Campeonato
    {
        [JsonProperty("campeonato_id")]
        public int CampeonatoId { get; set; }

        [JsonProperty("agregador_id")]
        public int AgregadorId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class HistoricoJogos
    {
        [JsonProperty("jogos")]
        public IList<JogoHistorico> Jogos { get; set; }

        [JsonProperty("equipes")]
        public Dictionary<string, Equipe> Equipes { get; set; }

        [JsonProperty("campeonatos")]
        public Dictionary<string, Campeonato> Campeonatos { get; set; }
    }
}
