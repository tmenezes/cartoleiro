﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cartoleiro.Core.Data;
using Cartoleiro.Crawler;
using Cartoleiro.Crawler.Crawlers.ApiCartola;
using Newtonsoft.Json;

namespace Cartoleiro.CrawlerConsole
{
    class Program
    {
        private static string ArquivoClubesJson { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "clubes.json"); } }
        private static string ArquivoJogadoresJson { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jogadores.json"); } }
        private static string ArquivoRodadasJson { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rodadas.json"); } }
        private static string ArquivoHistoricoDeJogosJson { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "historicoDeJogos.json"); } }

        static void Main(string[] args)
        {
            var crawler = new CartoleiroCrawler();
            crawler.ObjetoCarregado += Crawler_ObjetoCarregado;

            var tipoExecucao = GetTipoExecucao();
            ExecutarCrawler(GetExcecucaoCrawler(crawler, tipoExecucao));


            Console.WriteLine("Crawler em execucao... Pressione qualquer tecla para cancelar.");
            Console.WriteLine("");
            Console.ReadLine();
        }


        private static void ExecutarCrawler(Func<ICartolaDataSource> executarCrawler)
        {
            // setup de exceptions
            TaskScheduler.UnobservedTaskException += (sender, excArgs) =>
            {
                Console.WriteLine("Erro ao salvar: {0}", excArgs.Exception.Message);
                excArgs.SetObserved();
            };

            // executando...
            Task.Factory.StartNew(() => executarCrawler())
                        .ContinueWith(t => SalvarDataSource(t.Result));
        }

        private static void SalvarDataSource(ICartolaDataSource dataSource)
        {
            Console.WriteLine("");
            Console.WriteLine("Salvando informacao capturada...");

            if (dataSource != null)
            {
                SavlarDados(dataSource.Clubes, ArquivoClubesJson);
                SavlarDados(dataSource.Jogadores, ArquivoJogadoresJson);
                SavlarDados(dataSource.Rodadas, ArquivoRodadasJson);
                SavlarDados(dataSource.HistoricoDeJogos, ArquivoHistoricoDeJogosJson);
            }

            Console.WriteLine("");
            Console.WriteLine("Crawler concluido!!! Pressione qualquer tecla para sair.");
        }

        private static void SavlarDados<T>(IEnumerable<T> lista, string arquivo)
        {
            if (!lista.Any())
                return;

            Console.WriteLine("Salvando {0}...", typeof(T).Name);

            try
            {
                using (var writer = new StreamWriter(arquivo, false, Encoding.Default))
                {
                    foreach (var item in lista)
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(item));
                    }
                }

                Console.WriteLine("Dados salvos com sucesso em:");
                Console.WriteLine(arquivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar {0}: \n{1}", typeof(T).Name, ex.Message);
            }
        }


        private static TipoExecucaoCrawler GetTipoExecucao()
        {
            while (true)
            {
                Console.WriteLine("Informe o tipo de execucao:");
                Console.WriteLine("1 - Clubes");
                Console.WriteLine("2 - Jogadores ");
                Console.WriteLine("3 - Rodadas ");
                Console.WriteLine("4 - Histórico de jogos");
                Console.WriteLine("5 - Completa");
                Console.WriteLine("6 - Imagem Jogadores");
                var tipoSelecionado = Console.ReadLine();

                try
                {
                    if (!Enum.IsDefined(typeof(TipoExecucaoCrawler), Convert.ToInt32(tipoSelecionado)))
                        throw new InvalidOperationException("Opção invalida");

                    var tipoExecucao = (TipoExecucaoCrawler)Convert.ToInt32(tipoSelecionado);
                    return tipoExecucao;
                }
                catch (Exception)
                {
                    Console.WriteLine("");
                    Console.WriteLine("!!!!!!!! Opcao invalida !!!!!!!!");
                    Console.WriteLine("");
                }
            }
        }

        private static Func<ICartolaDataSource> GetExcecucaoCrawler(CartoleiroCrawler crawler, TipoExecucaoCrawler tipoExecucao)
        {
            switch (tipoExecucao)
            {
                case TipoExecucaoCrawler.Clubes:
                    return crawler.ExecutarCrawlerDeClubes;

                case TipoExecucaoCrawler.Jogadores:
                    return crawler.ExecutarCrawlerDeJogadores;

                case TipoExecucaoCrawler.Rodadas:
                    return crawler.ExecutarCrawlerDeRodadas;

                case TipoExecucaoCrawler.HistoricoDeJogos:
                    return crawler.ExecutarCrawlerDeHistoricoDeJogos;

                case TipoExecucaoCrawler.ImagemJogadores:
                    return BaixarImagemJogadores;

                case TipoExecucaoCrawler.Completa:
                default:
                    return crawler.Executar;
            }
        }

        private static ICartolaDataSource BaixarImagemJogadores()
        {
            new ApiCartolaImagemJogadoresCrawler().BaixarImagens();

            return null;
        }

        private static void Crawler_ObjetoCarregado(object sender, CrawlingInfo e)
        {
            Console.WriteLine("");
            Console.WriteLine("Progresso: {0:000} de {1}", e.ObjetosCarregados, e.TotalDeObjetos);
            Console.WriteLine("Objeto   : {0} -> {1}", e.TipoDoObjeto, e.DadosDoObjeto);
        }
    }
}
