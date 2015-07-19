using System.Web;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.AppCode.Extensions
{
    public static class ClubeExtensions
    {
        public static string GetUrlImagem(this Clube clube)
        {
            var imagem = string.Format("~/Images/clubes/{0}.png", GetNomeNormalizado(clube));

            return UrlHelper.GenerateContentUrl(imagem, HttpContext.Current.Request.RequestContext.HttpContext);
        }

        public static string GetUrlImagemGrande(this Clube clube)
        {
            var imagem = string.Format("~/Images/clubes/{0}_120px.png", GetNomeNormalizado(clube));

            return UrlHelper.GenerateContentUrl(imagem, HttpContext.Current.Request.RequestContext.HttpContext);
        }

        public static string GetTooltip(this Clube clube)
        {
            return string.Format("{0}: {1}º lugar", clube.Nome, clube.Campeonato.Posicao);
        }

        public static string GetNomeNormalizado(this Clube clube)
        {
            var clubeSemAcento = ModelUtils.RemoverAcentos(clube.Nome.ToLower().Replace(" ", ""));

            return clubeSemAcento;
        }

        public static Clube AdversarioNaProximaRodada(this Clube clube)
        {
            var adversario = Campeonato.Rodadas.ProximaRodada.GetJogoDoClube(clube).GetAdversario(clube);
            return adversario;
        }
    }
}
