using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Confronto;
using Cartoleiro.Core.Data;

namespace Cartoleiro.Core.Escalador.Analizador.Confronto
{
    public class AnalisadorPesoNoConfronto : AnalisadorGenerico, IAnalisador
    {
        private static Dictionary<Clube, ResultadoDoConfronto> _medicoesDeConfrontos;

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
                    ? resultadoDoConfronto.TotalMandante + 1
                    : resultadoDoConfronto.TotalVisitante;
            });
        }

        private static void MedirPesoDosConfrontos()
        {
            _medicoesDeConfrontos = new Dictionary<Clube, ResultadoDoConfronto>();

            foreach (var jogo in Cartola.Campeonato.Rodadas.ProximaRodada.Jogos)
            {
                var resultaDoConfronto = new MedidorDeConfronto(jogo.Mandante, jogo.Visitante).MedirConfronto();

                _medicoesDeConfrontos.Add(jogo.Mandante, resultaDoConfronto);
                _medicoesDeConfrontos.Add(jogo.Visitante, resultaDoConfronto);
            }
        }
    }
}