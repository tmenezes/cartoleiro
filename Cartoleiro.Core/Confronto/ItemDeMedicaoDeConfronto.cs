using System;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Confronto
{
    public class ItemDeMedicaoDeConfronto
    {
        public string Descricao { get; set; }
        public Clube Vencedor { get; set; }
        public double ResultadoMandante { get; set; }
        public double ResultadoVisitante { get; set; }
        public string Formatacao { get; set; }


        public ItemDeMedicaoDeConfronto(string descricao, Clube vencedor, double resultadoMandante, double resultadoVisitante)
            : this(descricao, vencedor, resultadoMandante, resultadoVisitante, "N")
        {
        }

        public ItemDeMedicaoDeConfronto(string descricao, Clube vencedor, double resultadoMandante, double resultadoVisitante, string formatacao)
        {
            Descricao = descricao;
            Vencedor = vencedor;
            ResultadoMandante = resultadoMandante;
            ResultadoVisitante = resultadoVisitante;
            Formatacao = formatacao;
        }


        public override string ToString()
        {
            return string.Format("Mandante {0} - {1} Visitante ({2})", ResultadoMandante, ResultadoVisitante, Descricao);
        }
    }
}