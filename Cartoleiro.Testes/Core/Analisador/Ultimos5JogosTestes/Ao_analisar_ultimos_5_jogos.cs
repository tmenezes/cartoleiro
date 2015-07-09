using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador.Campeonato;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Core.Analisador.Ultimos5JogosTestes
{
    [TestClass]
    public class Ao_analisar_ultimos_5_jogos : AbstractTesteAutoAct
    {
        Jogador jogador1;
        Jogador jogador2;
        Jogador jogador3;

        List<PontuacaoDeEscalacao> ranqueamento;
        AnalisadorUltimos5Jogos analisador;

        public override void Arrange()
        {
            var saoPaulo = new Clube("São Paulo") { Campeonato = new Campeonato() { UltimosJogos = new UltimosJogos(2, 1, 2) } };
            var flamengo = new Clube("Flamengo") { Campeonato = new Campeonato() { UltimosJogos = new UltimosJogos(5, 0, 0) } };
            var inter = new Clube("Internacional") { Campeonato = new Campeonato() { UltimosJogos = new UltimosJogos(1, 1, 0) } };

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

            analisador = new AnalisadorUltimos5Jogos();
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
        public void Pontuacao_do_jogador_3_deve_ser_3()
        {
            var pontuacaoJogador3 = ranqueamento.First(i => i.Jogador == jogador3);
            Assert.AreEqual(3, pontuacaoJogador3.Pontos);
        }
    }
}
