
namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorBuilder
    {
        private readonly Analizadores _analisadores;
        public Analizadores Analisadores
        {
            get { return _analisadores; }
        }

        public AnalisadorBuilder()
        {
            _analisadores = new Analizadores();
        }


        public AnalisadorBuilder PontuacaoMedia()
        {
            Analisadores.Add(new AnalisadorMediaPontuacao());
            return this;
        }

        public AnalisadorBuilder UltimaPontuacao()
        {
            Analisadores.Add(new AnalisadorUltimaPontuacao());
            return this;
        }
    }
}
