using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Escalador
{
    public class PontuacaoDeEscalacao
    {
        public Jogador Jogador { get; private set; }
        public double Pontos { get; private set; }

        public PontuacaoDeEscalacao(Jogador jogador)
        {
            Jogador = jogador;
            Pontos = 0;
        }

        public void AddPontos(double pontos)
        {
            Pontos += pontos;
        }


        public override string ToString()
        {
            return string.Format("Jogador: {0}, Pontos: {1}", Jogador, Pontos);
        }
    }
}