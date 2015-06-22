using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.ScoutsCartola;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Crawler.ScoutsCartola
{
    [TestClass]
    public class Ao_obter_jogadores : AbstractTesteAutoAct
    {
        ScoutsCartolaSiteCrawler crawler;
        IEnumerable<Jogador> jogadores;


        public override void Arrange()
        {
            var uri = new Uri("http://www.scoutscartola.com");

            crawler = new ScoutsCartolaSiteCrawler(uri);
        }

        public override void Act()
        {
            jogadores = crawler.CarregarJogadores(2);
        }

        [TestMethod]
        public void Deve_carregar_jogadores_com_sucesso()
        {
            Assert.IsTrue(jogadores.Any());

            var jogador1 = jogadores.First();
            Assert.IsNotNull(jogador1.Nome);
            Assert.IsNotNull(jogador1.Clube);
            Assert.IsNotNull(jogador1.Pontuacao);
            Assert.IsTrue(Enum.IsDefined(typeof(Status), jogador1.Status));

            Assert.IsNotNull(jogador1.Scouts);
            Assert.IsTrue(jogador1.Scouts.TotalDePositivos + jogador1.Scouts.TotalDeNegativos > 0);
        }
    }
}
