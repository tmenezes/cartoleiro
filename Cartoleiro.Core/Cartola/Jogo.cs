using System;

namespace Cartoleiro.Core.Cartola
{
    public class Jogo
    {
        public int NumeroDaRodada { get; set; }
        public Clube Mandante { get; set; }
        public Clube Visitante { get; set; }
        public int PlacarMandante { get; set; }
        public int PlacarVisitante { get; set; }

        public Clube Vencedor
        {
            get
            {
                return (PlacarMandante > PlacarVisitante)
                    ? Mandante
                    : (PlacarMandante < PlacarVisitante) ? Visitante : null;
            }
        }
        public bool Empate
        {
            get { return Vencedor == null; }
        }
        public int TotalDeGols
        {
            get { return PlacarMandante + PlacarVisitante; }
        }


        public Jogo(int numeroDaRodada, Clube mandante, Clube visitante)
        {
            NumeroDaRodada = numeroDaRodada;
            Mandante = mandante;
            Visitante = visitante;
        }


        public bool ParticipaDesseJogo(Clube clube)
        {
            return clube == Mandante || clube == Visitante;
        }

        public Clube GetAdversario(Clube clube)
        {
            if (!ParticipaDesseJogo(clube))
                throw new InvalidOperationException("Clube não participa desse jogo");

            return (clube == Mandante)
                ? Visitante
                : Mandante;
        }

        public int GolsDoClube(Clube clube)
        {
            if (!ParticipaDesseJogo(clube))
                throw new InvalidOperationException("Clube não participa desse jogo");

            return (clube == Mandante)
                ? PlacarMandante
                : PlacarVisitante;
        }

        public int PontosConquistados(Clube clube)
        {
            if (!ParticipaDesseJogo(clube))
                throw new InvalidOperationException("Clube não participa desse jogo");

            return (clube == Vencedor)
                ? 3
                : (Empate) ? 1 : 0;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} x {2} {3}", Mandante.Nome, PlacarMandante, PlacarVisitante, Visitante.Nome);
        }
    }
}