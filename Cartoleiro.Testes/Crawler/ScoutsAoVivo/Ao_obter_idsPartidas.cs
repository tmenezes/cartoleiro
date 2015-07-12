using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Web.AppCode.ScoutsAoVivo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Crawler.ScoutsAoVivo
{
    [TestClass]
    public class Ao_obter_idsPartidas : AbstractTesteAutoAct
    {
        ScoutsAoVivoJogosCrawler crawler;
        IEnumerable<string> idsPartidas;


        public override void Arrange()
        {
            crawler = new ScoutsAoVivoJogosCrawler();
        }

        public override void Act()
        {
            idsPartidas = crawler.CarregarDosJogos();
        }

        [TestMethod]
        public void Deve_carregar_partidas_com_sucesso()
        {
            Assert.IsTrue(idsPartidas.Any());

            Assert.IsTrue(idsPartidas.Any(i => i.Contains("flamengo")));
        }
    }
}
