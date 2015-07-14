using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.ApiCartola.Json;
using Cartoleiro.Crawler.Utils;
using Newtonsoft.Json;
using OpenQA.Selenium;
using Clube = Cartoleiro.Crawler.Crawlers.ApiCartola.Json.Clube;

namespace Cartoleiro.Crawler.Crawlers.ApiCartola
{
    public class ApiCartolaSiteCrawler : ISiteCrawler
    {
        private const string URL_CARTOLA = "http://api.cartola.globo.com";
        private const int MERCADO_ABERTO = 1;

        private readonly Uri _uriBase;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;

        public bool SuportaClubes { get { return false; } }
        public bool SuportaJogadores
        {
            get
            {
                var jsonMercadoStatus = HttpClientHelper.Get(_uriBase.ToString(), "/mercado/status.json");
                var mercadoStatus = JsonConvert.DeserializeObject<MercadoStatus>(jsonMercadoStatus);

                var mercadoAberto = (mercadoStatus != null) && mercadoStatus.Mercado.Status == MERCADO_ABERTO;
                return mercadoAberto;
            }
        }
        public bool SuportaRodadas { get { return false; } }


        public ApiCartolaSiteCrawler()
            : this(new Uri(URL_CARTOLA))
        {
        }

        public ApiCartolaSiteCrawler(Uri uriBase)
        {
            _uriBase = uriBase;
        }


        public IEnumerable<Core.Cartola.Clube> CarregarClubes()
        {
            yield break;
        }

        public IEnumerable<Jogador> CarregarJogadores()
        {
            IList<Jogador> jogadores = new List<Jogador>();

            if (!SuportaJogadores)
                return jogadores;


            var jsonJogadores = HttpClientHelper.Get(_uriBase.ToString(), "/mercado.json");
            var mercado = JsonConvert.DeserializeObject<MercadoAtletas>(jsonJogadores);

            foreach (var atleta in mercado.Atletas)
            {
                var jogador = ApiCartolaJogadorCrawler.ObterJogador(atleta);

                jogadores.Add(jogador);

                OnObjetoCarregado(new CrawlingInfo(mercado.Atletas.Count, jogadores.Count, jogador));
            }

            return jogadores;
        }

        public IEnumerable<Rodada> CarregarRodadas()
        {
            yield break;
        }


        protected virtual void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            if (handler != null) handler(this, e);
        }
    }
}