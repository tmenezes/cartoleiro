using System;
using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Crawler.Crawlers.ScoutsCartola;

namespace Cartoleiro.Crawler
{
    public class CartoleiroCrawler
    {
        readonly ScoutsCartolaSiteCrawler _siteCrawler;

        public event EventHandler<CrawlingInfo> ObjetoCarregado;

        public CartoleiroCrawler()
        {
            var uri = new Uri("http://www.scoutscartola.com");
            _siteCrawler = new ScoutsCartolaSiteCrawler(uri);

            _siteCrawler.ObjetoCarregado += Crawler_ObjetoCarregado;
        }
        ~CartoleiroCrawler()
        {
            _siteCrawler.ObjetoCarregado -= Crawler_ObjetoCarregado;
        }


        public ICartolaDataSource Executar()
        {
            var clubes = new List<Clube>();
            var jogadores = _siteCrawler.CarregarJogadores();

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
