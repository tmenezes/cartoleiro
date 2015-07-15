using System;
using System.Net;
using System.Threading.Tasks;
using Cartoleiro.Crawler.Crawlers.ApiCartola.Json;
using Cartoleiro.Crawler.Utils;
using Newtonsoft.Json;

namespace Cartoleiro.Crawler.Crawlers.ApiCartola
{
    public class ApiCartolaImagemJogadoresCrawler
    {
        private const string URL_CARTOLA = "http://api.cartola.globo.com";
        private readonly Uri _uriBase;

        public ApiCartolaImagemJogadoresCrawler()
            : this(new Uri(URL_CARTOLA))
        {
        }

        public ApiCartolaImagemJogadoresCrawler(Uri uriBase)
        {
            _uriBase = uriBase;
        }

        public void BaixarImagens()
        {
            var jsonJogadores = HttpClientHelper.Get(_uriBase.ToString(), "/mercado.json");
            var mercado = JsonConvert.DeserializeObject<MercadoAtletas>(jsonJogadores);

            foreach (var atleta in mercado.Atletas)
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        var extensao = ".jpeg"; //atleta.Foto.Substring(atleta.Foto.LastIndexOf('.'));
                        var diretorio = @"c:\temp\";

                        atleta.Foto = atleta.Foto ?? "http://mestrecartoleiro.com/Image/jogador.jpg";

                        var formato50px = atleta.Foto.Replace("FORMATO", "50x50");
                        var formato80px = atleta.Foto.Replace("FORMATO", "80x80");

                        var imgFormato50px = string.Format("{0}{1}_50px{2}", diretorio, atleta.Id, extensao);
                        var imgFormato80px = string.Format("{0}{1}_80px{2}", diretorio, atleta.Id, extensao);


                        client.DownloadFile(new Uri(formato50px), imgFormato50px);
                        client.DownloadFile(new Uri(formato80px), imgFormato80px);
                    }
                    catch (Exception)
                    {
                    }

                    Console.WriteLine("Done! {0}-{1}", atleta.Apelido, atleta.Foto);
                }
            }
        }
    }
}
