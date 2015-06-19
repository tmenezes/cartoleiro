using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cartoleiro.Core.Data;
using Cartoleiro.Crawler;
using Newtonsoft.Json;

namespace Cartoleiro.CrawlerConsole
{
    class Program
    {
        private static string CaminhoDataSource { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jogadores.json"); } }

        static void Main(string[] args)
        {
            var crawler = new CartoleiroCrawler();
            crawler.ObjetoCarregado += crawler_ObjetoCarregado;

            ExecutarCrawler(crawler);

            Console.WriteLine("Crawler em execucao. Pressione qualquer tecla para cancelar");
            Console.WriteLine("");
            Console.ReadLine();
        }


        private static void ExecutarCrawler(CartoleiroCrawler crawler)
        {
            // setup de exceptions
            TaskScheduler.UnobservedTaskException += (sender, excArgs) =>
            {
                Console.WriteLine("Erro ao salvar: {0}", excArgs.Exception.Message);
                excArgs.SetObserved();
            };

            // executando...
            Task.Factory.StartNew(() => crawler.Executar())
                         .ContinueWith(t => SalvarDataSource(t.Result));
        }

        private static void SalvarDataSource(ICartolaDataSource dataSource)
        {
            Console.WriteLine("");
            Console.WriteLine("Salvando informação capturada...");

            try
            {
                using (var arquivoDeJogadores = new StreamWriter(CaminhoDataSource, false, Encoding.Default))
                {
                    foreach (var jogador in dataSource.Jogadores)
                    {
                        arquivoDeJogadores.WriteLine(JsonConvert.SerializeObject(jogador));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar: {0}", ex.Message);
            }

            Console.WriteLine("Dados foram salvos em:");
            Console.WriteLine("\t{0}", CaminhoDataSource);
        }


        static void crawler_ObjetoCarregado(object sender, CrawlingInfo e)
        {
            Console.WriteLine("");
            Console.WriteLine("Objeto carregado: {0:000} de {1}", e.ObjetosCarregados, e.TotalDeObjetos);
            Console.WriteLine("Objeto          : {0}", e.DadosDoObjeto);
        }
    }
}
