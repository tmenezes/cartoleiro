using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.WebPages;

namespace Cartoleiro.Web.AppCode.HtmlHelpers
{
    public static class ActionLinkEx
    {
        public static IHtmlString ActionLink<T>(this AjaxHelper ajaxHelper, T item, Func<T, HelperResult> template,
                                                string action, string controller, object routeValues, AjaxOptions options, object htmAttributes = null)
        {
            string rawContent = template(item).ToHtmlString();
            MvcHtmlString a = ajaxHelper.ActionLink("$$$", action,
                controller, routeValues, options, htmAttributes);
            return MvcHtmlString.Create(a.ToString().Replace("$$$", rawContent));
        }
    }
}
