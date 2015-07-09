using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.AppCode.Extensions
{
    public static class JogoExtensions
    {
        public static string GetIdJogo(this Jogo jogo)
        {
            var nomeMandante = jogo.Mandante.GetNomeNormalizado();
            var nomeVisitante = jogo.Visitante.GetNomeNormalizado();

            var idPartida = string.Format("{0}_{1}", nomeMandante, nomeVisitante);
            return idPartida;
        }
    }
}
