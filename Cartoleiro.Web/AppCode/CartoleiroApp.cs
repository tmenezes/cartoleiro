using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.Core.Extensions;
using Cartoleiro.DAO;
using Cartoleiro.Web.AppCode.Extensions;

namespace Cartoleiro.Web.AppCode
{
    public static class CartoleiroApp
    {
        private static readonly DateTime INICIO_CAMPEONATO = DateTime.ParseExact(ConfigurationManager.AppSettings["INICIO_CAMPEONATO"], "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
        private static Dictionary<Clube, string> _descricaoDosClubes;
        private static IEnumerable<Jogador> _jogadores;

        public static ICartolaDataSource CartolaDataSource { get; private set; }
        public static Time TimeDeMaiorMedia { get; private set; }

        public static IEnumerable<Jogador> MelhoresGoleiros { get; private set; }
        public static IEnumerable<Jogador> MelhoresZagueiros { get; private set; }
        public static IEnumerable<Jogador> MelhoresLaterais { get; private set; }
        public static IEnumerable<Jogador> MelhoresMeias { get; private set; }
        public static IEnumerable<Jogador> MelhoresAtacantes { get; private set; }
        public static IEnumerable<Jogador> MelhoresTecnicos { get; private set; }

        public static IEnumerable<Clube> MelhoresDefesas { get; private set; }
        public static IEnumerable<Clube> MelhoresMeioCampos { get; private set; }
        public static IEnumerable<Clube> MelhoresAtaques { get; private set; }


        public static void Iniciar()
        {
            var cartolaDataSource = new CartolaJsonDataSource(HttpContext.Current.Server.MapPath("~/App_Data"));

            Iniciar(cartolaDataSource);
        }

        public static void AtualizarDataSource(ICartolaDataSource cartolaDataSource)
        {
            lock (CartolaDataSource)
            {
                Iniciar(cartolaDataSource);
            }
        }

        public static string GetDescricaoDoClube(Clube clube)
        {
            return _descricaoDosClubes.ContainsKey(clube)
                ? _descricaoDosClubes[clube]
                : string.Empty;
        }

        public static void CarregarDescricaoDosClubes(HttpContext httpContext)
        {
            if (_descricaoDosClubes != null) // ja carregado
                return;

            var appData = HttpContext.Current.Server.MapPath("~/App_Data/Clubes");
            var descricoes = new Dictionary<Clube, string>();

            foreach (var clube in CartolaDataSource.Clubes)
            {
                var arquivo = Path.Combine(appData, string.Concat(clube.GetNomeNormalizado(), ".txt"));
                using (var reader = new StreamReader(arquivo, Encoding.Default))
                {
                    var descricao = AplicarLinks(reader.ReadToEnd());

                    descricoes.Add(clube, descricao);
                }
            }

            _descricaoDosClubes = descricoes;
        }

        public static double CalcularMediaDoClube(Clube clube, SetorDoCampo setorDoCampo)
        {
            switch (setorDoCampo)
            {
                case SetorDoCampo.Defesa:
                    return CalcularMediaDaDefesa(clube);

                case SetorDoCampo.MeioCampo:
                    return CalcularMediaDoMeioCampo(clube);

                case SetorDoCampo.Ataque:
                default:
                    return CalcularMediaDoAtaque(clube);
            }
        }

        public static double CalcularMediaDaDefesa(Clube clube)
        {
            return _jogadores.DoClube(clube).DaDefesa().Media(j => j.Pontuacao.Media);
        }

        public static double CalcularMediaDoMeioCampo(Clube clube)
        {
            return _jogadores.DoClube(clube).DoMeioCampo().Media(j => j.Pontuacao.Media);
        }

        public static double CalcularMediaDoAtaque(Clube clube)
        {
            return _jogadores.DoClube(clube).DoAtaque().Media(j => j.Pontuacao.Media);
        }

        public static bool CampeonatoIniciado { get { return DateTime.Now > INICIO_CAMPEONATO; } }


        private static void Iniciar(ICartolaDataSource cartolaDataSource)
        {
            CartolaDataSource = cartolaDataSource;

            _jogadores = CartolaDataSource.Jogadores.Where(j => j.Status == Status.Provavel || j.Status == Status.Duvida).ToList();

            EscalarMelhorTime();

            DefinirMelhoresJogadores();
            DefinirMelhoresClubes();
        }

        private static void EscalarMelhorTime()
        {
            var escalador = new EscaladorDeTime(CartolaDataSource)
                .ComPatrimonio(double.MaxValue)
                .ComAnalisadores(new AnalisadorBuilder().PontuacaoMedia().Analisadores);

            TimeDeMaiorMedia = escalador.MontarTime();
        }

        private static void DefinirMelhoresJogadores()
        {
            var escalador = new EscaladorDeTime(CartolaDataSource)
                .ComPatrimonio(double.MaxValue)
                .ComQtdeJogosMaiorQue(Convert.ToInt32(Campeonato.Rodadas.RodadaAtual.Numero * 0.33))
                .ComAnalisadores(new AnalisadorBuilder().PontuacaoMedia().ScoutsPorPosicao().Analisadores);

            var ranqueamento = escalador.ObterRanqueamento().ToList();

            MelhoresGoleiros = ranqueamento.JogadoresMelhoresPontuados(Posicao.Goleiro).Take(5);
            MelhoresLaterais = ranqueamento.JogadoresMelhoresPontuados(Posicao.Lateral).Take(5);
            MelhoresZagueiros = ranqueamento.JogadoresMelhoresPontuados(Posicao.Zagueiro).Take(5);
            MelhoresMeias = ranqueamento.JogadoresMelhoresPontuados(Posicao.MeioCampo).Take(5);
            MelhoresAtacantes = ranqueamento.JogadoresMelhoresPontuados(Posicao.Atacante).Take(5);
            MelhoresTecnicos = ranqueamento.JogadoresMelhoresPontuados(Posicao.Tecnico).Take(5);
        }

        private static void DefinirMelhoresClubes()
        {
            var escalador = new EscaladorDeTime(CartolaDataSource)
                .SomenteProvaveis(false)
                .ComPatrimonio(double.MaxValue)
                .ComAnalisadores(new AnalisadorBuilder().PontuacaoMedia().Analisadores);

            var ranqueamento = escalador.ObterRanqueamento().ToList();

            MelhoresDefesas = ranqueamento.MelhoresPontuados(Posicao.Lateral, Posicao.Zagueiro).AgruparPorClube().Take(5);
            MelhoresMeioCampos = ranqueamento.MelhoresPontuados(Posicao.MeioCampo).AgruparPorClube().Take(5);
            MelhoresAtaques = ranqueamento.MelhoresPontuados(Posicao.Atacante).AgruparPorClube().Take(5).ToList();
        }

        private static string AplicarLinks(string descricao)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var linkTemplate = @"<a href='{1}'>{0}</a>";
            foreach (var clube in CartolaDataSource.Clubes)
            {
                var url = urlHelper.Action("Detalhe", "Clube", new { id = clube.GetNomeNormalizado() });
                var link = string.Format(linkTemplate, clube.Nome, url);

                descricao = descricao.Replace(clube.Nome, link);
            }

            return descricao;
        }
    }
}
