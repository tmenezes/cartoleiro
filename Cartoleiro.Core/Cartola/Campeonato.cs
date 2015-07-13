
using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Cartola
{
    public class Campeonato
    {
        private Clube _clube;
        private int? _vitoriasEmCasa = null;
        private int? _vitoriasForaDeCasa = null;

        public int Posicao { get; set; }
        public int Pontos { get; set; }
        public int Jogos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolsPro { get; set; }
        public int GolsContra { get; set; }
        public int SaldoDeGol { get; set; }
        public double Aproveitamento { get { return Vitorias / (double)Jogos * 100; } }
        public UltimosJogos UltimosJogos { get; set; }

        public int VitoriasEmCasa
        {
            get
            {
                if (_vitoriasEmCasa == null)
                {
                    _vitoriasEmCasa = Rodadas.JogosComoMandante(_clube).Count(j => j.Vencedor == _clube);
                }

                return _vitoriasEmCasa.Value;
            }
        }
        public int VitoriasForaDeCasa
        {
            get
            {
                if (_vitoriasForaDeCasa == null)
                {
                    _vitoriasForaDeCasa = Rodadas.JogosComoVisitante(_clube).Count(j => j.Vencedor == _clube);
                }

                return _vitoriasForaDeCasa.Value;
            }
        }

        public int JogosEmCasa
        {
            get { return Rodadas.JogosComoMandante(_clube).Count(); }
        }
        public int JogosForaDeCasa
        {
            get { return Rodadas.JogosComoVisitante(_clube).Count(); }
        }

        public double AproveitamentoEmCasa
        {
            get { return VitoriasEmCasa / (double)JogosEmCasa * 100; }
        }
        public double AproveitamentoForaDeCasa
        {
            get { return VitoriasForaDeCasa / (double)JogosForaDeCasa * 100; }
        }

        public static Rodadas Rodadas { get; set; }


        static Campeonato()
        {
            Rodadas = new Rodadas(new List<Rodada>());
        }


        public void SetClube(Clube clube)
        {
            _clube = clube;
        }

        public override string ToString()
        {
            return string.Format("{0}º lugar, Pontos: {1}, Vitorias: {2}", Posicao, Pontos, Vitorias);
        }
    }
}
