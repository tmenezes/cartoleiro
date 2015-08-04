using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.Core.Extensions;

namespace Cartoleiro.Core.Escalador
{
    public class EscaladorDeTime
    {
        // atributos
        private IEnumerable<PontuacaoDeEscalacao> _ranqueamento;

        private EsquemaTatico _esquemaTatico;
        private double _patrimonio;
        private Posicao? _posicaoEmFoco;
        private bool _distribuirProporcionalNaPosicao;
        private Analisadores _analisadores;
        private bool _somenteProvaveis;
        private double? _mediaLimite;
        private int? _qtdeJogosLimite;

        // propriedades
        public ICartolaDataSource CartolaDS { get; set; }

        // construtoes
        public EscaladorDeTime(ICartolaDataSource cartolaDS)
        {
            CartolaDS = cartolaDS;

            _esquemaTatico = EsquemaTatico._442;
            _patrimonio = 100;
            _posicaoEmFoco = null;
            _somenteProvaveis = true;
            _distribuirProporcionalNaPosicao = true;
            _mediaLimite = null;

            _analisadores = new AnalisadorBuilder().PontuacaoMedia()
                                                   .UltimaPontuacao()
                                                   .Analisadores;
        }

        public static EscaladorDeTime NovoEscalador()
        {
            return new EscaladorDeTime(null);
        }


        // publicos
        public EscaladorDeTime ComEsquema(EsquemaTatico esquema)
        {
            _esquemaTatico = esquema;
            return this;
        }

        public EscaladorDeTime ComPatrimonio(double patrimonio)
        {
            _patrimonio = patrimonio;
            return this;
        }

        public EscaladorDeTime ComFocoNoAtaque()
        {
            _posicaoEmFoco = Posicao.Atacante;
            return this;
        }
        public EscaladorDeTime ComFocoNaDefesa()
        {
            _posicaoEmFoco = Posicao.Zagueiro;
            return this;
        }
        public EscaladorDeTime ComFocoNoMeioCampo()
        {
            _posicaoEmFoco = Posicao.MeioCampo;
            return this;
        }
        public EscaladorDeTime ComFocoNoGoleiro()
        {
            _posicaoEmFoco = Posicao.Goleiro;
            return this;
        }
        public EscaladorDeTime ComFocoNoTecnico()
        {
            _posicaoEmFoco = Posicao.Tecnico;
            return this;
        }
        public EscaladorDeTime ComFocoNaPosicao(Posicao posicao)
        {
            _posicaoEmFoco = posicao;
            return this;
        }
        public EscaladorDeTime ComFocoNoTimeTodo()
        {
            _posicaoEmFoco = null;
            return this;
        }

        public EscaladorDeTime SomenteProvaveis(bool valor)
        {
            _somenteProvaveis = valor;
            return this;
        }
        public EscaladorDeTime DistribuirProporcionalNaPosicao(bool valor)
        {
            _distribuirProporcionalNaPosicao = valor;
            return this;
        }
        public EscaladorDeTime ComMediaMaiorQue(double media)
        {
            _mediaLimite = media;
            return this;
        }
        public EscaladorDeTime ComQtdeJogosMaiorQue(int jogos)
        {
            _qtdeJogosLimite = jogos;
            return this;
        }

        public EscaladorDeTime ComAnalisadores(Analisadores analisadores)
        {
            _analisadores = analisadores;
            return this;
        }

        public Time MontarTime()
        {
            CriarRanqueamento();

            _analisadores.ExecutarAnalises(_ranqueamento);

            var carteira = new Carteira(_esquemaTatico, _patrimonio, CalcularValoresMinimos(), _posicaoEmFoco);
            var jogadores = EscalarJogadores(carteira);

            var time = new Time(_esquemaTatico);
            foreach (var jogador in jogadores)
            {
                if (jogador == null)
                    throw new InvalidOperationException("Não foi possível escalar todos os jogadores do time com os filtros e patrimônio (cartoletas) informados.");

                time.AddJogador(jogador);
            }

            return time;
        }

        public IEnumerable<PontuacaoDeEscalacao> ObterRanqueamento()
        {
            CriarRanqueamento();

            _analisadores.ExecutarAnalises(_ranqueamento);

            return _ranqueamento;
        }


        private void CriarRanqueamento()
        {
            var jogadores = CartolaDS.Jogadores;

            jogadores = (_somenteProvaveis)
                ? jogadores.Where(j => j.Status == Status.Provavel)
                : jogadores.Where(j => j.Status == Status.Provavel || j.Status == Status.Duvida);

            if (_mediaLimite.HasValue)
                jogadores = jogadores.Where(j => j.Pontuacao.Media > _mediaLimite.Value);

            if (_qtdeJogosLimite.HasValue)
                jogadores = jogadores.Where(j => j.Jogos > _qtdeJogosLimite.Value);

            _ranqueamento = jogadores.Select(j => new PontuacaoDeEscalacao(j)).ToList();
        }

        private List<Jogador> EscalarJogadores(Carteira carteira)
        {
            var goleiro = EscolherJogador(Posicao.Goleiro, carteira);
            var zagueiros = EscolherJogadores(Posicao.Zagueiro, carteira);
            var laterais = EscolherJogadores(Posicao.Lateral, carteira);
            var meioCampos = EscolherJogadores(Posicao.MeioCampo, carteira);
            var atacantes = EscolherJogadores(Posicao.Atacante, carteira);
            var tecnico = EscolherJogador(Posicao.Tecnico, carteira);

            var jogadores = new List<Jogador>() { goleiro, tecnico };
            jogadores.AddRange(zagueiros);
            jogadores.AddRange(laterais);
            jogadores.AddRange(meioCampos);
            jogadores.AddRange(atacantes);

            return jogadores;
        }

        private Jogador EscolherJogador(Posicao posicao, Carteira carteira)
        {
            var jogadoresMelhoresPontuados = _ranqueamento.JogadoresMelhoresPontuados(posicao);

            var jogador = jogadoresMelhoresPontuados.FirstOrDefault(j => carteira.PossuiCartoletasParaComprar(j));
            if (jogador != null)
            {
                carteira.ComprarJogador(jogador);
            }

            return jogador;
        }

        private IEnumerable<Jogador> EscolherJogadores(Posicao posicao, Carteira carteira)
        {
            var jogadoresMelhoresPontuados = _ranqueamento.JogadoresMelhoresPontuados(posicao);

            var numeroDeJogadores = EsquemaTaticoHelper.GetNumeroDeJogadores(posicao, _esquemaTatico);
            var jogadores = new List<Jogador>();

            var patrimonioTotalDaPosicao = carteira.Partilha[posicao];
            int i = 0;

            while ((jogadores.Count < numeroDeJogadores) && (i < jogadoresMelhoresPontuados.Count()))
            {
                var jogador = jogadoresMelhoresPontuados.ElementAt(i);

                if (carteira.PossuiCartoletasParaComprar(jogador))
                {
                    if (PodeEscolherJogador(jogador, patrimonioTotalDaPosicao))
                    {
                        carteira.ComprarJogador(jogador);
                        jogadores.Add(jogador);
                    }
                }

                i++;
            }

            return jogadores;
        }

        private List<Jogador> GetJogadoresMelhoresPontuados(Posicao posicao)
        {
            return _ranqueamento.Where(i => i.Jogador.Posicao == posicao)
                                .OrderByDescending(i => i.Pontos)
                                .Select(i => i.Jogador)
                                .ToList();
        }

        private bool PodeEscolherJogador(Jogador jogador, double patrimonioTotalDaPosicao)
        {
            if (!_distribuirProporcionalNaPosicao)
                return true;

            var numeroDeJogadores = EsquemaTaticoHelper.GetNumeroDeJogadores(jogador.Posicao, _esquemaTatico);
            var percentualMaximoDeValorAceito = (100 / numeroDeJogadores) * 1.2;  // (100 / 2) -> (50 * 1.2) -> 60 %  |  (100 / 3) -> (33.3 * 1.2) -> 40 %

            var percentualDoJogador = jogador.Preco.Atual * 100 / patrimonioTotalDaPosicao;

            return percentualDoJogador <= percentualMaximoDeValorAceito;
        }

        private Dictionary<Posicao, double> CalcularValoresMinimos()
        {
            var valoresMinimos = new Dictionary<Posicao, double>();
            var posicoes = Enum.GetValues(typeof(Posicao)).OfType<Posicao>();
            var jogadores = _ranqueamento.Select(i => i.Jogador).ToList();

            foreach (var posicao in posicoes)
            {
                var posicaoCopia = posicao;
                var jogadoresMaisBaratos = jogadores.Where(j => j.Posicao == posicaoCopia)
                                                    .OrderBy(j => j.Preco.Atual)
                                                    .Take(5);

                var precoMinimo = jogadoresMaisBaratos.Any()
                    ? jogadoresMaisBaratos.Average(j => j.Preco.Atual)
                    : 0;

                valoresMinimos.Add(posicaoCopia, precoMinimo);
            }

            return valoresMinimos;
        }
    }
}
