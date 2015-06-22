using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Escalador
{
    public class Time
    {
        // atributos
        private readonly IList<Jogador> _zaga;
        private readonly IList<Jogador> _meioCampo;
        private readonly IList<Jogador> _ataque;

        // propriedades
        public EsquemaTatico EsquemaTatico { get; private set; }

        public Jogador Goleiro { get; private set; }
        public IEnumerable<Jogador> Zaga
        {
            get { return _zaga; }
        }
        public IEnumerable<Jogador> MeioCampo
        {
            get { return _meioCampo; }
        }
        public IEnumerable<Jogador> Ataque
        {
            get { return _ataque; }
        }
        public Jogador Tecnico { get; private set; }

        public IEnumerable<Jogador> TodosJogadores
        {
            get
            {
                // retorno time em ordem de exibicao

                var jogadores = new List<Jogador> { Goleiro };

                if (Zaga.Any(j => j.Posicao == Posicao.Lateral))
                {
                    jogadores.AddRange(Zaga.Where(j => j.Posicao == Posicao.Lateral));
                    jogadores.AddRange(Zaga.Where(j => j.Posicao == Posicao.Zagueiro));
                }
                else
                {
                    jogadores.AddRange(Zaga);
                }

                jogadores.AddRange(MeioCampo);
                jogadores.AddRange(Ataque);
                jogadores.Add(Tecnico);

                return jogadores;
            }
        }
        public int TotalDeJogadores { get; private set; }
        public double ValorDoTime { get; private set; }
        public bool EsValido { get; private set; }

        // construtor
        public Time(EsquemaTatico esquemaTatico)
        {
            EsquemaTatico = esquemaTatico;

            _zaga = new List<Jogador>();
            _meioCampo = new List<Jogador>();
            _ataque = new List<Jogador>();

            EsValido = false;
        }


        // publicos
        public void AddJogador(Jogador jogador)
        {
            switch (jogador.Posicao)
            {
                case Posicao.Goleiro:
                    Goleiro = jogador;
                    break;

                case Posicao.Lateral:
                    if (EsquemaTatico == EsquemaTatico._343 || EsquemaTatico == EsquemaTatico._352)
                    {
                        _meioCampo.Add(jogador);
                    }
                    else
                    {
                        _zaga.Add(jogador);
                    }
                    break;

                case Posicao.Zagueiro:
                    _zaga.Add(jogador);
                    break;

                case Posicao.MeioCampo:
                    _meioCampo.Add(jogador);
                    break;

                case Posicao.Atacante:
                    _ataque.Add(jogador);
                    break;

                case Posicao.Tecnico:
                    Tecnico = jogador;
                    break;
            }

            TotalDeJogadores++;
            ValorDoTime += jogador.Preco.Atual;
            Validar();
        }

        public void Validar()
        {
            var goleiroValido = Goleiro != null && Goleiro.Posicao == Posicao.Goleiro;
            var zagaValida = Zaga.Any() && Zaga.All(j => j.Posicao == Posicao.Zagueiro || j.Posicao == Posicao.Lateral);
            var meioCampoValido = MeioCampo.Any() && MeioCampo.All(j => j.Posicao == Posicao.MeioCampo || j.Posicao == Posicao.Lateral);
            var ataqueValida = Ataque.Any() && Ataque.All(j => j.Posicao == Posicao.Atacante);
            var tecnicoValido = Tecnico != null && Tecnico.Posicao == Posicao.Tecnico;

            var esquemaValido = Zaga.Count() == EsquemaTaticoHelper.NumeroTotalDaZaga(EsquemaTatico) &&
                                MeioCampo.Count() == EsquemaTaticoHelper.NumeroTotalDoMeioCampo(EsquemaTatico) &&
                                Ataque.Count() == EsquemaTaticoHelper.NumeroTotalDoAtaque(EsquemaTatico);

            EsValido = goleiroValido && zagaValida && meioCampoValido &&
                       ataqueValida && tecnicoValido && esquemaValido;
        }
    }
}