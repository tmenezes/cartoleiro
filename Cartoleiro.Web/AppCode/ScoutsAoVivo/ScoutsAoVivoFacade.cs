using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Web.AppCode.Extensions;
using Cartoleiro.Web.AppCode.Utils;
using Cartoleiro.Web.Models.ScoutsAoVivoModels;
using Elmah;
using Newtonsoft.Json;

namespace Cartoleiro.Web.AppCode.ScoutsAoVivo
{
    public static class ScoutsAoVivoFacade
    {
        private static DateTime _dataAtualizacaoDosDados = DateTime.MinValue;

        private static IDictionary<string, string> _chavesScouts;
        private static IDictionary<string, Jogo> _jogosPorIdPartida;
        private static IDictionary<Jogo, ScoutsData> _scoutsDasPartidas;
        private static IDictionary<Jogo, ValidadeScouts> _validadeDosScouts;

        private static Task _atualizadorDeScouts;
        private static HttpContext _httpContext;

        public static ScoutsData Scouts { get; private set; }


        // publicos
        public static void Iniciar()
        {
            _atualizadorDeScouts = Task.Factory.StartNew(AtualizarScouts, TaskCreationOptions.LongRunning);

        }


        public static void SetHttpContext(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public static ScoutsData ObterScoutsAoVivo(string idPartida)
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
                        AtualizarChavesDosJogos();

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
                catch (Exception ex)
                {
                    LogError(ex);
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
                var url = "http://scoutsaovivo.appspot.com";
                var urlRecurso = "getdata.php?match=" + _chavesScouts[idPartida];

                var json = HttpClientHelper.Get(url, urlRecurso);
                var scouts = JsonConvert.DeserializeObject<ScoutsData>(json);
                Scouts = scouts;

                return scouts;
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

        private static void AtualizarChavesDosJogos()
        {
            var crawler = new ScoutsAoVivoJogosCrawler();
            var idsPartidas = crawler.CarregarDosJogos();

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

        private static void LogError(Exception ex)
        {
            try
            {
                if (_httpContext != null)
                {
                    ErrorSignal.FromContext(_httpContext).Raise(ex);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}