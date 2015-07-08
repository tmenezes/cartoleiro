using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Cartoleiro.Web
{
    public static class CartoleiroKeepAlive
    {
        private const int QUATRO_MINUTOS = 1000 * 60 * 4;
        private static bool _inicializado = false;
        private static string _url = "";


        public static void Iniciar(HttpContext httpContext)
        {
            if (_inicializado)
                return;

            _url = httpContext.Request.Url.Scheme + Uri.SchemeDelimiter + httpContext.Request.Url.Host +
                   (httpContext.Request.Url.IsDefaultPort ? "" : ":" + httpContext.Request.Url.Port);

            DoKeepAliveRequest();

            _inicializado = true;
        }

        private static void DoKeepAliveRequest()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_url);
                        var response = client.GetAsync("Home/KeepAlive").Result;
                    }

                    Thread.Sleep(QUATRO_MINUTOS);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}