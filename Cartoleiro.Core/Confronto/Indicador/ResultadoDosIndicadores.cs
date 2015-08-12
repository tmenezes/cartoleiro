using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Confronto.Indicador
{
    public class ResultadoDosIndicadores
    {
        // atributos
        private int? _totalMandante;
        private int? _totalVisitante;
        private readonly IList<Indicador> _indicadores;

        // propriedades
        public Clube Mandande { get; set; }
        public Clube Visitante { get; set; }

        public int TotalMandante
        {
            get
            {
                if (_totalMandante == null)
                    _totalMandante = Indicadores.Count(i => i.Vencedor == Mandande);

                return _totalMandante.Value;
            }
        }
        public int TotalVisitante
        {
            get
            {
                if (_totalVisitante == null)
                    _totalVisitante = Indicadores.Count(i => i.Vencedor == Visitante);

                return _totalVisitante.Value;
            }
        }

        public IEnumerable<Indicador> Indicadores
        {
            get { return _indicadores; }
        }


        // construtor
        public ResultadoDosIndicadores(Clube mandande, Clube visitante)
        {
            Mandande = mandande;
            Visitante = visitante;

            _indicadores = new List<Indicador>();
        }

        // publico
        public ResultadoDosIndicadores AdicionarIndicador(Indicador indicador)
        {
            _indicadores.Add(indicador);

            return this;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} vs {2} {3}", Mandande.Nome, TotalMandante, TotalVisitante, Visitante.Nome);
        }
    }
}