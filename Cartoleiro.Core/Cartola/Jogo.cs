namespace Cartoleiro.Core.Cartola
{
    public class Jogo
    {
        public Clube Mandante { get; set; }
        public Clube Visitante { get; set; }
        public int PlacarMandante { get; set; }
        public int PlacarVisitante { get; set; }

        public Jogo(Clube mandante, Clube visitante)
        {
            Mandante = mandante;
            Visitante = visitante;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} x {2} {3}", Mandante, PlacarMandante, PlacarVisitante, Visitante);
        }
    }
}