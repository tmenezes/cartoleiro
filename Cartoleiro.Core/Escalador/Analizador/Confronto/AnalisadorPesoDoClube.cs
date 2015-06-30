using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Confronto
{
    public class AnalisadorPesoDoClube : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item =>
            {
                if (Cartola.Campeonato.Rodadas.ProximaRodada == null)
                    return 0;

                var pontos = 0;
                var proximaRodada = Cartola.Campeonato.Rodadas.ProximaRodada;
                var clubeDoJogador = item.Jogador.Clube;
                var clubeAdversario = proximaRodada.GetJogoDoClube(clubeDoJogador).GetAdversario(clubeDoJogador);

                var esMandante = proximaRodada.EsMandante(clubeDoJogador);
                var melhorSaldo = clubeDoJogador.Campeonato.SaldoDeGol >= clubeAdversario.Campeonato.SaldoDeGol;


                pontos += clubeDoJogador.Campeonato.Pontos;
                pontos += (esMandante) ? 5 : 0;
                pontos += (melhorSaldo) ? 5 : 0;

                return pontos;
            });
        }
    }
}