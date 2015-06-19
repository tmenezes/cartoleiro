using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorUltimaPontuacao : IAnalisador
    {
        private const int MAX_PONTOS_ANALISADOR = 10;

        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            var maiorUltimaPontuacao = ranqueamento.Max(item => item.Jogador.Pontuacao.Ultima);

            foreach (var item in ranqueamento)
            {
                var pontos = item.Jogador.Pontuacao.Ultima * MAX_PONTOS_ANALISADOR / maiorUltimaPontuacao;

                item.AddPontos(pontos);
            }
        }
    }
}