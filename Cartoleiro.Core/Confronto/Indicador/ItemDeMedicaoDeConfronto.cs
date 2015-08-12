using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Confronto.Indicador
{
    public class ItemDeMedicaoDeConfronto
    {
        private const string MEDIDOR_PONTOS_CAMPEONATO = "Pontos no campeonato";
        private const string MEDIDOR_PONTOS_5_JOGOS = "Pontos nos �ltimos 5 jogos";
        private const string MEDIDOR_VITORIAS_CASA = "Vit�rias em casa no campeonato";
        private const string MEDIDOR_VITORIAS_FORA = "Vit�rias fora no campeonato";
        private const string MEDIDOR_DERROTAS_CASA = "Derrotas em casa no campeonato";
        private const string MEDIDOR_DERROTAS_FORA = "Derrotas fora no campeonato";
        private const string MEDIDOR_APROVEITAMENTO_CASA = "Aproveitamento em casa";
        private const string MEDIDOR_APROVEITAMENTO_FORA = "Aproveitamento fora de casa";
        private const string MEDIDOR_APROVEITAMENTO = "Aproveitamento no campeonato";
        private const string MEDIDOR_GOLS_PRO = "Gols pr�";
        private const string MEDIDOR_GOLS_CONTRA = "Gols contra";
        private const string MEDIDOR_SALDO_GOL = "Saldo de gols";
        private const string MEDIDOR_MEDIA_DEFESA = "Pontua��o m�dia da defesa";
        private const string MEDIDOR_MEDIA_MEIOCAMPO = "Pontua��o m�dia do meio campo";
        private const string MEDIDOR_MEDIA_ATAQUE = "Pontua��o m�dia do ataque";
        private const string MEDIDOR_MEDIA_CLUBE = "Pontua��o m�dia dos jogadores";
        private const string MEDIDOR_CONFRONTO_BRASILEIRO = "Vit�rias confrontos no Brasileir�o";
        private const string MEDIDOR_CONFRONTO_TODOS = "Vit�rias em todos os confrontos";
        private const string MEDIDOR_VITORIAS_BRASILEIRO = "% Vit�rias / jogos no Brasileir�o";
        private const string MEDIDOR_VITORIAS_HISTORIA = "% Vit�rias / jogos na hist�ria";

        public TipoMedicao TipoMedicao { get; private set; }
        public string Descricao { get; private set; }
        public Clube Vencedor { get; private set; }
        public double ResultadoMandante { get; private set; }
        public double ResultadoVisitante { get; private set; }
        public string Formatacao { get; private set; }


        public ItemDeMedicaoDeConfronto(TipoMedicao tipoMedicao, Clube vencedor, double resultadoMandante, double resultadoVisitante)
            : this(tipoMedicao, vencedor, resultadoMandante, resultadoVisitante, "N")
        {
        }

        public ItemDeMedicaoDeConfronto(TipoMedicao tipoMedicao, Clube vencedor, double resultadoMandante, double resultadoVisitante, string formatacao)
        {
            TipoMedicao = tipoMedicao;
            Descricao = ObterDescricao();
            Vencedor = vencedor;
            ResultadoMandante = resultadoMandante;
            ResultadoVisitante = resultadoVisitante;
            Formatacao = formatacao;
        }


        private string ObterDescricao()
        {
            switch (TipoMedicao)
            {
                case TipoMedicao.PontosNoCampeonato:
                    return MEDIDOR_PONTOS_CAMPEONATO;

                case TipoMedicao.PontosNosUltimos5Jogos:
                    return MEDIDOR_PONTOS_5_JOGOS;

                case TipoMedicao.VitoriasEmCasa:
                    return MEDIDOR_VITORIAS_CASA;

                case TipoMedicao.VitoriasForaDeCasa:
                    return MEDIDOR_VITORIAS_FORA;

                case TipoMedicao.DerrotasEmCasa:
                    return MEDIDOR_DERROTAS_CASA;

                case TipoMedicao.DerrotasForaCasa:
                    return MEDIDOR_DERROTAS_FORA;

                case TipoMedicao.AproveitamentoEmCasa:
                    return MEDIDOR_APROVEITAMENTO_CASA;

                case TipoMedicao.AproveitamentoForaDeCasa:
                    return MEDIDOR_APROVEITAMENTO_FORA;

                case TipoMedicao.AproveitamentoNoCampeonato:
                    return MEDIDOR_APROVEITAMENTO;

                case TipoMedicao.GolsPro:
                    return MEDIDOR_GOLS_PRO;

                case TipoMedicao.GolsContra:
                    return MEDIDOR_GOLS_CONTRA;

                case TipoMedicao.SaldoDeGols:
                    return MEDIDOR_SALDO_GOL;

                case TipoMedicao.MediaDaDefesa:
                    return MEDIDOR_MEDIA_DEFESA;

                case TipoMedicao.MediaDaMeioCampo:
                    return MEDIDOR_MEDIA_MEIOCAMPO;

                case TipoMedicao.MediaDaAtaque:
                    return MEDIDOR_MEDIA_ATAQUE;

                //case TipoMedicao.MediaDoClube:
                //    return MEDIDOR_MEDIA_CLUBE;

                case TipoMedicao.VitoriasEmConfrontosNoBrasileiro:
                    return MEDIDOR_CONFRONTO_BRASILEIRO;

                case TipoMedicao.VitoriasEmTodosOsConfronto:
                    return MEDIDOR_CONFRONTO_TODOS;

                case TipoMedicao.VitoriasSobreJogosNoBrasileiro:
                    return MEDIDOR_VITORIAS_BRASILEIRO;

                case TipoMedicao.VitoriasSobreJogosNaHistoriaDoClube:
                    return MEDIDOR_VITORIAS_HISTORIA;

                default:
                    return MEDIDOR_PONTOS_CAMPEONATO;
            }
        }


        public override string ToString()
        {
            return string.Format("Mandante {0} - {1} Visitante ({2})", ResultadoMandante, ResultadoVisitante, Descricao);
        }
    }
}