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

        public static IEnumerable<Jogador> ElencoDoClube(this IEnumerable<Jogador> jogadores, Clube clube)
        {
            return jogadores.Where(j => j.Clube == clube);
        }
        public static IEnumerable<Jogador> JogadoresTitularesDoClube(this IEnumerable<Jogador> jogadores, Clube clube)
        {
            return ElencoDoClube(jogadores, clube).Where(j => j.Status == Status.Provavel);
        }
    }
}
