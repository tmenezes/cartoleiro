using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Escalador
{
    internal class Carteira
    {
        private const int TOTAL_JOGADORES = 12; // 11 + tecnico
        private const double FATOR_AJUSTE_FOCO_EM_POSICAO = 1.5;

        private readonly EsquemaTatico _esquema;
        private readonly double _patrimonio;
        private readonly Dictionary<Posicao, double> _valoresMinimos;
        private readonly Posicao? _posicaoEmFoco;
        private Dictionary<Posicao, int> _jogadoresComprados = new Dictionary<Posicao, int>();

        public Dictionary<Posicao, double> Partilha { get; set; }


        // construtores
        public Carteira(EsquemaTatico esquema, double patrimonio, Dictionary<Posicao, double> valoresMinimos)
            : this(esquema, patrimonio, valoresMinimos, null)
        {
        }

        public Carteira(EsquemaTatico esquema, double patrimonio, Dictionary<Posicao, double> valoresMinimos, Posicao? posicaoEmFoco)
        {
            _esquema = esquema;
            _patrimonio = patrimonio;
            _valoresMinimos = valoresMinimos;
            _posicaoEmFoco = posicaoEmFoco;

            PartilharDinheiro();
            ConfigurarJogadoresComprados();
        }


        // publicos
        public void ComprarJogador(Jogador jogador)
        {
            var patrimonioDaPosicao = Partilha[jogador.Posicao];
            var sobraDoPatrimonio = patrimonioDaPosicao - jogador.Preco.Atual;

            _jogadoresComprados[jogador.Posicao] += 1;

            var comprouTodosDaPosicao = _jogadoresComprados[jogador.Posicao] >= EsquemaTaticoHelper.GetNumeroDeJogadores(jogador.Posicao, _esquema);
            if (comprouTodosDaPosicao)
            {
                PartilharSobra(sobraDoPatrimonio);
            }
            else
            {
                Partilha[jogador.Posicao] = sobraDoPatrimonio;
            }
        }

        public bool PossuiCartoletasParaComprar(Jogador jogador)
        {
            return Partilha[jogador.Posicao] >= jogador.Preco.Atual;
        }


        // privados
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
                           { Posicao.Goleiro, valorIndividual },
                           { Posicao.Lateral, valorTotalParaLateral },
                           { Posicao.Zagueiro, valorTotalParaZaga },
                           { Posicao.MeioCampo, valorTotalParaMeioCampo },
                           { Posicao.Atacante, valorTotalParaAtaque },
                           { Posicao.Tecnico, valorIndividual },
                       };

            AjustarValoresMinimos();
        }

        private void PartilharComFocoEmPosicao(Posicao posicaoEmFoco)
        {
            var valorIndividual = _patrimonio / TOTAL_JOGADORES;
            var valorIndividualAjustadoDaPosicaoEmFoco = valorIndividual * FATOR_AJUSTE_FOCO_EM_POSICAO;

            var qtdeJogadoresDaPosicaoEmFoco = EsquemaTaticoHelper.GetNumeroTotalDeJogadores(posicaoEmFoco, _esquema);
            var valorTotalParaPosicaoEmFoco = qtdeJogadoresDaPosicaoEmFoco * valorIndividualAjustadoDaPosicaoEmFoco;

            var patrimonioRestante = _patrimonio - valorTotalParaPosicaoEmFoco;

            PartilharIgualmente(patrimonioRestante * 1.2);

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


        private void ConfigurarJogadoresComprados()
        {
            _jogadoresComprados = new Dictionary<Posicao, int>()
                                  {
                                      {Posicao.Goleiro, 0},
                                      {Posicao.Lateral,  0 },
                                      {Posicao.Zagueiro,  0 },
                                      {Posicao.MeioCampo, 0},
                                      {Posicao.Atacante, 0},
                                      {Posicao.Tecnico, 0},
                                  };
        }

        private void PartilharSobra(double sobraDoPatrimonio)
        {
            if (sobraDoPatrimonio <= 0)
                return;

            var posicoesNaoFinalizadas = new List<Posicao>();

            foreach (var item in _jogadoresComprados)
            {
                var posicao = item.Key;
                var jogadoresComprados = item.Value;

                var comprouTodosDaPosicao = jogadoresComprados >= EsquemaTaticoHelper.GetNumeroDeJogadores(posicao, _esquema);
                if (!comprouTodosDaPosicao)
                {
                    posicoesNaoFinalizadas.Add(posicao);
                }
            }

            var sobraPorPosicao = sobraDoPatrimonio / posicoesNaoFinalizadas.Count;
            foreach (var posicao in posicoesNaoFinalizadas)
            {
                Partilha[posicao] += sobraPorPosicao;
            }
        }

        private void AjustarValoresMinimos()
        {
            var posicoes = Enum.GetValues(typeof(Posicao)).OfType<Posicao>();

            foreach (var posicao in posicoes)
            {
                var numeroDeJogadores = EsquemaTaticoHelper.GetNumeroDeJogadores(posicao, _esquema);
                var valorIndividual = Partilha[posicao] / numeroDeJogadores;
                var valorMinimo = _valoresMinimos[posicao];

                if (valorIndividual < valorMinimo)
                {
                    var valorAjustado = valorMinimo * numeroDeJogadores;
                    Partilha[posicao] = valorAjustado;
                }
            }
        }
    }
}