using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Util;
using Cartoleiro.Crawler.Crawlers.Futpedia.Json;
using Cartoleiro.Crawler.Utils;
using Newtonsoft.Json;

namespace Cartoleiro.Crawler.Crawlers.Futpedia
{
    public class FutpediaSiteCrawler
    {
        private const string URL_CARTOLA = "http://futpedia.globo.com/";

        private readonly Uri _uriBase;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;


        public FutpediaSiteCrawler()
            : this(new Uri(URL_CARTOLA))
        {
        }

        public FutpediaSiteCrawler(Uri uriBase)
        {
            _uriBase = uriBase;
        }


        public IEnumerable<Jogo> CarregarJogos(IEnumerable<Clube> clubes)
        {
            IList<Jogo> jogos = new List<Jogo>();
            int clubesCrawleados = 0;

            foreach (var clube in clubes)
            {
                var nomeClubeNoFutepedia = GetNomeClubeNoFutpedia(clube);
                var urlClube = $"/{nomeClubeNoFutepedia}/numeros.json";
                var jsonNumeros = HttpClientHelper.Get(_uriBase.ToString(), urlClube);
                var historicoDeJogos = JsonConvert.DeserializeObject<HistoricoJogos>(jsonNumeros);

                foreach (var jogoHistorico in historicoDeJogos.Jogos)
                {
                    var jogo = FutpediaJogoCrawler.ObterJogo(jogoHistorico, clube, historicoDeJogos);

                    jogos.Add(jogo);
                }

                clubesCrawleados++;
                OnObjetoCarregado(new CrawlingInfo(clubes.Count(), clubesCrawleados, $"{clube.Nome} carregado. Total de jogos carregados: {jogos.Count}"));
            }

            return jogos;
        }


        private string GetNomeClubeNoFutpedia(Clube clube)
        {
            var clubeSemAcento = StringUtils.RemoverAcentos(clube.Nome.ToLower().Replace(" ", "-"));

            return clubeSemAcento;
        }


        protected virtual void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            handler?.Invoke(this, e);
        }
    }
}