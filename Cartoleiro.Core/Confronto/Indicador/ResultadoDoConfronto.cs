using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Confronto.Indicador
{
    public class ResultadoDoConfronto
    {
        // atributos
        private int? _totalMandante;
        private int? _totalVisitante;
        private readonly IList<ItemDeMedicaoDeConfronto> _itensDeMedicao;

        // propriedades
        public Clube Mandande { get; set; }
        public Clube Visitante { get; set; }

        public int TotalMandante
        {
            get
            {
                if (_totalMandante == null)
                    _totalMandante = ItensDeMedicao.Count(i => i.Vencedor == Mandande);

                return _totalMandante.Value;
            }
        }
        public int TotalVisitante
        {
            get
            {
                if (_totalVisitante == null)
                    _totalVisitante = ItensDeMedicao.Count(i => i.Vencedor == Visitante);

                return _totalVisitante.Value;
            }
        }

        public IEnumerable<ItemDeMedicaoDeConfronto> ItensDeMedicao
        {
            get { return _itensDeMedicao; }
        }


        // construtor
        public ResultadoDoConfronto(Clube mandande, Clube visitante)
        {
            Mandande = mandande;
            Visitante = visitante;

            _itensDeMedicao = new List<ItemDeMedicaoDeConfronto>();
        }

        // publico
        public ResultadoDoConfronto AdicionarItemDeMedicao(ItemDeMedicaoDeConfronto item)
        {
            _itensDeMedicao.Add(item);

            return this;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} vs {2} {3}", Mandande.Nome, TotalMandante, TotalVisitante, Visitante.Nome);
        }
    }
}