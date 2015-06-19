using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Escalador
{
    internal class PartilhaDeDinheito
    {
        private const int TOTAL_JOGADORES = 12; // 11 + tecnico
        private const double FATOR_AJUSTE_FOCO_EM_POSICAO = 1.5;

        private readonly EsquemaTatico _esquema;
        private readonly double _patrimonio;
        private readonly Posicao? _posicaoEmFoco;

        public Dictionary<Posicao, double> Partilha { get; set; }


        public PartilhaDeDinheito(EsquemaTatico esquema, double patrimonio)
        {
            _posicaoEmFoco = null;
            _esquema = esquema;
            _patrimonio = patrimonio;

            PartilharDinheiro();
        }

        public PartilhaDeDinheito(EsquemaTatico esquema, double patrimonio, Posicao? posicaoEmFoco)
        {
            _esquema = esquema;
            _posicaoEmFoco = posicaoEmFoco;
            _patrimonio = patrimonio;

            PartilharDinheiro();
        }


        private void PartilharDinheiro()
        {
            if (_posicaoEmFoco.HasValue)
            {
                PartilharComFocoEmPosicao(_posicaoEmFoco.Value);
            }
            else
            {
                PartilharIgualmente();
            }
        }

        private void PartilharIgualmente()
        {
            PartilharIgualmente(_patrimonio);
        }

        private void PartilharIgualmente(double patrimonio)
        {
            var valorIndividual = patrimonio / TOTAL_JOGADORES;

            var valorTotalParaLateral = EsquemaTaticoHelper.NumeroDeLaterais() * valorIndividual;
            var valorTotalParaZaga = EsquemaTaticoHelper.NumeroDeZagueiros(_esquema) * valorIndividual;
            var valorTotalParaMeioCampo = EsquemaTaticoHelper.NumeroDeMeioCampos(_esquema) * valorIndividual;
            var valorTotalParaAtaque = EsquemaTaticoHelper.NumeroDeAtacantes(_esquema) * valorIndividual;

            Partilha = new Dictionary<Posicao, double>()
                       {
                           {Posicao.Goleiro, valorIndividual},
                           {Posicao.Lateral,  valorTotalParaLateral },
                           {Posicao.Zagueiro,  valorTotalParaZaga },
                           {Posicao.MeioCampo, valorTotalParaMeioCampo},
                           {Posicao.Atacante, valorTotalParaAtaque},
                           {Posicao.Tecnico, valorIndividual},
                       };
        }

        private void PartilharComFocoEmPosicao(Posicao posicaoEmFoco)
        {
            var valorIndividual = _patrimonio / TOTAL_JOGADORES;
            var valorIndividualAjustadoDaPosicaoEmFoco = valorIndividual * FATOR_AJUSTE_FOCO_EM_POSICAO;

            var qtdeJogadoresDaPosicaoEmFoco = EsquemaTaticoHelper.GetNumeroTotalDeJogadores(posicaoEmFoco, _esquema);
            var valorTotalParaPosicaoEmFoco = qtdeJogadoresDaPosicaoEmFoco * valorIndividualAjustadoDaPosicaoEmFoco;

            var patrimonioRestante = _patrimonio - valorTotalParaPosicaoEmFoco;

            PartilharIgualmente(patrimonioRestante);

            bool posicaoEmFocoPossuiLaterais = EsquemaTaticoHelper.PossuiLaterais(posicaoEmFoco, _esquema);
            if (posicaoEmFocoPossuiLaterais)
            {
                Partilha[posicaoEmFoco] = valorTotalParaPosicaoEmFoco / 2;
                Partilha[Posicao.Lateral] = valorTotalParaPosicaoEmFoco / 2;
            }
            else
            {
                Partilha[posicaoEmFoco] = valorTotalParaPosicaoEmFoco;
            }
        }
    }
}