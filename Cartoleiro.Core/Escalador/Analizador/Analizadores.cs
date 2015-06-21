using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class Analisadores : List<IAnalisador>
    {
        internal const double MAX_PONTOS_ANALISADOR = 10;

        public void ExecutarAnalises(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            foreach (var analisador in this)
            {
                analisador.Analisar(ranqueamento);
            }
        }
    }
}
