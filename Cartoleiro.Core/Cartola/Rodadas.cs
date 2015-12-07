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
            get { return this.FirstOrDefault(r => r.Numero == (RodadaAtual.Numero + 1)) ?? RodadasAtivas().Last(); }
        }


        public Rodadas(IEnumerable<Rodada> rodadas)
            : base(rodadas)
        {
        }


        public IEnumerable<Jogo> JogosPassados(Clube clube)
        {
            return RodadasAtivas().Select(r => r.Jogos.First(j => j.ParticipaDesseJogo(clube)));
        }

        public IEnumerable<Jogo> JogosComoMandante(Clube clube)
        {
            return RodadasAtivas().Select(r => r.Jogos.FirstOrDefault(j => j.Mandante == clube)).Where(j => j != null);
        }

        public IEnumerable<Jogo> JogosComoVisitante(Clube clube)
        {
            return RodadasAtivas().Select(r => r.Jogos.FirstOrDefault(j => j.Visitante == clube)).Where(j => j != null);
        }

        public IEnumerable<Jogo> Ultimos5Jogos(Clube clube)
        {
            return RodadasAtivas().OrderByDescending(r => r.Numero)
                                  .Take(5)
                                  .Select(r => r.Jogos.FirstOrDefault(j => j.ParticipaDesseJogo(clube)))
                                  .OrderBy(j => j.NumeroDaRodada)
                                  .ToList();
        }


        private IEnumerable<Rodada> RodadasAtivas()
        {
            return this.Where(r => r.Numero <= RodadaAtual.Numero);
        }
    }
}