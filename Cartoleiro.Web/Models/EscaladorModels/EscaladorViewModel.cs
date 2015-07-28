using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Web.AppCode.MvcHelpers;

namespace Cartoleiro.Web.Models.EscaladorModels
{
    public class EscaladorViewModel
    {
        [Required]
        [DisplayName("Esquema tático")]
        public EsquemaTatico EsquemaTatico { get; set; }

        [Required]
        [DisplayName("Patrimônio")]
        public double Patrimonio { get; set; }

        [DisplayName("Posição em foco")]
        public Posicao? PosicaoEmFoco { get; set; }

        [DisplayName("Média maior que")]
        public double? MediaMaiorQue { get; set; }

        [DisplayName("Qtde Jogos maior que")]
        public int? JogosMaiorQue { get; set; }

        public bool ProporcionalNaPosicao { get; set; }
        public bool SomenteProvaveis { get; set; }

        // analisadores de jogador
        public bool AnalisadorPontuacaoMedia { get; set; }
        public bool AnalisadorUltimaPontuacao { get; set; }
        public bool AnalisadorScoutsPorPosicao { get; set; }
        public bool AnalisadorScoutsPositivos { get; set; }
        public bool AnalisadorScoutsNegativos { get; set; }

        // analisadores de campeonato
        public bool AnalisadorUltimos5Jogos { get; set; }
        public bool AnalisadorPontosNoCampeonato { get; set; }
        public bool AnalisadorVitorias { get; set; }
        public bool AnalisadorGolsPro { get; set; }
        public bool AnalisadorGolsContra { get; set; }
        public bool AnalisadorSaldoDeGols { get; set; }

        // analisadores de confronto
        public bool AnalisadorPesoDoClube { get; set; }
        public bool AnalisadorAproveitamentoPorMando { get; set; }
        public bool AnalisadorGolsProPorMando { get; set; }
        public bool AnalisadorHistoricoNoConfronto { get; set; }


        public EscaladorViewModel()
        {
            EsquemaTatico = EsquemaTatico._442;
            Patrimonio = 999;
            MediaMaiorQue = ViewModelHelper.MaiorMedia / 2;
            JogosMaiorQue = ViewModelHelper.MaiorQtdeJogos / 2;
            ProporcionalNaPosicao = true;
            SomenteProvaveis = true;

            AnalisadorPontuacaoMedia = true;
            AnalisadorUltimos5Jogos = true;
            AnalisadorPesoDoClube = true;
        }
    }
}
