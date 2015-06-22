using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Jogador
{
    public class AnalisadorScoutsNegativos : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            AnalisarInvertido(ranqueamento, item => item.Jogador.Scouts.TotalDeNegativos);
        }
    }
}