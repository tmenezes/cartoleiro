using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.GloboEsporte;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Crawler.GloboEsporte
{
    [TestClass]
    public class Ao_obter_Rodadas : AbstractTesteAutoAct
    {
        GloboEsporteSiteCrawler crawler;
        IEnumerable<Rodada> rodadas;


        public override void Arrange()
        {
            var uri = new Uri("http://globoesporte.globo.com/futebol/brasileirao-serie-a/");

            crawler = new GloboEsporteSiteCrawler(uri);
        }

        public override void Act()
        {
            rodadas = crawler.CarregarRodadas(2);
        }

        [TestMethod]
        public void Deve_carregar_rodadas_com_sucesso()
        {
            Assert.IsTrue(rodadas.Any());

            var rodada1 = rodadas.First();
            Assert.IsTrue(rodada1.Numero > 0);
            Assert.IsTrue(rodada1.Jogos.Any());
        }
    }
}