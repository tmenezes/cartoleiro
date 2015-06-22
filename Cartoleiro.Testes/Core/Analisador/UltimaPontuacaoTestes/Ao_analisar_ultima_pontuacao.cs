using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.Core.Escalador.Analizador.Jogador;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Core.Analisador.UltimaPontuacaoTestes
{
    [TestClass]
    public class Ao_analisar_ultima_pontuacao : AbstractTesteAutoAct
    {
        Jogador jogador1;
        Jogador jogador2;
        Jogador jogador3;

        List<PontuacaoDeEscalacao> ranqueamento;
        AnalisadorUltimaPontuacao analisador;

        public override void Arrange()
        {
            var flamengo = new Clube("Flamengo");

            jogador1 = new Jogador("1", flamengo, Posicao.Atacante)
                       {
                           Pontuacao = new Pontuacao(50, 60)
                       };
            jogador2 = new Jogador("2", flamengo, Posicao.Atacante)
                       {
                           Pontuacao = new Pontuacao(100, 100)
                       };
            jogador3 = new Jogador("3", flamengo, Posicao.Atacante)
                       {
                           Pontuacao = new Pontuacao(25, 15)
                       };

            ranqueamento = new List<PontuacaoDeEscalacao>()
                           {
                               new PontuacaoDeEscalacao(jogador1),
                               new PontuacaoDeEscalacao(jogador2),
                               new PontuacaoDeEscalacao(jogador3),
                           };

            analisador = new AnalisadorUltimaPontuacao();
        }

        public override void Act()
        {
            analisador.Analisar(ranqueamento);
        }

        [TestMethod]
        public void Pontuacao_do_jogador_1_deve_ser_6()
        {
            var pontuacaoJogador1 = ranqueamento.First(i => i.Jogador == jogador1);
            Assert.AreEqual(6, pontuacaoJogador1.Pontos);
        }

        [TestMethod]
        public void Pontuacao_do_jogador_2_deve_ser_10()
        {
            var pontuacaoJogador2 = ranqueamento.First(i => i.Jogador == jogador2);
            Assert.AreEqual(10, pontuacaoJogador2.Pontos);
        }

        [TestMethod]
        public void Pontuacao_do_jogador_3_deve_ser_1_e_meio()
        {
            var pontuacaoJogador3 = ranqueamento.First(i => i.Jogador == jogador3);
            Assert.AreEqual(1.5, pontuacaoJogador3.Pontos);
        }
    }
}
