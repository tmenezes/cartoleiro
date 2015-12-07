using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Extensions;
using Cartoleiro.Crawler.Crawlers.ApiCartola;
using Newtonsoft.Json;

namespace Cartoleiro.Web.AppCode
{
    public static class AtualizadorCartola
    {
        private const int DEZ_MINUTOS = 1000 * 60 * 10;
        private static bool _inicializado = false;
        private static string _pastaAppData;
        private static string ArquivoJogadores { get { return Path.Combine(_pastaAppData, "jogadores.json"); } }


        public static void Iniciar()
        {
            if (_inicializado)
                return;

            _pastaAppData = HttpContext.Current.Server.MapPath("~/App_Data");
            _inicializado = true;

            AtualizarJogadores();
        }

        private static void AtualizarJogadores()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {

                    if (!CartoleiroApp.CampeonatoIniciado)
                    {
                        Thread.Sleep(DEZ_MINUTOS);
                        continue;
                    }

                    try
                    {
                        Thread.Sleep(DEZ_MINUTOS);

                        var jogadores = new ApiCartolaSiteCrawler().CarregarJogadores();
                        if (jogadores.Any())
                        {
                            SalvarJsonJogadores(jogadores);
                            AtualizarJogadoresEmMemoria(jogadores);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }


        private static void AtualizarJogadoresEmMemoria(IEnumerable<Jogador> jogadores)
        {
            jogadores.SetClubes(CartoleiroApp.CartolaDataSource.Clubes);

            var novoCartolaDataSource = new CartolaDataSourceWeb(CartoleiroApp.CartolaDataSource, jogadores);

            CartoleiroApp.AtualizarDataSource(novoCartolaDataSource);
        }

        private static void SalvarJsonJogadores(IEnumerable<Jogador> jogadores)
        {
            using (var writer = new StreamWriter(ArquivoJogadores, false, Encoding.Default))
            {
                foreach (var item in jogadores)
                {
                    writer.WriteLine(JsonConvert.SerializeObject(item));
                }
            }
        }
    }
}