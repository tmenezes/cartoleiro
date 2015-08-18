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

        public static string GetCssDeIconeDoResultadoDoJogo(this Jogo jogo, Clube clube)
        {
            if (jogo.Empate())
                return "icone-circulo-empate";

            return jogo.Vencedor() == clube
                ? "icone-circulo-vitoria"
                : "icone-circulo-derrota";
        }
    }
}
