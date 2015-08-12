using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Confronto.Probabilidade
{
    public class CalculadorDeProbabilidades
    {
        // publicos
        public static IEnumerable<ProbabilidadeDeResultado> CalcularProbabilidade(Rodada rodada)
        {
            foreach (var jogo in rodada.Jogos)
            {
                yield return CalcularProbabilidade(jogo);
            }
        }

        public static ProbabilidadeDeResultado CalcularProbabilidade(Jogo jogo)
        {
            return new ProbabilidadeDeResultado(jogo);
        }
    }
}
