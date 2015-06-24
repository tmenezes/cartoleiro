
using Cartoleiro.Core.Escalador.Analizador.Campeonato;
using Cartoleiro.Core.Escalador.Analizador.Confronto;
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


        // jogador
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

        public AnalisadorBuilder ScoutsPositivos()
        {
            Analisadores.Add(new AnalisadorScoutsPositivos());
            return this;
        }

        public AnalisadorBuilder ScoutsNegativos()
        {
            Analisadores.Add(new AnalisadorScoutsNegativos());
            return this;
        }


        // campeonato
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


        // confronto
        public AnalisadorBuilder MandoDeCampo()
        {
            Analisadores.Add(new AnalisadorMandoDeCampo());
            return this;
        }

        public AnalisadorBuilder VitoriasEmCasa()
        {
            Analisadores.Add(new AnalisadorVitoriasEmCasa());
            return this;
        }

        public AnalisadorBuilder VitoriasForaDeCasa()
        {
            Analisadores.Add(new AnalisadorVitoriasForaDeCasa());
            return this;
        }
    }
}
