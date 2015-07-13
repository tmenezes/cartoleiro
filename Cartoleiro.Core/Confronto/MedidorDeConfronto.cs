using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;

namespace Cartoleiro.Core.Confronto
{
    public class MedidorDeConfronto
    {
        private const string MEDIDOR_PONTOS_CAMPEONATO = "Pontos no campeonato";
        private const string MEDIDOR_APROVEITAMENTO_CASA = "Aproveitamento em casa";
        private const string MEDIDOR_APROVEITAMENTO_FORA = "Aproveitamento fora de casa";
        private const string MEDIDOR_SALDO_GOL = "Saldo de gols";
        private const string MEDIDOR_MEDIA_DEFESA = "Pontuação média da defesa";
        private const string MEDIDOR_MEDIA_MEIOCAMPO = "Pontuação média do meio campo";
        private const string MEDIDOR_MEDIA_ATAQUE = "Pontuação média do ataque";
        private const string MEDIDOR_MEDIA_CLUBE = "Pontuação média dos jogadores";

        private IEnumerable<Jogador> _jogadores;

        public Clube Mandande { get; set; }
        public Clube Visitante { get; set; }
        public ICartolaDataSource CartolaDataSource { get; set; }


        public MedidorDeConfronto(Clube mandande, Clube visitante, ICartolaDataSource cartolaDataSource)
        {
            Mandande = mandande;
            Visitante = visitante;
            CartolaDataSource = cartolaDataSource;

            _jogadores = CartolaDataSource.Jogadores.Where(j => j.Status == Status.Provavel || j.Status == Status.Duvida).ToList();
        }

        public ResultadoDoConfronto MedirConfronto()
        {
            var result = new ResultadoDoConfronto(Mandande, Visitante);

            result.AdicionarItemDeMedicao(MedirPosicaoNoCampeonato())
                  .AdicionarItemDeMedicao(MedirAproveitamentoEmCasa())
                  .AdicionarItemDeMedicao(MedirAproveitamentoForaDeCasa())
                  .AdicionarItemDeMedicao(MedirSaldoDeGols())
                  .AdicionarItemDeMedicao(MedirMediaDaDefesa())
                  .AdicionarItemDeMedicao(MedirMediaDoMeioCampo())
                  .AdicionarItemDeMedicao(MedirMediaDoAtaque())
                  .AdicionarItemDeMedicao(MedirMediaDoClube());

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

            return new ItemDeMedicaoDeConfronto(MEDIDOR_PONTOS_CAMPEONATO, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirAproveitamentoEmCasa()
        {
            var pontosMandante = Mandande.Campeonato.AproveitamentoEmCasa / 100;
            var pontosVisitante = Visitante.Campeonato.AproveitamentoEmCasa / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(MEDIDOR_APROVEITAMENTO_CASA, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private ItemDeMedicaoDeConfronto MedirAproveitamentoForaDeCasa()
        {
            var pontosMandante = Mandande.Campeonato.AproveitamentoForaDeCasa / 100;
            var pontosVisitante = Visitante.Campeonato.AproveitamentoForaDeCasa / 100;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(MEDIDOR_APROVEITAMENTO_FORA, vencedor, pontosMandante, pontosVisitante, "P");
        }

        private ItemDeMedicaoDeConfronto MedirSaldoDeGols()
        {
            var pontosMandante = Mandande.Campeonato.SaldoDeGol;
            var pontosVisitante = Visitante.Campeonato.SaldoDeGol;

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(MEDIDOR_SALDO_GOL, vencedor, pontosMandante, pontosVisitante, "G");
        }

        private ItemDeMedicaoDeConfronto MedirMediaDaDefesa()
        {
            var pontosMandante = _jogadores.Where(j => j.Clube == Mandande && (j.Posicao == Posicao.Zagueiro || j.Posicao == Posicao.Lateral)).Average(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante && (j.Posicao == Posicao.Zagueiro || j.Posicao == Posicao.Lateral)).Average(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(MEDIDOR_MEDIA_DEFESA, vencedor, pontosMandante, pontosVisitante);
        }

        private ItemDeMedicaoDeConfronto MedirMediaDoMeioCampo()
        {
            var pontosMandante = _jogadores.Where(j => j.Clube == Mandande && j.Posicao == Posicao.MeioCampo).Average(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante && j.Posicao == Posicao.MeioCampo).Average(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(MEDIDOR_MEDIA_MEIOCAMPO, vencedor, pontosMandante, pontosVisitante);
        }

        private ItemDeMedicaoDeConfronto MedirMediaDoAtaque()
        {
            try
            {
                var pontosMandante = _jogadores.Where(j => j.Clube == Mandande && j.Posicao == Posicao.Atacante).Average(j => j.Pontuacao.Media);
                var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante && j.Posicao == Posicao.Atacante).Average(j => j.Pontuacao.Media);

                var vencedor = (pontosMandante > pontosVisitante)
                    ? Mandande
                    : (pontosVisitante > pontosMandante) ? Visitante : null;

                return new ItemDeMedicaoDeConfronto(MEDIDOR_MEDIA_ATAQUE, vencedor, pontosMandante, pontosVisitante);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private ItemDeMedicaoDeConfronto MedirMediaDoClube()
        {
            var pontosMandante = _jogadores.Where(j => j.Clube == Mandande).Average(j => j.Pontuacao.Media);
            var pontosVisitante = _jogadores.Where(j => j.Clube == Visitante).Average(j => j.Pontuacao.Media);

            var vencedor = (pontosMandante > pontosVisitante)
                ? Mandande
                : (pontosVisitante > pontosMandante) ? Visitante : null;

            return new ItemDeMedicaoDeConfronto(MEDIDOR_MEDIA_CLUBE, vencedor, pontosMandante, pontosVisitante);
        }
    }
}
