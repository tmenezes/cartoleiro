using System.Collections.Generic;
using System.Linq;

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
                if (esMandante)
                {
                    var totalJogosEmCasa = (double)Cartola.Campeonato.Rodadas.JogosComoMandante(item.Jogador.Clube).Count();
                    var vitoriasEmCasa = item.Jogador.Clube.Campeonato.VitoriasEmCasa;

                    return vitoriasEmCasa / totalJogosEmCasa * 100;
                }
                else
                {
                    var totalJogosForaDeCasa = (double)Cartola.Campeonato.Rodadas.JogosComoVisitante(item.Jogador.Clube).Count();
                    var vitoriasForaDeCasa = item.Jogador.Clube.Campeonato.VitoriasForaDeCasa;

                    return vitoriasForaDeCasa / totalJogosForaDeCasa * 100;
                }
            });
        }
    }
}
