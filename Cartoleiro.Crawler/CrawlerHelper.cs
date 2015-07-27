using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace Cartoleiro.Crawler
{
    internal class CrawlerHelper
    {
        private static readonly Dictionary<string, Clube> _clubes = new Dictionary<string, Clube>();
        internal static Clube GetClube(string nome)
        {
            if (!_clubes.ContainsKey(nome))
                _clubes.Add(nome, new Clube(nome));

            return _clubes[nome];
        }

		internal static readonly Dictionary<string, Clube> _clubesComTrocaDeNome = new Dictionary<string, Clube>();
		internal static void AdicionarClubeComTrocaDeNome(Clube clube, string nomeAntigo)
		{
			if (!_clubesComTrocaDeNome.ContainsKey(nomeAntigo))
				_clubesComTrocaDeNome.Add(nomeAntigo, clube);
		}
		internal static string GetNomeDoClube(Clube clube)
		{
			return (_clubesComTrocaDeNome.ContainsKey (clube.Nome)) 
				? _clubesComTrocaDeNome[clube.Nome].Nome
				: clube.Nome;
		}

        internal static RemoteWebDriver GetWebDriver()
        {
            var driver = new PhantomJSDriver();
            return driver;
        }
    }
}
