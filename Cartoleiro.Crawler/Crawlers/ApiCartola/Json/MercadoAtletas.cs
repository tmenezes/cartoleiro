using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cartoleiro.Crawler.Crawlers.ApiCartola.Json
{
    public class Clube
    {
        [JsonProperty("mercado")]
        public int Mercado { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("abreviacao")]
        public string Abreviacao { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class Scout
    {
        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class Posicao
    {
        [JsonProperty("abreviacao")]
        public string Abreviacao { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class PartidaClubeVisitante
    {
        [JsonProperty("mercado")]
        public int Mercado { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("abreviacao")]
        public string Abreviacao { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class PartidaClubeCasa
    {
        [JsonProperty("mercado")]
        public int Mercado { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("abreviacao")]
        public string Abreviacao { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class Atleta
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("foto")]
        public string Foto { get; set; }

        [JsonProperty("clube")]
        public Clube Clube { get; set; }

        [JsonProperty("status_id")]
        public int StatusId { get; set; }

        [JsonProperty("jogos")]
        public int Jogos { get; set; }

        [JsonProperty("scout")]
        public IList<Scout> Scout { get; set; }

        [JsonProperty("preco")]
        public string Preco { get; set; }

        [JsonProperty("partida_data")]
        public string PartidaData { get; set; }

        [JsonProperty("variacao")]
        public string Variacao { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("media")]
        public string Media { get; set; }

        [JsonProperty("pontos")]
        public string Pontos { get; set; }

        [JsonProperty("apelido")]
        public string Apelido { get; set; }

        [JsonProperty("posicao")]
        public Posicao Posicao { get; set; }

        [JsonProperty("partida_clube_visitante")]
        public PartidaClubeVisitante PartidaClubeVisitante { get; set; }

        [JsonProperty("partida_clube_casa")]
        public PartidaClubeCasa PartidaClubeCasa { get; set; }
    }

    public class MercadoAtletas
    {
        [JsonProperty("atleta")]
        public IList<Atleta> Atletas { get; set; }
    }

}
