using System;
using System.Net;
using System.Net.Http;

namespace Cartoleiro.Crawler.Utils
{
    public static class HttpClientHelper
    {
        public static string Get(string urlBase, string urlRecurso = "")
        {
            var clientHandler = new HttpClientHandler
                                {
                                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                                };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(urlBase);

                var response = client.GetAsync(urlRecurso).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}
