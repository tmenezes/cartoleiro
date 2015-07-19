using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Extensions;

namespace Cartoleiro.Core.Cartola
{
    public class Clube
    {
        public string Nome { get; set; }
        public Campeonato Campeonato { get; set; }

        public Clube(string nome)
        {
            Nome = nome;
        }

        public IEnumerable<Jogador> Elenco(ICartolaDataSource cartolaDataSource)
        {
            return cartolaDataSource.Jogadores.ElencoDoClube(this);
        }
        public IEnumerable<Jogador> JogadoresTitulares(ICartolaDataSource cartolaDataSource)
        {
            return cartolaDataSource.Jogadores.JogadoresTitularesDoClube(this);
        }

        public override string ToString()
        {
            if (Campeonato == null)
                return Nome;

            return string.Format("{0} - {1}", Nome, Campeonato);
        }
    }
}