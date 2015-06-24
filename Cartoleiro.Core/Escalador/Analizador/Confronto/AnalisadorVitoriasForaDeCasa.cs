using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Confronto
{
    public class AnalisadorVitoriasForaDeCasa : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item => item.Jogador.Clube.Campeonato.VitoriasForaDeCasa);
        }
    }
}