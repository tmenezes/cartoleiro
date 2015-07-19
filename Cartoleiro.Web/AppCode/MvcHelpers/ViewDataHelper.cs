using System.Collections.Generic;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Escalador;

namespace Cartoleiro.Web.AppCode.MvcHelpers
{
    public static class ViewDataExtensions
    {
        // qualquer controller
        public static string GetErro(this ViewDataDictionary viewData)
        {
            if (viewData.ContainsKey("CONTROLLER_ERROR"))
                return viewData["CONTROLLER_ERROR"] as string;

            return null;
        }
        public static void SetErro(this ViewDataDictionary viewData, string erro)
        {
            if (!viewData.ContainsKey("CONTROLLER_ERROR"))
                viewData.Add("CONTROLLER_ERROR", erro);
            else
                viewData["CONTROLLER_ERROR"] = erro;
        }

        // escalador
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

        // clube
        public static string GetDescricaoDoClube(this ViewDataDictionary viewData)
        {
            if (viewData.ContainsKey("CLUBE_DESCRICAO"))
                return viewData["CLUBE_DESCRICAO"] as string;

            return null;
        }
        public static void SetDescricaoDoClube(this ViewDataDictionary viewData, string descricaoClube)
        {
            if (!viewData.ContainsKey("CLUBE_DESCRICAO"))
                viewData.Add("CLUBE_DESCRICAO", descricaoClube);
            else
                viewData["CLUBE_DESCRICAO"] = descricaoClube;
        }

        public static IEnumerable<Jogador> GetTitularesDoClube(this ViewDataDictionary viewData)
        {
            if (viewData.ContainsKey("CLUBE_TITULARES"))
                return viewData["CLUBE_TITULARES"] as IEnumerable<Jogador>;

            return null;
        }
        public static void SetTitularesDoClube(this ViewDataDictionary viewData, IEnumerable<Jogador> titulares)
        {
            if (!viewData.ContainsKey("CLUBE_TITULARES"))
                viewData.Add("CLUBE_TITULARES", titulares);
            else
                viewData["CLUBE_TITULARES"] = titulares;
        }

        public static IEnumerable<Jogador> GetElencoDoClube(this ViewDataDictionary viewData)
        {
            if (viewData.ContainsKey("CLUBE_ELENCO"))
                return viewData["CLUBE_ELENCO"] as IEnumerable<Jogador>;

            return null;
        }
        public static void SetElencoDoClube(this ViewDataDictionary viewData, IEnumerable<Jogador> elenco)
        {
            if (!viewData.ContainsKey("CLUBE_ELENCO"))
                viewData.Add("CLUBE_ELENCO", elenco);
            else
                viewData["CLUBE_ELENCO"] = elenco;
        }
    }
}
