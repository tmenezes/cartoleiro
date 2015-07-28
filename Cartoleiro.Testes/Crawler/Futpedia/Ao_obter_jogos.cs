using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.ApiCartola;
using Cartoleiro.Crawler.Crawlers.Futpedia;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Crawler.Futpedia
{
    [TestClass]
    public class Ao_obter_jogos : AbstractTesteAutoAct
    {
        FutpediaSiteCrawler crawler;
        IEnumerable<Jogo> jogos;
        string nomeClube1;
        string nomeClube2;


        public override void Arrange()
        {
            crawler = new FutpediaSiteCrawler();
        }

        public override void Act()
        {
            nomeClube1 = "São Paulo";
            nomeClube2 = "Palmeiras";
            jogos = crawler.CarregarJogos(new List<Clube>() { new Clube(nomeClube1), new Clube(nomeClube2) });
        }

        [TestMethod]
        public void Deve_carregar_jogos_com_sucesso()
        {
            Assert.IsTrue(jogos.Any());

            var jogo1 = jogos.First();
            Assert.IsNotNull(jogo1.Mandante);
            Assert.IsNotNull(jogo1.Visitante);

            bool todosOsJogosDosClubesEsperados = jogos.All(j => j.Mandante.Nome == nomeClube1 || j.Visitante.Nome == nomeClube1 ||
                                                                 j.Mandante.Nome == nomeClube2 || j.Visitante.Nome == nomeClube2);
            Assert.IsTrue(todosOsJogosDosClubesEsperados);
        }
    }
}
