using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Confronto.Indicador;

namespace Cartoleiro.Core.Escalador.Analizador.Confronto
{
    public class AnalisadorPesoNoConfronto : AnalisadorGenerico, IAnalisador
    {
        private static Dictionary<Clube, ResultadoDosIndicadores> _medicoesDeConfrontos;
        private static double _adicionalPorSerMandante = 1;

        static AnalisadorPesoNoConfronto()
        {
            MedirPesoDosConfrontos();
        }


        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item =>
            {
                if (Cartola.Campeonato.Rodadas.ProximaRodada == null)
                    return 0;

                var proximaRodada = Cartola.Campeonato.Rodadas.ProximaRodada;
                var clubeDoJogador = item.Jogador.Clube;
                var esMandante = proximaRodada.EsMandante(clubeDoJogador);
                var resultadoDoConfronto = _medicoesDeConfrontos[clubeDoJogador];

                return (esMandante)
                    ? resultadoDoConfronto.TotalMandante + _adicionalPorSerMandante
                    : resultadoDoConfronto.TotalVisitante;
            });
        }

        private static void MedirPesoDosConfrontos()
        {
            _medicoesDeConfrontos = new Dictionary<Clube, ResultadoDosIndicadores>();

            var tiposDeMedicao = new List<TipoDeIndicador>()
                                 {
                                     TipoDeIndicador.PontosNoCampeonato, TipoDeIndicador.AproveitamentoNoCampeonato, TipoDeIndicador.SaldoDeGols,
                                     TipoDeIndicador.MediaDaDefesa, TipoDeIndicador.MediaDaMeioCampo, TipoDeIndicador.MediaDaAtaque,
                                 };

            foreach (var jogo in Cartola.Campeonato.Rodadas.ProximaRodada.Jogos)
            {
                var resultaDoConfronto = new CalculadorDeIndicadores(jogo.Mandante, jogo.Visitante).CalcularConfronto(tiposDeMedicao);

                _medicoesDeConfrontos.Add(jogo.Mandante, resultaDoConfronto);
                _medicoesDeConfrontos.Add(jogo.Visitante, resultaDoConfronto);
            }

            _adicionalPorSerMandante = _medicoesDeConfrontos.First().Value.Indicadores.Count() * 0.25;
        }
    }
}