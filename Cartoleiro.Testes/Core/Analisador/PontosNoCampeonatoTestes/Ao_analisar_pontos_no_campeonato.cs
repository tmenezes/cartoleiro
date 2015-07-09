using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador.Campeonato;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Core.Analisador.PontosNoCampeonatoTestes
{
    [TestClass]
    public class Ao_analisar_pontos_no_campeonato : AbstractTesteAutoAct
    {
        Jogador jogador1;
        Jogador jogador2;
        Jogador jogador3;

        List<PontuacaoDeEscalacao> ranqueamento;
        AnalisadorPontuacaoNoCampeonato analisador;

        public override void Arrange()
        {
            var saoPaulo = new Clube("São Paulo") { Campeonato = new Campeonato() { Pontos = 10 } };
            var flamengo = new Clube("Flamengo") { Campeonato = new Campeonato() { Pontos = 20 } };
            var inter = new Clube("Internacional") { Campeonato = new Campeonato() { Pontos = 5 } };

            jogador1 = new Jogador("1", saoPaulo, Posicao.Atacante)
            {
                Pontuacao = new Pontuacao(50, 60)
            };
            jogador2 = new Jogador("2", flamengo, Posicao.Atacante)
            {
                Pontuacao = new Pontuacao(100, 100)
            };
            jogador3 = new Jogador("3", inter, Posicao.Atacante)
            {
                Pontuacao = new Pontuacao(25, 15)
            };

            ranqueamento = new List<PontuacaoDeEscalacao>()
                           {
                               new PontuacaoDeEscalacao(jogador1),
                               new PontuacaoDeEscalacao(jogador2),
                               new PontuacaoDeEscalacao(jogador3),
                           };

            analisador = new AnalisadorPontuacaoNoCampeonato();
        }

        public override void Act()
        {
            analisador.Analisar(ranqueamento);
        }

        [TestMethod]
        public void Pontuacao_do_jogador_1_deve_ser_5()
        {
            var pontuacaoJogador1 = ranqueamento.First(i => i.Jogador == jogador1);
            Assert.AreEqual(5, pontuacaoJogador1.Pontos);
        }

        [TestMethod]
        public void Pontuacao_do_jogador_2_deve_ser_10()
        {
            var pontuacaoJogador2 = ranqueamento.First(i => i.Jogador == jogador2);
            Assert.AreEqual(10, pontuacaoJogador2.Pontos);
        }

        [TestMethod]
        public void Pontuacao_do_jogador_3_deve_ser_2_e_meio()
        {
            var pontuacaoJogador3 = ranqueamento.First(i => i.Jogador == jogador3);
            Assert.AreEqual(2.5, pontuacaoJogador3.Pontos);
        }
    }
}
