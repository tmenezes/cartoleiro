using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Confronto.Indicador
{
    public class Indicador
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

        public TipoDeIndicador TipoDeIndicador { get; private set; }
        public string Descricao { get; private set; }
        public Clube Vencedor { get; private set; }
        public double ResultadoMandante { get; private set; }
        public double ResultadoVisitante { get; private set; }
        public string Formatacao { get; private set; }


        public Indicador(TipoDeIndicador tipoDeIndicador, Clube vencedor, double resultadoMandante, double resultadoVisitante)
            : this(tipoDeIndicador, vencedor, resultadoMandante, resultadoVisitante, "N")
        {
        }

        public Indicador(TipoDeIndicador tipoDeIndicador, Clube vencedor, double resultadoMandante, double resultadoVisitante, string formatacao)
        {
            TipoDeIndicador = tipoDeIndicador;
            Descricao = ObterDescricao();
            Vencedor = vencedor;
            ResultadoMandante = resultadoMandante;
            ResultadoVisitante = resultadoVisitante;
            Formatacao = formatacao;
        }


        private string ObterDescricao()
        {
            switch (TipoDeIndicador)
            {
                case TipoDeIndicador.PontosNoCampeonato:
                    return MEDIDOR_PONTOS_CAMPEONATO;

                case TipoDeIndicador.PontosNosUltimos5Jogos:
                    return MEDIDOR_PONTOS_5_JOGOS;

                case TipoDeIndicador.VitoriasEmCasa:
                    return MEDIDOR_VITORIAS_CASA;

                case TipoDeIndicador.VitoriasForaDeCasa:
                    return MEDIDOR_VITORIAS_FORA;

                case TipoDeIndicador.DerrotasEmCasa:
                    return MEDIDOR_DERROTAS_CASA;

                case TipoDeIndicador.DerrotasForaCasa:
                    return MEDIDOR_DERROTAS_FORA;

                case TipoDeIndicador.AproveitamentoEmCasa:
                    return MEDIDOR_APROVEITAMENTO_CASA;

                case TipoDeIndicador.AproveitamentoForaDeCasa:
                    return MEDIDOR_APROVEITAMENTO_FORA;

                case TipoDeIndicador.AproveitamentoNoCampeonato:
                    return MEDIDOR_APROVEITAMENTO;

                case TipoDeIndicador.GolsPro:
                    return MEDIDOR_GOLS_PRO;

                case TipoDeIndicador.GolsContra:
                    return MEDIDOR_GOLS_CONTRA;

                case TipoDeIndicador.SaldoDeGols:
                    return MEDIDOR_SALDO_GOL;

                case TipoDeIndicador.MediaDaDefesa:
                    return MEDIDOR_MEDIA_DEFESA;

                case TipoDeIndicador.MediaDaMeioCampo:
                    return MEDIDOR_MEDIA_MEIOCAMPO;

                case TipoDeIndicador.MediaDaAtaque:
                    return MEDIDOR_MEDIA_ATAQUE;

                //case TipoMedicao.MediaDoClube:
                //    return MEDIDOR_MEDIA_CLUBE;

                case TipoDeIndicador.VitoriasEmConfrontosNoBrasileiro:
                    return MEDIDOR_CONFRONTO_BRASILEIRO;

                case TipoDeIndicador.VitoriasEmTodosOsConfronto:
                    return MEDIDOR_CONFRONTO_TODOS;

                case TipoDeIndicador.VitoriasSobreJogosNoBrasileiro:
                    return MEDIDOR_VITORIAS_BRASILEIRO;

                case TipoDeIndicador.VitoriasSobreJogosNaHistoriaDoClube:
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