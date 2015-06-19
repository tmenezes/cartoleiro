using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Crawler
{
    internal static class CrawlerRuntimeHelper
    {
        private static readonly Dictionary<string, Clube> _clubes = new Dictionary<string, Clube>();

        internal static Clube GetClube(string nome)
        {
            if (!_clubes.ContainsKey(nome))
                _clubes.Add(nome, new Clube(nome));

            return _clubes[nome];
        }
    }
}