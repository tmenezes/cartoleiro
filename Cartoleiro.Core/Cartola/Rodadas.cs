using System.Collections.Generic;
using System.Linq;

namespace Cartoleiro.Core.Cartola
{
    public class Rodadas : List<Rodada>
    {
        Rodada _rodadaAtual = null;
        public Rodada RodadaAtual
        {
            get
            {
                if (_rodadaAtual == null)
                {
                    _rodadaAtual = this.LastOrDefault(r => r.TotalDeGols > 0);
                }

                return _rodadaAtual ?? this.First();
            }
        }

        public Rodada ProximaRodada
        {
            get { return this.FirstOrDefault(r => r.Numero == (RodadaAtual.Numero + 1)); }
        }


        public Rodadas(IEnumerable<Rodada> rodadas)
            : base(rodadas)
        {
        }


        public IEnumerable<Jogo> JogosComoMandante(Clube clube)
        {
            return RodadasAtivas().Select(r => r.Jogos.FirstOrDefault(j => j.Mandante == clube)).Where(j => j != null);
        }

        public IEnumerable<Jogo> JogosComoVisitante(Clube clube)
        {
            return RodadasAtivas().Select(r => r.Jogos.FirstOrDefault(j => j.Visitante == clube)).Where(j => j != null);
        }


        private IEnumerable<Rodada> RodadasAtivas()
        {
            return this.Where(r => r.Numero <= RodadaAtual.Numero);
        }
    }
}