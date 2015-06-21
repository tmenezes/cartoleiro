using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorUltimos5Jogos : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item =>
            {
                var ultimosJogos = item.Jogador.Clube.Campeonato.UltimosJogos;

                return (ultimosJogos.Vitorias * 2) + (ultimosJogos.Empates);
            });
        }
    }
}