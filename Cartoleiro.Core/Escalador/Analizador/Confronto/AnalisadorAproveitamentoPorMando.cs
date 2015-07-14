using System.Collections.Generic;

namespace Cartoleiro.Core.Escalador.Analizador.Confronto
{
    public class AnalisadorAproveitamentoPorMando : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item =>
            {
                if (Cartola.Campeonato.Rodadas.ProximaRodada == null)
                    return 0;


                var esMandante = Cartola.Campeonato.Rodadas.ProximaRodada.EsMandante(item.Jogador.Clube);

                return (esMandante)
                    ? item.Jogador.Clube.Campeonato.AproveitamentoEmCasa
                    : item.Jogador.Clube.Campeonato.AproveitamentoForaDeCasa;
            });
        }
    }
}
