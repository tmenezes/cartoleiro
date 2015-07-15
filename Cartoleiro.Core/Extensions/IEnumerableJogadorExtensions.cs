using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Extensions
{
    public static class IEnumerableJogadorExtensions
    {
        public static void SetClubes(this IEnumerable<Jogador> jogadores, IEnumerable<Clube> clubes)
        {
            foreach (var jogador in jogadores)
            {
                var clube = clubes.FirstOrDefault(c => string.Equals(c.Nome, jogador.Clube.Nome, StringComparison.CurrentCultureIgnoreCase));

                if (clube != null)
                    jogador.Clube = clube;

                if (jogador.Scouts == null)
                    jogador.Scouts = new Scouts();
            }
        }
    }
}
