using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Escalador.Analizador;

namespace Cartoleiro.Core.Escalador
{
    public class EscaladorDeTime
    {
        // atributos
        private readonly IEnumerable<PontuacaoDeEscalacao> _ranqueamento;

        private EsquemaTatico _esquemaTatico;
        private double _patrimonio;
        private Posicao? _posicaoEmFoco;
        private bool _distribuirProporcionalNaPosicao;
        private IEnumerable<IAnalisador> _analisadores;

        // propriedades
        public ICartolaDataSource CartolaDS { get; set; }

        // construtoes
        public EscaladorDeTime(ICartolaDataSource cartolaDS)
        {
            CartolaDS = cartolaDS;

            _ranqueamento = cartolaDS.Jogadores.Select(j => new PontuacaoDeEscalacao(j)).ToList();

            _esquemaTatico = EsquemaTatico._442;
            _patrimonio = 100;
            _posicaoEmFoco = null;
            _distribuirProporcionalNaPosicao = true;
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

        public EscaladorDeTime DistribuirProporcionalNaPosicao(bool valor)
        {
            return this;
        }

        public EscaladorDeTime ComAnalisadores(IEnumerable<IAnalisador> analisadores)
        {
            _analisadores = analisadores;
            return this;
        }

        public Time MontarTime()
        {
            foreach (var analisador in _analisadores)
            {
                analisador.Analisar(_ranqueamento);
            }

            var partilhaDoDinheiro = new PartilhaDeDinheito(_esquemaTatico, _patrimonio, _posicaoEmFoco);
            var jogadores = EscalarJogadores(partilhaDoDinheiro);

            var time = new Time(_esquemaTatico);
            foreach (var jogador in jogadores)
            {
                if (jogador == null)
                    throw new InvalidOperationException("Não foi possível escalar todos os jogadores do time com o patrimônio (cartoletas) informado");

                time.AddJogador(jogador);
            }

            return time;
        }


        private List<Jogador> EscalarJogadores(PartilhaDeDinheito partilhaDoDinheiro)
        {
            var goleiro = EscolherJogador(Posicao.Goleiro, partilhaDoDinheiro.Partilha[Posicao.Goleiro]);
            var zagueiros = EscolherJogadores(Posicao.Zagueiro, partilhaDoDinheiro.Partilha[Posicao.Zagueiro]);
            var laterais = EscolherJogadores(Posicao.Lateral, partilhaDoDinheiro.Partilha[Posicao.Lateral]);
            var meioCampos = EscolherJogadores(Posicao.MeioCampo, partilhaDoDinheiro.Partilha[Posicao.MeioCampo]);
            var atacantes = EscolherJogadores(Posicao.Atacante, partilhaDoDinheiro.Partilha[Posicao.Atacante]);
            var tecnico = EscolherJogador(Posicao.Tecnico, partilhaDoDinheiro.Partilha[Posicao.Tecnico]);

            var jogadores = new List<Jogador>() { goleiro, tecnico };
            jogadores.AddRange(zagueiros);
            jogadores.AddRange(laterais);
            jogadores.AddRange(meioCampos);
            jogadores.AddRange(atacantes);

            return jogadores;
        }

        private Jogador EscolherJogador(Posicao posicao, double patrimonio)
        {
            var jogadoresMelhoresPontuados = GetJogadoresMelhoresPontuados(posicao);

            return jogadoresMelhoresPontuados.FirstOrDefault(j => j.Preco.Atual <= patrimonio);
        }

        private IEnumerable<Jogador> EscolherJogadores(Posicao posicao, double patrimonio)
        {
            var jogadoresMelhoresPontuados = GetJogadoresMelhoresPontuados(posicao);

            var numeroDeJogadores = EsquemaTaticoHelper.GetNumeroDeJogadores(posicao, _esquemaTatico);
            var jogadores = new List<Jogador>();

            int i = 0;
            var patrimonioCorrente = patrimonio;

            while ((jogadores.Count < numeroDeJogadores) && (i < jogadoresMelhoresPontuados.Count))
            {
                var jogador = jogadoresMelhoresPontuados[i];

                if (jogador.Preco.Atual <= patrimonioCorrente)
                {
                    if (PodeEscolherJogador(jogador, patrimonio))
                    {
                        jogadores.Add(jogador);
                        patrimonioCorrente -= jogador.Preco.Atual;
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

        private bool PodeEscolherJogador(Jogador jogador, double patrimonio)
        {
            if (!_distribuirProporcionalNaPosicao)
                return true;

            var numeroDeJogadores = EsquemaTaticoHelper.GetNumeroDeJogadores(jogador.Posicao, _esquemaTatico);
            var percentualMaximoDeValorAceito = (100 / numeroDeJogadores) * 1.2;

            var percentualDoJogador = jogador.Preco.Atual * 100 / patrimonio;

            return percentualDoJogador <= percentualMaximoDeValorAceito;
        }
    }
}