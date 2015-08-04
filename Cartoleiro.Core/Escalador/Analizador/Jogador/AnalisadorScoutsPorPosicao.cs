using System.Collections.Generic;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Escalador.Analizador.Jogador
{
    public class AnalisadorScoutsPorPosicao : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item =>
            {
                var scouts = item.Jogador.Scouts;

                switch (item.Jogador.Posicao)
                {
                    case Posicao.Goleiro:
                        return scouts.DefesasDificeis + scouts.SemGolSofrido;

                    case Posicao.Lateral:
                    case Posicao.Zagueiro:
                        return scouts.RoubadasDeBola + scouts.SemGolSofrido;

                    case Posicao.MeioCampo:
                        return scouts.Assistencias + scouts.RoubadasDeBola;

                    case Posicao.Atacante:
                        return scouts.Gols + scouts.Assistencias;

                    case Posicao.Tecnico:
                    default:
                        return item.Jogador.Pontuacao.Media;
                }
            });
        }
    }
}