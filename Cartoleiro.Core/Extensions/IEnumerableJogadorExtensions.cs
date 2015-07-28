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
            return jogadores.Where(j => j.Clube == clube).OrdenarComoTime();
        }

        public static IEnumerable<Jogador> JogadoresTitularesDoClube(this IEnumerable<Jogador> jogadores, Clube clube)
        {
            return ElencoDoClube(jogadores, clube).Where(j => j.Status == Status.Provavel).OrdenarComoTime();
        }

        public static IEnumerable<Jogador> OrdenarComoTime(this IEnumerable<Jogador> jogadores)
        {
            return jogadores.OrderBy(j => (int)j.Posicao);
        }


        public static IEnumerable<Jogador> DoClube(this IEnumerable<Jogador> jogadores, Clube clube)
        {
            return jogadores.Where(j => j.Clube == clube);
        }

        public static IEnumerable<Jogador> DaDefesa(this IEnumerable<Jogador> jogadores)
        {
            return jogadores.Where(j => (j.Posicao == Posicao.Zagueiro || j.Posicao == Posicao.Lateral));
        }

        public static IEnumerable<Jogador> DoMeioCampo(this IEnumerable<Jogador> jogadores)
        {
            return jogadores.Where(j => j.Posicao == Posicao.MeioCampo);
        }

        public static IEnumerable<Jogador> DoAtaque(this IEnumerable<Jogador> jogadores)
        {
            return jogadores.Where(j => j.Posicao == Posicao.Atacante);
        }

        public static double Media(this IEnumerable<Jogador> jogadores, Func<Jogador, double> avgFunc)
        {
            return jogadores.Any()
                ? jogadores.Average(avgFunc)
                : 0;
        }
    }
}
