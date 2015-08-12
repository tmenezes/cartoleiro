using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Confronto.Indicador;

namespace Cartoleiro.Web.Models.ConfrontosModels
{
    public class ResultadoViewModel
    {
        public ResultadoDosIndicadores Resultado { get; set; }
        public Clube Clube { get; set; }

        public ResultadoViewModel(ResultadoDosIndicadores resultado, Clube clube)
        {
            Resultado = resultado;
            Clube = clube;
        }

        public string GetResultado(Indicador item)
        {
            return (Clube == Resultado.Mandande)
                ? item.ResultadoMandante.ToString(item.Formatacao)
                : item.ResultadoVisitante.ToString(item.Formatacao);
        }

        public string GetCssPosicaoResultado(Indicador item)
        {
            return (Clube == Resultado.Mandande)
                ? "pull-right text-right"
                : "pull-left text-left";
        }

        public string GetCssLabelResultado(Indicador item)
        {
            return (Clube == item.Vencedor)
                ? "label-success"
                : "label-default";
        }

        public string GetCssPosicaoDescricao(Indicador item)
        {
            return (Clube == Resultado.Mandande)
                ? "pull-right"
                : "pull-left";
        }
    }
}
