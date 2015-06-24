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

        public override string ToString()
        {
            return string.Format("{0} {1} x {2} {3}", Mandante.Nome, PlacarMandante, PlacarVisitante, Visitante.Nome);
        }
    }
}