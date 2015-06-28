using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Escalador.Analizador.Confronto
{
    public class AnalisadorGolsProMando : AnalisadorGenerico, IAnalisador
    {
        public void Analisar(IEnumerable<PontuacaoDeEscalacao> ranqueamento)
        {
            Analisar(ranqueamento, item =>
            {
                if (Cartola.Campeonato.Rodadas.ProximaRodada == null)
                    return 0;


                var esMandante = Cartola.Campeonato.Rodadas.ProximaRodada.EsMandante(item.Jogador.Clube);

                var jogosPassados = (esMandante)
                    ? Cartola.Campeonato.Rodadas.JogosComoMandante(item.Jogador.Clube)
                    : Cartola.Campeonato.Rodadas.JogosComoVisitante(item.Jogador.Clube);

                return jogosPassados.Sum(j => j.GolsDoClube(item.Jogador.Clube));
            });
        }
    }
}