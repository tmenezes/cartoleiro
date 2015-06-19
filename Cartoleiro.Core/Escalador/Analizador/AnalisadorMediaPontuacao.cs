using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorMediaPontuacao : IAnalisador
    {
        private const int MAX_PONTOS_ANALISADOR = 10;

        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            var maiorMediaDePontuacao = ranqueamento.Max(item => item.Jogador.Pontuacao.Media);

            foreach (var item in ranqueamento)
            {
                var pontos = item.Jogador.Pontuacao.Media * MAX_PONTOS_ANALISADOR / maiorMediaDePontuacao;

                item.AddPontos(pontos);
            }
        }
    }
}