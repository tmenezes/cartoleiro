using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;

namespace Cartoleiro.Core.Extensions
{
    public static class IEnumerableRanqueamentoExtensions
    {
        public static IEnumerable<Jogador> JogadoresMelhoresPontuados(this IEnumerable<PontuacaoDeEscalacao> ranqueamento, Posicao posicao)
        {
            return ranqueamento.Where(i => i.Jogador.Posicao == posicao)
                                .OrderByDescending(i => i.Pontos)
                                .Select(i => i.Jogador)
                                .ToList();
        }
    }
}
