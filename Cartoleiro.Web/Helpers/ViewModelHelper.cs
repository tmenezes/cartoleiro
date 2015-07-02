using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.Helpers
{
    public static class ViewModelHelper
    {
        public static IEnumerable<SelectListItem> EsquemasTaticos
        {
            get
            {
                return new List<SelectListItem>()
                       {
                            new SelectListItem { Value = ((int)EsquemaTatico._343).ToString(), Text = "3-4-3" },
                            new SelectListItem { Value = ((int)EsquemaTatico._352).ToString(), Text = "3-5-2" },
                            new SelectListItem { Value = ((int)EsquemaTatico._433).ToString(), Text = "4-3-3" },
                            new SelectListItem { Value = ((int)EsquemaTatico._442).ToString(), Text = "4-4-2" },
                            new SelectListItem { Value = ((int)EsquemaTatico._451).ToString(), Text = "4-5-1" },
                            new SelectListItem { Value = ((int)EsquemaTatico._532).ToString(), Text = "5-3-2" },
                            new SelectListItem { Value = ((int)EsquemaTatico._541).ToString(), Text = "5-4-1" },
                       };
            }
        }

        public static IEnumerable<SelectListItem> Posicoes
        {
            get
            {
                var posicoes = Enum.GetValues(typeof(Posicao))
                                   .OfType<Posicao>()
                                   .Select(i => new SelectListItem()
                                                {
                                                    Value = i.ToString(),
                                                    Text = i.ToString()
                                                });
                return posicoes;
            }
        }
    }
}
