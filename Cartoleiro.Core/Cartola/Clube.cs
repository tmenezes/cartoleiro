namespace Cartoleiro.Core.Cartola
{
    public class Clube
    {
        public string Nome { get; set; }
        public Campeonato Campeonato { get; set; }

        public Clube(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Nome, Campeonato);
        }
    }
}