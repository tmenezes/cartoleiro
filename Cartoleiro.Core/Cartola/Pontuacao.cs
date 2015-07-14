namespace Cartoleiro.Core.Cartola
{
    public class Pontuacao
    {
        public double Media { get; set; }
        public double Ultima { get; set; }

        public Pontuacao(double media, double ultima)
        {
            Media = media;
            Ultima = ultima;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Media, Ultima);
        }
    }
}