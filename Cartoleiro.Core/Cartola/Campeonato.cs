﻿
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
        public double Aproveitamento { get { return Vitorias / Jogos * 100; } }
        public UltimosJogos UltimosJogos { get; set; }

        public int VitoriasEmCasa
        {
            get
            {
                if (_vitoriasEmCasa == null)
                {
                    _vitoriasEmCasa = Rodadas.Count(r => r.Jogos.First(j => j.Mandante == _clube).Vencedor == _clube);
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
                    _vitoriasForaDeCasa = Rodadas.Count(r => r.Jogos.First(j => j.Visitante == _clube).Vencedor == _clube);
                }

                return _vitoriasForaDeCasa.Value;
            }
        }

        public static IEnumerable<Rodada> Rodadas { get; set; }


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
