using System.ComponentModel.DataAnnotations;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.Models.EscaladorModels
{
    public class EscaladorViewModel
    {
        [Required]
        public EsquemaTatico EsquemaTatico { get; set; }

        [Required]
        public double Patrimonio { get; set; }

        public Posicao? PosicaoEmFoco { get; set; }

        public bool ProporcionalNaPosicao { get; set; }

        // analisazdores
        public bool AnalisadorPontuacaoMedia { get; set; }
        public bool AnalisadorUltimaPontuacao { get; set; }
        public bool AnalisadorPontosNoCampeonato { get; set; }
        public bool AnalisadorVitorias { get; set; }
        public bool AnalisadorGolsPro { get; set; }
        public bool AnalisadorGolsContra { get; set; }
        public bool AnalisadorSaldoDeGols { get; set; }
        public bool AnalisadorUltimos5Jogos { get; set; }

        public EscaladorViewModel()
        {
            EsquemaTatico = EsquemaTatico._442;
            Patrimonio = 100;
            ProporcionalNaPosicao = true;

            AnalisadorPontuacaoMedia = true;
        }
    }
}
