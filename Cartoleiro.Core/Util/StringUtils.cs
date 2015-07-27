using System.Text;

namespace Cartoleiro.Core.Util
{
    public static class StringUtils
    {
        public static string RemoverAcentos(string texto)
        {
            var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            return Encoding.UTF8.GetString(tempBytes, 0, tempBytes.Length);
        }
    }
}
