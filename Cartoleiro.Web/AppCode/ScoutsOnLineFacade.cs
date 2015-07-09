using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Crawler.Crawlers.ScoutsAoVivo;
using Cartoleiro.Web.AppCode.Extensions;
using Cartoleiro.Web.Models.ScoutsAoVivoModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Cartoleiro.Web.AppCode
{
    public static class ScoutsOnLineFacade
    {
        private static DateTime _dataAtualizacaoDosDados = DateTime.MinValue;

        private static IDictionary<string, string> _chavesScouts;
        private static IDictionary<string, Jogo> _jogosPorIdPartida;
        private static IDictionary<Jogo, ScoutsData> _scoutsDasPartidas;
        private static IDictionary<Jogo, ValidadeScouts> _validadeDosScouts;
        private static ConcurrentQueue<Jogo> _atualizacoesDeScoutsPendentes;

        private static Task _atualizadorDeScouts;


        // publicos
        public static void Iniciar()
        {
            _atualizacoesDeScoutsPendentes = new ConcurrentQueue<Jogo>();

            _atualizadorDeScouts = Task.Factory.StartNew(AtualizarScouts, TaskCreationOptions.LongRunning);
        }

        public static ScoutsData ObterScoutsOnLine(string idPartida)
        {
            var jogo = _jogosPorIdPartida.ContainsKey(idPartida) ? _jogosPorIdPartida[idPartida] : null;
            if (jogo == null)
                return null;

            var scoutsAtual = VerificarValidadeDosScouts(jogo);
            if (scoutsAtual)
            {
                var deveAnteciparProximaAtualizacao = _validadeDosScouts[jogo].DataProximaAtualizacao > DateTime.Now.AddSeconds(30);
                if (deveAnteciparProximaAtualizacao)
                {
                    _validadeDosScouts[jogo].DataProximaAtualizacao = DateTime.Now.AddSeconds(30);
                }

                return _scoutsDasPartidas[jogo];
            }

            var scouts = ObterScouts(idPartida, DateTime.Now.AddSeconds(60));

            return scouts;
        }


        // privados
        private static void AtualizarScouts()
        {
            while (true)
            {
                try
                {
                    // TASK 1: atualizar estruturas
                    bool deveAtualizarEstruturas = DateTime.Now > _dataAtualizacaoDosDados.AddHours(4);
                    if (deveAtualizarEstruturas)
                    {
                        InicializarEstruturasDeDados();
                        AtualizarChavesDasPartidas();

                        _dataAtualizacaoDosDados = DateTime.Now;
                    }


                    // TASK 2: atualizar scouts on line
                    foreach (var jogo in Campeonato.Rodadas.ProximaRodada.Jogos)
                    {
                        var deveAtualizarScouts = _validadeDosScouts[jogo].DeveAtualizar;
                        if (deveAtualizarScouts)
                        {
                            ObterScouts(jogo.GetIdJogo(), DateTime.Now.AddHours(4), 3);
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }
        }


        private static ScoutsData ObterScouts(string idPartida, DateTime proximaAtualizacao, int tentativas = 1)
        {
            while (tentativas > 0)
            {
                var scouts = ObterScoutsNaOrigem(idPartida);
                if (scouts != null)
                {
                    var jogo = _jogosPorIdPartida[idPartida];

                    _scoutsDasPartidas[jogo] = scouts;
                    _validadeDosScouts[jogo].Atualizar(proximaAtualizacao);

                    return scouts;
                }

                tentativas--;
            }

            return null;
        }

        private static ScoutsData ObterScoutsNaOrigem(string idPartida)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://scoutsaovivo.appspot.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("getdata.php?match=" + _chavesScouts[idPartida]).Result; // 81_avai_gremio

                    if (!response.IsSuccessStatusCode)
                        return null;

                    var content = response.Content.ReadAsStringAsync().Result;
                    var scouts = JsonConvert.DeserializeObject<ScoutsData>(content);

                    return scouts;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static bool VerificarValidadeDosScouts(Jogo jogo)
        {
            var scouts = _scoutsDasPartidas[jogo];
            if (scouts == null)
                return false;

            return _validadeDosScouts[jogo].DentroDaValidade;
        }

        private static void InicializarEstruturasDeDados()
        {
            _jogosPorIdPartida = new ConcurrentDictionary<string, Jogo>();
            _scoutsDasPartidas = new ConcurrentDictionary<Jogo, ScoutsData>();
            _validadeDosScouts = new ConcurrentDictionary<Jogo, ValidadeScouts>();

            foreach (var jogo in Campeonato.Rodadas.ProximaRodada.Jogos)
            {
                _jogosPorIdPartida.Add(jogo.GetIdJogo(), jogo);
                _scoutsDasPartidas.Add(jogo, null);
                _validadeDosScouts.Add(jogo, new ValidadeScouts());
            }
        }

        private static void AtualizarChavesDasPartidas()
        {
            var crawler = new ScoutsAoVivoJogosCrawler();
            var idsPartidas = crawler.CarregarIdsPartidas();

            _chavesScouts = new ConcurrentDictionary<string, string>();

            foreach (var jogo in Campeonato.Rodadas.ProximaRodada.Jogos)
            {
                var chaveScouts = idsPartidas.First(i => i.Contains(GetNomeDoClubeParaScoutsOnLine(jogo.Mandante)));
                _chavesScouts.Add(jogo.GetIdJogo(), chaveScouts);
            }
        }

        private static string GetNomeDoClubeParaScoutsOnLine(Clube clube)
        {
            var clubeSemAcento = ModelUtils.RemoverAcentos(clube.Nome.ToLower().Replace(" ", "-"));

            return clubeSemAcento;
        }
    }

    internal class ValidadeScouts
    {
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataProximaAtualizacao { get; set; }

        public bool DentroDaValidade { get { return DateTime.Now < DataAtualizacao.AddSeconds(60); } }
        public bool DeveAtualizar { get { return DateTime.Now > DataProximaAtualizacao; } }

        public ValidadeScouts()
        {
            DataAtualizacao = DateTime.MinValue;
            DataProximaAtualizacao = DateTime.MaxValue;
        }

        public void Atualizar(DateTime proximaAtualizacao)
        {
            DataAtualizacao = DateTime.Now;
            DataProximaAtualizacao = proximaAtualizacao;
        }
    }
}
