using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public abstract class AnalisadorGenerico
    {
        protected void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento, Func<PontuacaoDeEscalacao, int> indicadorEmAnalise)
        {
            var maiorIndicador = ranqueamento.Max(item => indicadorEmAnalise(item));

            foreach (var item in ranqueamento)
            {
                var pontos = indicadorEmAnalise(item) * Analisadores.MAX_PONTOS_ANALISADOR / maiorIndicador;

                item.AddPontos(pontos);
            }
        }

        protected void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento, Func<PontuacaoDeEscalacao, double> indicadorEmAnalise)
        {
            var maiorIndicador = ranqueamento.Max(item => indicadorEmAnalise(item));

            foreach (var item in ranqueamento)
            {
                var pontos = indicadorEmAnalise(item) * Analisadores.MAX_PONTOS_ANALISADOR / maiorIndicador;

                item.AddPontos(pontos);
            }
        }
    }
}