using System;
using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Crawler.Crawlers.GloboEsporte;
using Cartoleiro.Crawler.Crawlers.ScoutsCartola;

namespace Cartoleiro.Crawler
{
    public class CartoleiroCrawler
    {
        readonly GloboEsporteSiteCrawler _clubesCrawler;
        readonly ScoutsCartolaSiteCrawler _jogadoresCrawler;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;

        public CartoleiroCrawler()
        {
            var uriGloboEsporte = new Uri("http://globoesporte.globo.com/futebol/brasileirao-serie-a");
            _clubesCrawler = new GloboEsporteSiteCrawler(uriGloboEsporte);
            _clubesCrawler.ObjetoCarregado += Crawler_ObjetoCarregado;

            var uriScoutsCartola = new Uri("http://www.scoutscartola.com");
            _jogadoresCrawler = new ScoutsCartolaSiteCrawler(uriScoutsCartola);
            _jogadoresCrawler.ObjetoCarregado += Crawler_ObjetoCarregado;
        }
        ~CartoleiroCrawler()
        {
            _clubesCrawler.ObjetoCarregado -= Crawler_ObjetoCarregado;
            _jogadoresCrawler.ObjetoCarregado -= Crawler_ObjetoCarregado;
        }


        public ICartolaDataSource Executar()
        {
            var clubes = _clubesCrawler.CarregarClubes();
            var jogadores = _jogadoresCrawler.CarregarJogadores();

            return new CrawlerDataSource(clubes, jogadores);
        }

        public ICartolaDataSource ExecutarCrawlerDeClubes()
        {
            var clubes = _clubesCrawler.CarregarClubes();
            var jogadores = new List<Jogador>();

            return new CrawlerDataSource(clubes, jogadores);
        }

        public ICartolaDataSource ExecutarCrawlerDeJogadores()
        {
            var clubes = new List<Clube>();
            var jogadores = _jogadoresCrawler.CarregarJogadores();

            return new CrawlerDataSource(clubes, jogadores);
        }


        private void Crawler_ObjetoCarregado(object sender, CrawlingInfo e)
        {
            OnObjetoCarregado(e);
        }

        protected void OnObjetoCarregado(CrawlingInfo e)
        {
            var handler = ObjetoCarregado;
            if (handler != null) handler(this, e);
        }
    }
}
