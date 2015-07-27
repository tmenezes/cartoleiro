using System;
using System.Linq;
using System.Text;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Web.AppCode.Extensions;

namespace Cartoleiro.Web.AppCode
{
    public static class ModelUtils
    {
        [Obsolete("Usar StringUtils do Core")]
        public static string RemoverAcentos(string texto)
        {
            var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            return Encoding.UTF8.GetString(tempBytes);
        }

        public static Clube GetMandande(string idJogo)
        {
            var nomeClube = idJogo.Split('_')[0];

            var clube = CartoleiroApp.CartolaDataSource.Clubes.FirstOrDefault(c => c.GetNomeNormalizado() == nomeClube);

            return clube;
        }

        public static Clube GetVisitante(string idJogo)
        {
            var nomeClube = idJogo.Split('_')[1];

            var clube = CartoleiroApp.CartolaDataSource.Clubes.FirstOrDefault(c => c.GetNomeNormalizado() == nomeClube);

            return clube;
        }
    }
}
