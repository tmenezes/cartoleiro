using System.Text;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.Helpers
{
    public static class ClubeExtensions
    {
        public static string GetUrlImagem(this Clube clube)
        {
            var clubeSemAcento = RemoverAcentos(clube.Nome.ToLower().Replace(" ", ""));
            return string.Format("Images/clubes/{0}.png", clubeSemAcento);
        }

        private static string RemoverAcentos(string texto)
        {
            var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            return Encoding.UTF8.GetString(tempBytes);
        }
    }
}
