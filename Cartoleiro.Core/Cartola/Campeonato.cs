
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
        public double Aproveitamento { get { return Pontos / ((double)Jogos * 3) * 100; } }
        public UltimosJogos UltimosJogos { get; set; }

        public int GolsProEmCasa
        {
            get { return Rodadas.JogosComoMandante(_clube).Sum(j => j.PlacarMandante); }
        }
        public int GolsProForaDeCasa
        {
            get { return Rodadas.JogosComoVisitante(_clube).Sum(j => j.PlacarVisitante); }
        }

        public int VitoriasEmCasa
        {
            get
            {
                if (_vitoriasEmCasa == null)
                {
                    _vitoriasEmCasa = Rodadas.JogosComoMandante(_clube).Count(j => j.Vencedor() == _clube);
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
                    _vitoriasForaDeCasa = Rodadas.JogosComoVisitante(_clube).Count(j => j.Vencedor() == _clube);
                }

                return _vitoriasForaDeCasa.Value;
            }
        }

        public int PontosEmCasa
        {
            get { return Rodadas.JogosComoMandante(_clube).Sum(j => j.PontosConquistados(_clube)); }
        }
        public int PontosForaDeCasa
        {
            get { return Rodadas.JogosComoVisitante(_clube).Sum(j => j.PontosConquistados(_clube)); }
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
            get { return PontosEmCasa / ((double)JogosEmCasa * 3) * 100; }
        }
        public double AproveitamentoForaDeCasa
        {
            get { return PontosForaDeCasa / ((double)JogosForaDeCasa * 3) * 100; }
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
