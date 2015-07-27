using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Util;
using Cartoleiro.Crawler.Crawlers.ApiCartola;
using Cartoleiro.Crawler.Crawlers.ApiCartola.Json;
using Cartoleiro.Crawler.Crawlers.Futpedia.Json;
using Cartoleiro.Crawler.Utils;
using Newtonsoft.Json;
using Clube = Cartoleiro.Core.Cartola.Clube;
using Jogo = Cartoleiro.Core.Cartola.Jogo;

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
                var urlClube = string.Format("/{0}/numeros.json", GetNomeClubeNoFutpedia(clube));
                var jsonNumeros = HttpClientHelper.Get(_uriBase.ToString(), urlClube);
                var historicoDeJogos = JsonConvert.DeserializeObject<HistoricoJogos>(jsonNumeros);

                foreach (var jogoHistorico in historicoDeJogos.Jogos)
                {
					var jogo = FutpediaJogoCrawler.ObterJogo(jogoHistorico, clube, historicoDeJogos);

                    jogos.Add(jogo);
                }

                clubesCrawleados++;
                OnObjetoCarregado(new CrawlingInfo(clubes.Count(), clubesCrawleados, string.Format("{0} carregado. Total de jogos carregados: {1}", clube.Nome, jogos.Count)));
            }

			AtualizarClubesComMudancaDeNome(jogos);

            return jogos;
        }


        private string GetNomeClubeNoFutpedia(Clube clube)
        {
            var clubeSemAcento = StringUtils.RemoverAcentos(clube.Nome.ToLower().Replace(" ", "-"));

            return clubeSemAcento;
        }

		private static void AtualizarClubesComMudancaDeNome(IEnumerable<Jogo> jogos)
		{
			foreach (var jogo in jogos)
			{		
				jogo.Visitante.Nome = CrawlerHelper.GetNomeDoClube(jogo.Visitante);
			}
		}


        protected virtual void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            if (handler != null) handler(this, e);
        }
    }
}