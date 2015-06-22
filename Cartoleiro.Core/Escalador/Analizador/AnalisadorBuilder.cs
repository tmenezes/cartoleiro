
using Cartoleiro.Core.Escalador.Analizador.Campeonato;
using Cartoleiro.Core.Escalador.Analizador.Jogador;

namespace Cartoleiro.Core.Escalador.Analizador
{
    public class AnalisadorBuilder
    {
        private readonly Analisadores _analisadores;
        public Analisadores Analisadores
        {
            get { return _analisadores; }
        }

        public AnalisadorBuilder()
        {
            _analisadores = new Analisadores();
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

        public AnalisadorBuilder PontosNoCampeonato()
        {
            Analisadores.Add(new AnalisadorPontuacaoNoCampeonato());
            return this;
        }

        public AnalisadorBuilder Vitorias()
        {
            Analisadores.Add(new AnalisadorVitorias());
            return this;
        }

        public AnalisadorBuilder GolsPro()
        {
            Analisadores.Add(new AnalisadorGolsPro());
            return this;
        }

        public AnalisadorBuilder GolsContra()
        {
            Analisadores.Add(new AnalisadorGolsContra());
            return this;
        }

        public AnalisadorBuilder SaldoDeGols()
        {
            Analisadores.Add(new AnalisadorSaldoDeGols());
            return this;
        }

        public AnalisadorBuilder Ultimos5Jogos()
        {
            Analisadores.Add(new AnalisadorUltimos5Jogos());
            return this;
        }
    }
}
