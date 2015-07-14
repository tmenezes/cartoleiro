using Newtonsoft.Json;

namespace Cartoleiro.Crawler.Crawlers.ApiCartola.Json
{
    public class Fechamento
    {
        [JsonProperty("hora")]
        public int Hora { get; set; }

        [JsonProperty("ano")]
        public int Ano { get; set; }

        [JsonProperty("minuto")]
        public int Minuto { get; set; }

        [JsonProperty("dia")]
        public int Dia { get; set; }

        [JsonProperty("mes")]
        public int Mes { get; set; }
    }

    public class Mercado
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("times_escalados")]
        public int TimesEscalados { get; set; }

        [JsonProperty("fechamento")]
        public Fechamento Fechamento { get; set; }

        [JsonProperty("rodada")]
        public int Rodada { get; set; }
    }

    public class MercadoStatus
    {
        [JsonProperty("mercado")]
        public Mercado Mercado { get; set; }
    }
}
