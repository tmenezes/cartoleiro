using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Campeonato
{
    public class AnalisadorGolsContra : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            AnalisarInvertido(ranqueamento, item => item.Jogador.Clube.Campeonato.GolsContra);
        }
    }
}