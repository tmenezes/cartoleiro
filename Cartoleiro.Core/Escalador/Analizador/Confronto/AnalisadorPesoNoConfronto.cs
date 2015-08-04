using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Confronto;

namespace Cartoleiro.Core.Escalador.Analizador.Confronto
{
    public class AnalisadorPesoNoConfronto : AnalisadorGenerico, IAnalisador
    {
        private static Dictionary<Clube, ResultadoDoConfronto> _medicoesDeConfrontos;
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
            _medicoesDeConfrontos = new Dictionary<Clube, ResultadoDoConfronto>();

            var tiposDeMedicao = new List<TipoMedicao>()
                                 {
                                     TipoMedicao.PontosNoCampeonato, TipoMedicao.AproveitamentoNoCampeonato, TipoMedicao.SaldoDeGols,
                                     TipoMedicao.MediaDaDefesa, TipoMedicao.MediaDaMeioCampo, TipoMedicao.MediaDaAtaque,
                                 };

            foreach (var jogo in Cartola.Campeonato.Rodadas.ProximaRodada.Jogos)
            {
                var resultaDoConfronto = new MedidorDeConfronto(jogo.Mandante, jogo.Visitante).MedirConfronto(tiposDeMedicao);

                _medicoesDeConfrontos.Add(jogo.Mandante, resultaDoConfronto);
                _medicoesDeConfrontos.Add(jogo.Visitante, resultaDoConfronto);
            }

            _adicionalPorSerMandante = _medicoesDeConfrontos.First().Value.ItensDeMedicao.Count() * 0.25;
        }
    }
}