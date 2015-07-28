using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Data
{
    public interface ICartolaDataSource
    {
        IEnumerable<Clube> Clubes { get; }
        IEnumerable<Jogador> Jogadores { get; }
        IEnumerable<Rodada> Rodadas { get; }
        IEnumerable<Jogo> HistoricoDeJogos { get; }
    }
}