namespace Cartoleiro.Core.Cartola
{
    public class UltimosJogos
    {
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }

        public int TotalDePontos { get { return (Vitorias * 3) + Empates; } }

        public UltimosJogos(int vitorias, int empates, int derrotas)
        {
            Vitorias = vitorias;
            Empates = empates;
            Derrotas = derrotas;
        }
    }
}