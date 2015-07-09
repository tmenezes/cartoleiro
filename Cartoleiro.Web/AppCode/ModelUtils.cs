using System.Text;

namespace Cartoleiro.Web.AppCode
{
    public static class ModelUtils
    {
        public static string RemoverAcentos(string texto)
        {
            var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            return Encoding.UTF8.GetString(tempBytes);
        }
    }
}
