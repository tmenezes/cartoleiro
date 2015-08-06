using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Web.AppCode;

namespace Cartoleiro.Web.Models.SharedModels
{
    public class ClubesComMediaDeSetorDoCampo
    {
        public IEnumerable<Clube> Clubes { get; set; }
        public SetorDoCampo SetorDoCampo { get; set; }

        public ClubesComMediaDeSetorDoCampo(IEnumerable<Clube> clubes, SetorDoCampo setorDoCampo)
        {
            Clubes = clubes;
            SetorDoCampo = setorDoCampo;
        }
    }
}
