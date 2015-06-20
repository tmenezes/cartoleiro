using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorBuilder
    {
        private readonly IList<IAnalisador> _analisadores;
        public IList<IAnalisador> Analisadores
        {
            get { return _analisadores; }
        }

        public AnalisadorBuilder()
        {
            _analisadores = new List<IAnalisador>();
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
