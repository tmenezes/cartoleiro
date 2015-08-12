using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Extensions;
using Cartoleiro.Core.Util;

namespace Cartoleiro.Core.Confronto.Indicador
{
    public class CalculadorDeIndicadores
    {
        private readonly IEnumerable<Jogador> _jogadores;
        private readonly Dictionary<TipoDeIndicador, Func<Indicador>> _indicadores;

        public Clube Mandande { get; set; }
        public Clube Visitante { get; set; }
        public ICartolaDataSource CartolaDataSource { get; set; }


        // construtores
        public CalculadorDeIndicadores(Clube mandande, Clube visitante)
            : this(mandande, visitante, CartolaData.CartolaDataSource)
        {
        }

        public CalculadorDeIndicadores(Clube mandande, Clube visitante, ICartolaDataSource cartolaDataSource)
        {
            Mandande = mandande;
            Visitante = visitante;
            CartolaDataSource = cartolaDataSource;

            _jogadores = CartolaDataSource.Jogadores.Where(j => j.Status == Status.Provavel || j.Status == Status.Duvida).ToList();

            _indicadores = new Dictionary<TipoDeIndicador, Func<Indicador>>()
                           {
                               { TipoDeIndicador.PontosNoCampeonato, CalcularPosicaoNoCampeonato },
                               { TipoDeIndicador.PontosNosUltimos5Jogos, CalcularPontosNosUltimos5Jogos },
                               { TipoDeIndicador.VitoriasEmCasa, CalcularVitoriasEmCasa},
                               { TipoDeIndicador.VitoriasForaDeCasa, CalcularVitoriasForaDeCasa},
                               { TipoDeIndicador.DerrotasEmCasa, CalcularDerrotasEmCasa},
                               { TipoDeIndicador.DerrotasForaCasa, CalcularDerrotasForaDeCasa},
                               { TipoDeIndicador.AproveitamentoEmCasa, CalcularAproveitamentoEmCasa },
                               { TipoDeIndicador.AproveitamentoForaDeCasa, CalcularAproveitamentoForaDeCasa },
                               { TipoDeIndicador.AproveitamentoNoCampeonato, CalcularAproveitamentoNoCampeonato},
                               { TipoDeIndicador.GolsPro, CalcularGolsPro},
                               { TipoDeIndicador.GolsContra, CalcularGolsContra},
                               { TipoDeIndicador.SaldoDeGols, CalcularSaldoDeGols},
                               { TipoDeIndicador.MediaDaDefesa, CalcularMediaDaDefesa },
                               { TipoDeIndicador.MediaDaMeioCampo, CalcularMediaDoMeioCampo },
                               { TipoDeIndicador.MediaDaAtaque, CalcularMediaDoAtaque },
                               //{ TipoMedicao.MediaDoClube, CalcularMediaDoClube },
                               { TipoDeIndicador.VitoriasEmConfrontosNoBrasileiro, CalcularHistoricoDeVitoriasNoBrasileiro },
                               { TipoDeIndicador.VitoriasEmTodosOsConfronto, CalcularHistoricoDeVitoriasNoConfronto },
                               { TipoDeIndicador.VitoriasSobreJogosNoBrasileiro, CalcularVitoriasNaBrasileiro},
                               { TipoDeIndicador.VitoriasSobreJogosNaHistoriaDoClube, CalcularVitoriasNaHistoriaDoClube},
                           };
        }

        // publicos
        public ResultadoDosIndicadores CalcularConfronto()
        {
            return CalcularConfronto(EnumUtils.TodosOsItens<TipoDeIndicador>());
        }

        public ResultadoDosIndicadores CalcularConfronto(IEnumerable<TipoDeIndicador> tiposDeIndicadores)
        {
            var result = new ResultadoDosIndicadores(Mandande, Visitante);

            foreach (var tipoMedicao in tiposDeIndicadores)
            {
                result.AdicionarIndicador(_indicadores[tipoMedicao].Invoke());
            }

            return result;
        }


        // privados
        private Indicador CalcularPosicaoNoCampeonato()
        {
            var pontosMandante = Mandande.Campeonato.Pontos;
            var pontosVisitante = Visitante.Campeonato.Pontos;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.PontosNoCampeonato, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularPontosNosUltimos5Jogos()
        {
            var pontosMandante = Mandande.Campeonato.UltimosJogos.TotalDePontos;
            var pontosVisitante = Visitante.Campeonato.UltimosJogos.TotalDePontos;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.PontosNosUltimos5Jogos, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularVitoriasEmCasa()
        {
            var pontosMandante = Mandande.Campeonato.VitoriasEmCasa;
            var pontosVisitante = Visitante.Campeonato.VitoriasEmCasa;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.VitoriasEmCasa, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularVitoriasForaDeCasa()
        {
            var pontosMandante = Mandande.Campeonato.VitoriasForaDeCasa;
            var pontosVisitante = Visitante.Campeonato.VitoriasForaDeCasa;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.VitoriasForaDeCasa, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularDerrotasEmCasa()
        {
            var pontosMandante = Mandande.Campeonato.DerrotasEmCasa;
            var pontosVisitante = Visitante.Campeonato.DerrotasEmCasa;

            var vencedor = (pontosMandante < pontosVisitante)
                ? Mandande
                : (pontosVisitante < pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.DerrotasEmCasa, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularDerrotasForaDeCasa()
        {
            var pontosMandante = Mandande.Campeonato.DerrotasForaDeCasa;
            var pontosVisitante = Visitante.Campeonato.DerrotasForaDeCasa;

            var vencedor = (pontosMandante < pontosVisitante)
                ? Mandande
                : (pontosVisitante < pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.DerrotasForaCasa, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularAproveitamentoEmCasa()
        {
            var pontosMandante = Mandande.Campeonato.AproveitamentoEmCasa / 100;
            var pontosVisitante = Visitante.Campeonato.AproveitamentoEmCasa / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.AproveitamentoEmCasa, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private Indicador CalcularAproveitamentoForaDeCasa()
        {
            var pontosMandante = Mandande.Campeonato.AproveitamentoForaDeCasa / 100;
            var pontosVisitante = Visitante.Campeonato.AproveitamentoForaDeCasa / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.AproveitamentoForaDeCasa, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private Indicador CalcularAproveitamentoNoCampeonato()
        {
            var pontosMandante = Mandande.Campeonato.Aproveitamento / 100;
            var pontosVisitante = Visitante.Campeonato.Aproveitamento / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.AproveitamentoNoCampeonato, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private Indicador CalcularGolsPro()
        {
            var pontosMandante = Mandande.Campeonato.GolsPro;
            var pontosVisitante = Visitante.Campeonato.GolsPro;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.GolsPro, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularGolsContra()
        {
            var pontosMandante = Mandande.Campeonato.GolsContra;
            var pontosVisitante = Visitante.Campeonato.GolsContra;

            var vencedor = (pontosMandante < pontosVisitante)
                ? Mandande
                : (pontosVisitante < pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.GolsContra, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularSaldoDeGols()
        {
            var pontosMandante = Mandande.Campeonato.SaldoDeGol;
            var pontosVisitante = Visitante.Campeonato.SaldoDeGol;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.SaldoDeGols, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularMediaDaDefesa()
        {
            var pontosMandante = _jogadores.DoClube(Mandande).DaDefesa().Media(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.DoClube(Visitante).DaDefesa().Media(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.MediaDaDefesa, vencedor, pontosMandante, pontosVisitante);
        }

        private Indicador CalcularMediaDoMeioCampo()
        {
            var pontosMandante = _jogadores.DoClube(Mandande).DoMeioCampo().Media(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.DoClube(Visitante).DoMeioCampo().Media(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.MediaDaMeioCampo, vencedor, pontosMandante, pontosVisitante);
        }

        private Indicador CalcularMediaDoAtaque()
        {
            var pontosMandante = _jogadores.DoClube(Mandande).DoAtaque().Media(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.DoClube(Visitante).DoAtaque().Media(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.MediaDaAtaque, vencedor, pontosMandante, pontosVisitante);
        }

        //private ItemDeMedicaoDeConfronto CalcularMediaDoClube()
        //{
        //    var pontosMandante = _jogadores.DoClube(Mandande).Media(j => j.Pontuacao.Media);
        //    var pontosVisitante = _jogadores.DoClube(Visitante).Media(j => j.Pontuacao.Media);

        //    var vencedor = (pontosMandante > pontosVisitante)
        //        ? Mandande
        //        : (pontosVisitante > pontosMandante) ? Visitante : null;

        //    return new ItemDeMedicaoDeConfronto(TipoMedicao.MediaDoClube, vencedor, pontosMandante, pontosVisitante);
        //}

        private Indicador CalcularHistoricoDeVitoriasNoBrasileiro()
        {
            var confrontos = HistoricoDeJogos.GetHistoricoDeConfrontos(Mandande, Visitante).Where(j => j.Campeonato == TipoCampeonato.CampeonatoBrasileiro).ToList();

            var pontosMandante = confrontos.Count(j => j.Vencedor() == Mandande);
            var pontosVisitante = confrontos.Count(j => j.Vencedor() == Visitante);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.VitoriasEmConfrontosNoBrasileiro, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularHistoricoDeVitoriasNoConfronto()
        {
            var confrontos = HistoricoDeJogos.GetHistoricoDeConfrontos(Mandande, Visitante).ToList();

            var pontosMandante = confrontos.Count(j => j.Vencedor() == Mandande);
            var pontosVisitante = confrontos.Count(j => j.Vencedor() == Visitante);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.VitoriasEmTodosOsConfronto, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private Indicador CalcularVitoriasNaBrasileiro()
        {
            var totalDeJogosDoMandante = HistoricoDeJogos.GetHistoricoDeJogos(Mandande).Count(j => j.Campeonato == TipoCampeonato.CampeonatoBrasileiro);
            var totalDeJogosDoVisitante = HistoricoDeJogos.GetHistoricoDeJogos(Visitante).Count(j => j.Campeonato == TipoCampeonato.CampeonatoBrasileiro);

            var vitoriasDoMandante = HistoricoDeJogos.GetHistoricoDeJogos(Mandande).Count(j => j.Vencedor() == Mandande && j.Campeonato == TipoCampeonato.CampeonatoBrasileiro);
            var vitoriasDoVisitante = HistoricoDeJogos.GetHistoricoDeJogos(Visitante).Count(j => j.Vencedor() == Visitante && j.Campeonato == TipoCampeonato.CampeonatoBrasileiro);

            var pontosMandante = vitoriasDoMandante / (double)totalDeJogosDoMandante;
            var pontosVisitante = vitoriasDoVisitante / (double)totalDeJogosDoVisitante;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.VitoriasSobreJogosNoBrasileiro, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private Indicador CalcularVitoriasNaHistoriaDoClube()
        {
            var totalDeJogosDoMandante = HistoricoDeJogos.GetHistoricoDeJogos(Mandande).Count();
            var totalDeJogosDoVisitante = HistoricoDeJogos.GetHistoricoDeJogos(Visitante).Count();

            var vitoriasDoMandante = HistoricoDeJogos.GetHistoricoDeJogos(Mandande).Count(j => j.Vencedor() == Mandande);
            var vitoriasDoVisitante = HistoricoDeJogos.GetHistoricoDeJogos(Visitante).Count(j => j.Vencedor() == Visitante);

            var pontosMandante = vitoriasDoMandante / (double)totalDeJogosDoMandante;
            var pontosVisitante = vitoriasDoVisitante / (double)totalDeJogosDoVisitante;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new Indicador(TipoDeIndicador.VitoriasSobreJogosNaHistoriaDoClube, vencedor, pontosMandante, pontosVisitante, "P");
        }
    }
}
