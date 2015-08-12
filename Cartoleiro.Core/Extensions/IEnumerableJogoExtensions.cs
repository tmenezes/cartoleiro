using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Extensions
{
    public static class IEnumerableJogoExtensions
    {
        public static IEnumerable<Jogo> ComoMandante(this IEnumerable<Jogo> jogos, Clube clube)
        {
            return jogos.Where(j => j.Mandante == clube);
        }

        public static double PercentualDeVitorias(this IEnumerable<Jogo> jogos, Clube clube)
        {
            return jogos.Count(j => j.Vencedor() == clube) / (double)jogos.Count() * 100;
        }

        public static double PercentualDeEmpates(this IEnumerable<Jogo> jogos)
        {
            return jogos.Count(j => j.Vencedor() == null) / (double)jogos.Count() * 100;
        }
    }
}