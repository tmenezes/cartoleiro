using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.AppCode.Extensions
{
    public static class JogoExtensions
    {
        public static string GetIdPartidaScoutsOnLine(this Jogo jogo)
        {
            var rodada = Campeonato.Rodadas.First(r => r.Numero == jogo.NumeroDaRodada);

            var numeroDoJogo = rodada.Jogos.Select((j, i) => new { Jogo = j, Numero = i })
                                           .First(item => item.Jogo == jogo).Numero;

            int numeroDaPartida = (jogo.NumeroDaRodada - 1) * 10 + numeroDoJogo + 1;

            var nomeMandante = GetNomeDoClubeParaScoutsOnLine(jogo.Mandante);
            var nomeVisitante = GetNomeDoClubeParaScoutsOnLine(jogo.Visitante);

            var idPartida = string.Format("{0}_{1}_{2}", numeroDaPartida, nomeMandante, nomeVisitante);

            return idPartida;
        }

        private static string GetNomeDoClubeParaScoutsOnLine(Clube clube)
        {
            var clubeSemAcento = ModelUtils.RemoverAcentos(clube.Nome.ToLower().Replace(" ", "-"));

            return clubeSemAcento;
        }
    }
}
