using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Jogador
{
    public class AnalisadorScoutsPositivos : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item => item.Jogador.Scouts.TotalDePositivos);
        }
    }
}