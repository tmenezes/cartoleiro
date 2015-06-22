using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Jogador
{
    public class AnalisadorMediaPontuacao : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item => item.Jogador.Pontuacao.Media);
        }
    }
}