using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Data;

namespace Cartoleiro.Core.Cartola
{
    public static class HistoricoDeJogos
    {
        private static readonly Dictionary<Clube, IEnumerable<Jogo>> _jogosDoClube = new Dictionary<Clube, IEnumerable<Jogo>>();

        public static void Carregar(ICartolaDataSource cartolaDataSource)
        {
            if (_jogosDoClube.Any())
                return;

            foreach (var clube in cartolaDataSource.Clubes)
            {
                var copiaClube = clube;

                var jogos = cartolaDataSource.HistoricoDeJogos.Where(j => j.ParticipaDesseJogo(copiaClube)).ToList();

                _jogosDoClube.Add(clube, jogos);
            }
        }

        public static IEnumerable<Jogo> GetHistoricoDeJogos(Clube clube)
        {
            return _jogosDoClube.ContainsKey(clube)
                ? _jogosDoClube[clube]
                : null;
        }

        public static IEnumerable<Jogo> GetHistoricoDeConfrontos(Clube clubeA, Clube clubeB)
        {
            var jogosDoClubeA = GetHistoricoDeJogos(clubeA);

            return jogosDoClubeA.Where(j => j.ParticipaDesseJogo(clubeB));
        }
    }
}