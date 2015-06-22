using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador.Jogador;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Core.Analisador.ScoutsNegativosTestes
{
    [TestClass]
    public class Ao_analisar_scouts_negativos : AbstractTesteAutoAct
    {
        Jogador jogador1;
        Jogador jogador2;
        Jogador jogador3;

        List<PontuacaoDeEscalacao> ranqueamento;
        AnalisadorScoutsNegativos analisador;

        public override void Arrange()
        {
            var flamengo = new Clube("Flamengo");

            jogador1 = new Jogador("1", flamengo, Posicao.Atacante)
                       {
                           Scouts = new Scouts() { PassesErrados = 15, FaltasCometidas = 15, Gols = 1 },
                       };
            jogador2 = new Jogador("2", flamengo, Posicao.Atacante)
                       {
                           Scouts = new Scouts() { PassesErrados = 5, FaltasCometidas = 5, Gols = 10 },
                       };
            jogador3 = new Jogador("3", flamengo, Posicao.Atacante)
                       {
                           Scouts = new Scouts() { PassesErrados = 30, FaltasCometidas = 20, Gols = 10 },
                       };

            ranqueamento = new List<PontuacaoDeEscalacao>()
                           {
                               new PontuacaoDeEscalacao(jogador1),
                               new PontuacaoDeEscalacao(jogador2),
                               new PontuacaoDeEscalacao(jogador3),
                           };

            analisador = new AnalisadorScoutsNegativos();
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
