using System.Web;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.AppCode.Extensions
{
    public static class JogadorExtensions
    {
        public static string GetUrlImagem(this Jogador jogador, string formato = "80px")
        {
            var imagem = string.Format("~/Images/jogadores/{0}_{1}.jpeg", jogador.Id, formato);

            return UrlHelper.GenerateContentUrl(imagem, HttpContext.Current.Request.RequestContext.HttpContext);
        }
    }
}