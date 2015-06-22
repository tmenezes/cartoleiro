using System;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cartoleiro.Testes.Core.Escalador.EscaladorDeTimeTestes
{
    [TestClass]
    public class Ao_escalar_time : AbstractTesteAutoAct
    {
        Time time;
        CartolaStubDataSource cartolaDS;
        EscaladorDeTime escaladorDeTime;

        public override void Arrange()
        {
            var analizadores = new AnalisadorBuilder().PontuacaoMedia().Analisadores;

            cartolaDS = new CartolaStubDataSource();
            escaladorDeTime = new EscaladorDeTime(cartolaDS).ComAnalisadores(analizadores);
        }

        public override void Act()
        {
            time = escaladorDeTime.MontarTime();
        }

        [TestMethod]
        public void Time_nao_deve_ser_nulo()
        {
            Assert.IsNotNull(time);
        }

        [TestMethod]
        public void Time_deve_ter_formacao_correta()
        {
            Assert.AreEqual(EsquemaTatico._442, time.EsquemaTatico);
        }

        [TestMethod]
        public void Time_deve_conter_11_jogadores_mais_tecnico()
        {
            Assert.AreEqual(12, time.TotalDeJogadores);
        }

        [TestMethod]
        public void Time_deve_valor_menor_igual_ao_limite_de_dinheiro()
        {
            Assert.IsTrue(time.ValorDoTime <= 100);
        }

        [TestMethod]
        public void Time_deve_ser_valido()
        {
            Assert.IsTrue(time.EsValido);
        }
    }
}
