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
            var rodadas = _clubesCrawler.CarregarRodadas();

            return new CrawlerDataSource()
                   {
                       Clubes = clubes,
                       Jogadores = jogadores,
                       Rodadas = rodadas
                   };
        }

        public ICartolaDataSource ExecutarCrawlerDeClubes()
        {
            var clubes = _clubesCrawler.CarregarClubes();

            return new CrawlerDataSource()
                   {
                       Clubes = clubes
                   };
        }

        public ICartolaDataSource ExecutarCrawlerDeJogadores()
        {
            var jogadores = _jogadoresCrawler.CarregarJogadores();

            return new CrawlerDataSource()
                   {
                       Jogadores = jogadores
                   };
        }

        public ICartolaDataSource ExecutarCrawlerDeRodadas()
        {
            var rodadas = _clubesCrawler.CarregarRodadas();

            return new CrawlerDataSource()
                   {
                       Rodadas = rodadas
                   };
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
