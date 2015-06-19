using System;
using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Crawler
{
    public interface ISiteCrawler
    {
        bool SuportaClubes { get; }
        bool SuportaJogadores { get; }

        IEnumerable<Clube> CarregarClubes();
        IEnumerable<Jogador> CarregarJogadores();
        //IEnumerable<Jogador> CarregarJogadores();
    }

    class ScoutsCartolaSiteCrawler : ISiteCrawler
    {
        private readonly Uri _uriBase;

        public bool SuportaClubes { get { return true; } }
        public bool SuportaJogadores { get { return true; } }


        public ScoutsCartolaSiteCrawler(Uri uriBase)
        {
            _uriBase = uriBase;
        }


        public IEnumerable<Clube> CarregarClubes()
        {
            yield break;
        }

        public IEnumerable<Jogador> CarregarJogadores()
        {
            yield break;
        }
    }
}