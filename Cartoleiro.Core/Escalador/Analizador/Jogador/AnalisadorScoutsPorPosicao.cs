using System;
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
                switch (item.Jogador.Posicao)
                {
                    case Posicao.Goleiro:
                        return item.Jogador.Scouts.FinalizacoesDefendidas;

                    case Posicao.Lateral:
                        return item.Jogador.Scouts.TotalDePositivos;

                    case Posicao.Zagueiro:
                        return item.Jogador.Scouts.RoubadasDeBola;

                    case Posicao.MeioCampo:
                        return item.Jogador.Scouts.Assistencias;

                    case Posicao.Atacante:
                        return item.Jogador.Scouts.Gols;

                    case Posicao.Tecnico:
                    default:
                        return item.Jogador.Pontuacao.Media;
                }
            });
        }
    }
}