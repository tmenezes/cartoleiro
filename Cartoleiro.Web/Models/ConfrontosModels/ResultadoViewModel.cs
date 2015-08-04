using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Confronto;

namespace Cartoleiro.Web.Models.ConfrontosModels
{
    public class ResultadoViewModel
    {
        public ResultadoDoConfronto ResultadoDoConfronto { get; set; }
        public Clube Clube { get; set; }

        public ResultadoViewModel(ResultadoDoConfronto resultadoDoConfronto, Clube clube)
        {
            ResultadoDoConfronto = resultadoDoConfronto;
            Clube = clube;
        }

        public string GetResultado(ItemDeMedicaoDeConfronto item)
        {
            return (Clube == ResultadoDoConfronto.Mandande)
                ? item.ResultadoMandante.ToString(item.Formatacao)
                : item.ResultadoVisitante.ToString(item.Formatacao);
        }

        public string GetCssPosicaoResultado(ItemDeMedicaoDeConfronto item)
        {
            return (Clube == ResultadoDoConfronto.Mandande)
                ? "pull-right text-right"
                : "pull-left text-left";
        }

        public string GetCssLabelResultado(ItemDeMedicaoDeConfronto item)
        {
            return (Clube == item.Vencedor)
                ? "label-success"
                : "label-default";
        }

        public string GetCssPosicaoDescricao(ItemDeMedicaoDeConfronto item)
        {
            return (Clube == ResultadoDoConfronto.Mandande)
                ? "pull-right"
                : "pull-left";
        }
    }
}
