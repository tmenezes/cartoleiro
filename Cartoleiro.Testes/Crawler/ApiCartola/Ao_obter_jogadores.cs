﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.ApiCartola;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Crawler.ApiCartola
{
    [TestClass]
    [Ignore]
    public class Ao_obter_jogadores : AbstractTesteAutoAct
    {
        ApiCartolaSiteCrawler crawler;
        IEnumerable<Jogador> jogadores;


        public override void Arrange()
        {
            crawler = new ApiCartolaSiteCrawler();
        }

        public override void Act()
        {
            jogadores = crawler.CarregarJogadores();
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

            if (jogador1.Jogos > 0)
            {
                Assert.IsNotNull(jogador1.Scouts);
                Assert.IsTrue(jogador1.Scouts.TotalDePositivos + jogador1.Scouts.TotalDeNegativos > 0);
            }
        }
    }
}
