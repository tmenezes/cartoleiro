using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Cartola
{
    public class Rodada
    {
        public int Numero { get; set; }
        public IList<Jogo> Jogos { get; set; }

        public int TotalDeGols
        {
            get { return Jogos.Sum(j => j.TotalDeGols); }
        }


        public Rodada(int numero)
        {
            Numero = numero;
            Jogos = new List<Jogo>();
        }


        public bool EsMandante(Clube clube)
        {
            return Jogos.Any(j => j.Mandante == clube);
        }

        public Jogo GetJogoDoClube(Clube clube)
        {
            return Jogos.First(j => j.ParticipaDesseJogo(clube));
        }

        public override string ToString()
        {
            return string.Format("Numero: {0}, Jogos: {1}", Numero, Jogos.Count);
        }
    }
}
