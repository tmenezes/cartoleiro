using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Core.Analisador.GolsContraTestes
{
    [TestClass]
    public class Ao_analisar_gols_contra : AbstractTesteAutoAct
    {
        Jogador jogador1;
        Jogador jogador2;
        Jogador jogador3;

        List<PontuacaoDeEscalacao> ranqueamento;
        AnalisadorGolsContra analisador;

        public override void Arrange()
        {
            var saoPaulo = new Clube("São Paulo") { Campeonato = new Campeonato() { GolsContra = 5 } };
            var flamengo = new Clube("Flamengo") { Campeonato = new Campeonato() { GolsContra = 0 } };
            var inter = new Clube("Internacional") { Campeonato = new Campeonato() { GolsContra = 10 } };

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

            analisador = new AnalisadorGolsContra();
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
        public void Pontuacao_do_jogador_3_deve_ser_zero()
        {
            var pontuacaoJogador3 = ranqueamento.First(i => i.Jogador == jogador3);
            Assert.AreEqual(0, pontuacaoJogador3.Pontos);
        }
    }
}
