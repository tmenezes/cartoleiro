using System;
using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Crawler
{
    public interface ISiteCrawler
    {
        event EventHandler<CrawlingInfo> ObjetoCarregado;

        bool SuportaClubes { get; }
        bool SuportaJogadores { get; }
        bool SuportaRodadas { get; }

        IEnumerable<Clube> CarregarClubes();
        IEnumerable<Jogador> CarregarJogadores();
        IEnumerable<Rodada> CarregarRodadas();
    }
}