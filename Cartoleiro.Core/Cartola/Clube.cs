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
    }
}