using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public interface IAnalisador
    {
        void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento);
    }
}