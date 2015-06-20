
namespace Cartoleiro.Core.Cartola
{
    public class Campeonato
    {
        public int Posicao { get; set; }
        public int Pontos { get; set; }
        public int Jogos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolsPro { get; set; }
        public int GolsContra { get; set; }
        public int SaldoDeGol { get; set; }
        public double Aproveitamento { get { return Vitorias / Jogos * 100; } }
        public UltimosJogos UltimosJogos { get; set; }

        public override string ToString()
        {
            return string.Format("Posicao: {0}, Pontos: {1}, Vitorias: {2}", Posicao, Pontos, Vitorias);
        }
    }
}
