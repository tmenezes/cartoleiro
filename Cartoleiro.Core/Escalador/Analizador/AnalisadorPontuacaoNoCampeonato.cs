using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorPontuacaoNoCampeonato : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item => item.Jogador.Clube.Campeonato.Pontos);
        }
    }
}