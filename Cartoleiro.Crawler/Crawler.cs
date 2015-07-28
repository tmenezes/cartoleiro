using System;
using System.Linq;
using Cartoleiro.Core.Data;
using Cartoleiro.Crawler.Crawlers.ApiCartola;
using Cartoleiro.Crawler.Crawlers.Futpedia;
using Cartoleiro.Crawler.Crawlers.GloboEsporte;

namespace Cartoleiro.Crawler
{
    public class CartoleiroCrawler
    {
        readonly GloboEsporteSiteCrawler _clubesCrawler;
        readonly ApiCartolaSiteCrawler _jogadoresCrawler;
        readonly FutpediaSiteCrawler _historicoDeJogosCrawler;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;

        public CartoleiroCrawler()
        {
            _clubesCrawler = new GloboEsporteSiteCrawler();
            _clubesCrawler.ObjetoCarregado += Crawler_ObjetoCarregado;

            _jogadoresCrawler = new ApiCartolaSiteCrawler();
            _jogadoresCrawler.ObjetoCarregado += Crawler_ObjetoCarregado;

            _historicoDeJogosCrawler = new FutpediaSiteCrawler();
            _historicoDeJogosCrawler.ObjetoCarregado += Crawler_ObjetoCarregado;
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
            var jogos = _historicoDeJogosCrawler.CarregarJogos(clubes.OrderBy(c=>c.Nome).ToList());

            return new CrawlerDataSource()
                   {
                       Clubes = clubes,
                       Jogadores = jogadores,
                       Rodadas = rodadas,
                       HistoricoDeJogos = jogos,
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

        public ICartolaDataSource ExecutarCrawlerDeHistoricoDeJogos()
        {
            var clubes = _clubesCrawler.CarregarClubes();
            var jogos = _historicoDeJogosCrawler.CarregarJogos(clubes.OrderBy(c => c.Nome).ToList());

            return new CrawlerDataSource()
            {
                Clubes = clubes,
                HistoricoDeJogos = jogos,
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
