using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;

namespace Cartoleiro.Crawler
{
    public class CrawlerDataSource : ICartolaDataSource
    {
        public IEnumerable<Clube> Clubes { get; private set; }
        public IEnumerable<Jogador> Jogadores { get; private set; }

        public CrawlerDataSource(IEnumerable<Clube> clubes, IEnumerable<Jogador> jogadores)
        {
            Clubes = clubes;
            Jogadores = jogadores;
        }
    }
}