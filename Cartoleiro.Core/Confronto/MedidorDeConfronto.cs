using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Util;

namespace Cartoleiro.Core.Confronto
{
    public class MedidorDeConfronto
    {
        private readonly IEnumerable<Jogador> _jogadores;
        private readonly Dictionary<TipoMedicao, Func<ItemDeMedicaoDeConfronto>> _medidores;

        public Clube Mandande { get; set; }
        public Clube Visitante { get; set; }
        public ICartolaDataSource CartolaDataSource { get; set; }


        // construtores
        public MedidorDeConfronto(Clube mandande, Clube visitante)
            : this(mandande, visitante, CartolaData.CartolaDataSource)
        {
        }

        public MedidorDeConfronto(Clube mandande, Clube visitante, ICartolaDataSource cartolaDataSource)
        {
            Mandande = mandande;
            Visitante = visitante;
            CartolaDataSource = cartolaDataSource;

            _jogadores = CartolaDataSource.Jogadores.Where(j => j.Status == Status.Provavel || j.Status == Status.Duvida).ToList();

            _medidores = new Dictionary<TipoMedicao, Func<ItemDeMedicaoDeConfronto>>()
                         {
                             { TipoMedicao.PontosNoCampeonato, MedirPosicaoNoCampeonato },
                             { TipoMedicao.Vitorias, MedirVitorias},
                             { TipoMedicao.Derrotas, MedirDerrotas},
                             { TipoMedicao.AproveitamentoEmCasa, MedirAproveitamentoEmCasa },
                             { TipoMedicao.AproveitamentoForaDeCasa, MedirAproveitamentoForaDeCasa },
                             { TipoMedicao.AproveitamentoNoCampeonato, MedirAproveitamentoNoCampeonato},
                             { TipoMedicao.GolsPro, MedirGolsPro},
                             { TipoMedicao.GolsContra, MedirGolsContra},
                             { TipoMedicao.SaldoDeGols, MedirSaldoDeGols},
                             { TipoMedicao.MediaDaDefesa, MedirMediaDaDefesa },
                             { TipoMedicao.MediaDaMeioCampo, MedirMediaDoMeioCampo },
                             { TipoMedicao.MediaDaAtaque, MedirMediaDoAtaque },
                             { TipoMedicao.MediaDoClube, MedirMediaDoClube },
                             { TipoMedicao.VitoriasEmConfrontosNoBrasileiro, MedirHistoricoDeVitoriasNoBrasileiro },
                             { TipoMedicao.VitoriasEmTodosOsConfronto, MedirHistoricoDeVitoriasNoConfronto },
                             { TipoMedicao.VitoriasNoBrasileiro, MedirVitoriasNaBrasileiro},
                             { TipoMedicao.VitoriasNaHistoriaDoClube, MedirVitoriasNaHistoriaDoClube},
                         };
        }

        // publicos
        public ResultadoDoConfronto MedirConfronto()
        {
            return MedirConfronto(EnumUtils.TodosOsItens<TipoMedicao>());
        }

        public ResultadoDoConfronto MedirConfronto(IEnumerable<TipoMedicao> tiposDeMedicao)
        {
            var result = new ResultadoDoConfronto(Mandande, Visitante);

            foreach (var tipoMedicao in tiposDeMedicao)
            {
                result.AdicionarItemDeMedicao(_medidores[tipoMedicao].Invoke());
            }

            return result;
        }


        // privados
        private ItemDeMedicaoDeConfronto MedirPosicaoNoCampeonato()
        {
            var pontosMandante = Mandande.Campeonato.Pontos;
            var pontosVisitante = Visitante.Campeonato.Pontos;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.PontosNoCampeonato, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirVitorias()
        {
            var pontosMandante = Mandande.Campeonato.Vitorias;
            var pontosVisitante = Visitante.Campeonato.Vitorias;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.Vitorias, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirDerrotas()
        {
            var pontosMandante = Mandande.Campeonato.Derrotas;
            var pontosVisitante = Visitante.Campeonato.Derrotas;

            var vencedor = (pontosMandante < pontosVisitante)
                ? Mandande
                : (pontosVisitante < pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.Derrotas, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirAproveitamentoEmCasa()
        {
            var pontosMandante = Mandande.Campeonato.AproveitamentoEmCasa / 100;
            var pontosVisitante = Visitante.Campeonato.AproveitamentoEmCasa / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.AproveitamentoEmCasa, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private ItemDeMedicaoDeConfronto MedirAproveitamentoForaDeCasa()
        {
            var pontosMandante = Mandande.Campeonato.AproveitamentoForaDeCasa / 100;
            var pontosVisitante = Visitante.Campeonato.AproveitamentoForaDeCasa / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.AproveitamentoForaDeCasa, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private ItemDeMedicaoDeConfronto MedirAproveitamentoNoCampeonato()
        {
            var pontosMandante = Mandande.Campeonato.Aproveitamento / 100;
            var pontosVisitante = Visitante.Campeonato.Aproveitamento / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.AproveitamentoNoCampeonato, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private ItemDeMedicaoDeConfronto MedirGolsPro()
        {
            var pontosMandante = Mandande.Campeonato.GolsPro;
            var pontosVisitante = Visitante.Campeonato.GolsPro;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.GolsPro, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirGolsContra()
        {
            var pontosMandante = Mandande.Campeonato.GolsContra;
            var pontosVisitante = Visitante.Campeonato.GolsContra;

            var vencedor = (pontosMandante < pontosVisitante)
                ? Mandande
                : (pontosVisitante < pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.GolsContra, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirSaldoDeGols()
        {
            var pontosMandante = Mandande.Campeonato.SaldoDeGol;
            var pontosVisitante = Visitante.Campeonato.SaldoDeGol;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.SaldoDeGols, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirMediaDaDefesa()
        {
            var pontosMandante = _jogadores.Where(j => j.Clube == Mandande && (j.Posicao == Posicao.Zagueiro || j.Posicao == Posicao.Lateral)).Average(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante && (j.Posicao == Posicao.Zagueiro || j.Posicao == Posicao.Lateral)).Average(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.MediaDaDefesa, vencedor, pontosMandante, pontosVisitante);
        }

        private ItemDeMedicaoDeConfronto MedirMediaDoMeioCampo()
        {
            var pontosMandante = _jogadores.Where(j => j.Clube == Mandande && j.Posicao == Posicao.MeioCampo).Average(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante && j.Posicao == Posicao.MeioCampo).Average(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.MediaDaMeioCampo, vencedor, pontosMandante, pontosVisitante);
        }

        private ItemDeMedicaoDeConfronto MedirMediaDoAtaque()
        {
            var pontosMandante = _jogadores.Where(j => j.Clube == Mandande && j.Posicao == Posicao.Atacante).Average(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante && j.Posicao == Posicao.Atacante).Average(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.MediaDaAtaque, vencedor, pontosMandante, pontosVisitante);
        }

        private ItemDeMedicaoDeConfronto MedirMediaDoClube()
        {
            var pontosMandante = _jogadores.Where(j => j.Clube == Mandande).Average(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante).Average(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.MediaDoClube, vencedor, pontosMandante, pontosVisitante);
        }

        private ItemDeMedicaoDeConfronto MedirHistoricoDeVitoriasNoBrasileiro()
        {
            var confrontos = HistoricoDeJogos.GetHistoricoDeConfrontos(Mandande, Visitante).Where(j => j.Campeonato == TipoCampeonato.CampeonatoBrasileiro).ToList();

            var pontosMandante = confrontos.Count(j => j.Vencedor() == Mandande);
            var pontosVisitante = confrontos.Count(j => j.Vencedor() == Visitante);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.VitoriasEmConfrontosNoBrasileiro, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirHistoricoDeVitoriasNoConfronto()
        {
            var confrontos = HistoricoDeJogos.GetHistoricoDeConfrontos(Mandande, Visitante).ToList();

            var pontosMandante = confrontos.Count(j => j.Vencedor() == Mandande);
            var pontosVisitante = confrontos.Count(j => j.Vencedor() == Visitante);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.VitoriasEmTodosOsConfronto, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirVitoriasNaBrasileiro()
        {
            var pontosMandante = HistoricoDeJogos.GetHistoricoDeJogos(Mandande).Count(j => j.Vencedor() == Mandande && j.Campeonato == TipoCampeonato.CampeonatoBrasileiro);
            var pontosVisitante = HistoricoDeJogos.GetHistoricoDeJogos(Visitante).Count(j => j.Vencedor() == Visitante && j.Campeonato == TipoCampeonato.CampeonatoBrasileiro);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.VitoriasNoBrasileiro, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirVitoriasNaHistoriaDoClube()
        {
            var pontosMandante = HistoricoDeJogos.GetHistoricoDeJogos(Mandande).Count(j => j.Vencedor() == Mandande);
            var pontosVisitante = HistoricoDeJogos.GetHistoricoDeJogos(Visitante).Count(j => j.Vencedor() == Visitante);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(TipoMedicao.VitoriasNaHistoriaDoClube, vencedor, pontosMandante, pontosVisitante, "G");
        }
    }
}
