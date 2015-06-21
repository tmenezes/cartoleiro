using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorGolsPro : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item => item.Jogador.Clube.Campeonato.GolsPro);
        }
    }
}