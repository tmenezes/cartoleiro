using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.GloboEsporte;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Crawler.GloboEsporte
{
    [TestClass]
    public class Ao_obter_Clubes : AbstractTesteAutoAct
    {
        GloboEsporteSiteCrawler crawler;
        IEnumerable<Clube> clubes;


        public override void Arrange()
        {
            var uri = new Uri("http://globoesporte.globo.com/futebol/brasileirao-serie-a/");

            crawler = new GloboEsporteSiteCrawler(uri);
        }

        public override void Act()
        {
            clubes = crawler.CarregarClubes();
        }

        [TestMethod]
        public void Deve_carregar_clubes_com_sucesso()
        {
            Assert.IsTrue(clubes.Any());

            var clube1 = clubes.First();
            Assert.IsNotNull(clube1.Nome);
            Assert.IsNotNull(clube1.Campeonato);
            Assert.IsTrue(clube1.Campeonato.Posicao > 0);
            Assert.IsTrue(clube1.Campeonato.Jogos > 0);
        }
    }
}
