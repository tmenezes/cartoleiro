using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;

namespace Cartoleiro.Crawler
{
    public class CrawlerDataSource : ICartolaDataSource
    {
        public IEnumerable<Clube> Clubes { get; internal set; }
        public IEnumerable<Jogador> Jogadores { get; internal set; }
        public IEnumerable<Rodada> Rodadas { get; internal set; }
        public IEnumerable<Jogo> HistoricoDeJogos { get; internal set; }

        public CrawlerDataSource()
        {
            Clubes = new List<Clube>();
            Jogadores = new List<Jogador>();
            Rodadas = new List<Rodada>();
            HistoricoDeJogos = new List<Jogo>();
        }
    }
}