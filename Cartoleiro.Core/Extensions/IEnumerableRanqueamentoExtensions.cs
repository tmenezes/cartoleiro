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
            return MelhoresPontuados(ranqueamento, posicao).Select(i => i.Jogador)
                                                           .ToList();
        }

        public static IEnumerable<PontuacaoDeEscalacao> MelhoresPontuados(this IEnumerable<PontuacaoDeEscalacao> ranqueamento, Posicao posicao)
        {
            return ranqueamento.Where(i => i.Jogador.Posicao == posicao)
                               .OrderByDescending(i => i.Pontos);
        }

        public static IEnumerable<PontuacaoDeEscalacao> MelhoresPontuados(this IEnumerable<PontuacaoDeEscalacao> ranqueamento, Posicao posicao1, Posicao posicao2)
        {
            return ranqueamento.Where(i => i.Jogador.Posicao == posicao1 || i.Jogador.Posicao == posicao2)
                               .OrderByDescending(i => i.Pontos);
        }

        public static IEnumerable<Clube> AgruparPorClube(this IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            var clubes = ranqueamento.GroupBy(i => i.Jogador.Clube)
                                     .Select(group => new
                                     {
                                         Clube = group.Key,
                                         Pontuacao = group.Average(i => i.Pontos)
                                     })
                                     .OrderByDescending(i => i.Pontuacao)
                                     .ToList()
                                     .Select(i => i.Clube)
                                     .ToList();
            return clubes;
        }
    }
}
