using System.Collections.Generic;

namespace Cartoleiro.Core.Cartola
{
    public class Rodada
    {
        public int Numero { get; set; }
        public IList<Jogo> Jogos { get; set; }

        public Rodada(int numero)
        {
            Numero = numero;
            Jogos = new List<Jogo>();
        }


        public override string ToString()
        {
            return string.Format("Numero: {0}, Jogos: {1}", Numero, Jogos.Count);
        }
    }
}
