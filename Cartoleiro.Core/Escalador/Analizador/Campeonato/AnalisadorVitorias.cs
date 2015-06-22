using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Campeonato
{
    public class AnalisadorVitorias : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item => item.Jogador.Clube.Campeonato.Vitorias);
        }
    }
}