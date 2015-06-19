using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cartoleiro.Core.Escalador;

namespace Cartoleiro.Web.Models
{
    public static class ViewDataExtensions
    {
        public static Time GetTimeEscalado(this ViewDataDictionary viewData)
        {
            if (viewData.ContainsKey("TIME_ESCALADO"))
                return viewData["TIME_ESCALADO"] as Time;

            return null;
        }
        public static void SetTimeEscalado(this ViewDataDictionary viewData, Time time)
        {
            if (!viewData.ContainsKey("TIME_ESCALADO"))
                viewData.Add("TIME_ESCALADO", time);
            else
                viewData["TIME_ESCALADO"] = time;
        }
    }
}
