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
                var estaMelhorNoCampeonato = clubeDoJogador.Campeonato.Pontos >= clubeAdversario.Campeonato.Pontos;
                var ataqueMelhorQueDefesa = clubeDoJogador.Campeonato.GolsPro >= clubeAdversario.Campeonato.GolsContra;

                if (esMandante)
                    pontos += 10;

                if (estaMelhorNoCampeonato)
                    pontos += 10;

                if (ataqueMelhorQueDefesa)
                    pontos += 10;

                return pontos;
            });
        }
    }
}