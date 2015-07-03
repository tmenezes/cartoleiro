using System.Text;
using System.Web;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.Helpers
{
    public static class ClubeExtensions
    {
        public static string GetUrlImagem(this Clube clube)
        {
            var clubeSemAcento = RemoverAcentos(clube.Nome.ToLower().Replace(" ", ""));
            var imagem = string.Format("~/Images/clubes/{0}.png", clubeSemAcento);

            return UrlHelper.GenerateContentUrl(imagem, HttpContext.Current.Request.RequestContext.HttpContext);
        }

        public static string GetTooltip(this Clube clube)
        {
            return string.Format("{0}: {1}º lugar", clube.Nome, clube.Campeonato.Posicao);
        }



        private static string RemoverAcentos(string texto)
        {
            var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(texto);
            return Encoding.UTF8.GetString(tempBytes);
        }
    }
}
