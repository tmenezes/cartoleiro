using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Extensions;

namespace Cartoleiro.Core.Confronto.Probabilidade
{
    public class ProbabilidadeDeResultado
    {
        public Jogo Jogo { get; private set; }
        public double PercentualDeVitoriaMandante { get; private set; }
        public double PercentualDeEmpate { get; private set; }
        public double PercentualDeVitoriaVisitante { get; private set; }

        public ProbabilidadeDeResultado(Jogo jogo)
        {
            Jogo = jogo;

            CalcularProbablidade();
        }

        private void CalcularProbablidade()
        {
            // na historia
            var confrontos = HistoricoDeJogos.GetHistoricoDeConfrontos(Jogo.Mandante, Jogo.Visitante)
                                             .ComoMandante(Jogo.Mandante)
                                             .ToList();

            var percentualVitoriasMandanteNaHistoria = confrontos.PercentualDeVitorias(Jogo.Mandante);
            var percentualVitoriasVisitanteNaHistoria = confrontos.PercentualDeVitorias(Jogo.Visitante);
            var percentualEmpatesNaHistoria = confrontos.PercentualDeEmpates();


            //// no brasileirao
            //var confrontosBrasileirao = confrontos.Where(j => j.Campeonato == TipoCampeonato.CampeonatoBrasileiro).ToList();

            //var percentualVitoriasMandanteNoBrasileirao = confrontosBrasileirao.PercentualDeVitorias(Jogo.Mandante);
            //var percentualVitoriasVisitanteNoBrasileirao = confrontosBrasileirao.PercentualDeVitorias(Jogo.Visitante);
            //var percentualEmpatesNoBrasileirao = confrontosBrasileirao.PercentualDeEmpates();


            // no campeonato atual
            var percentualVitoriasMandanteNoCampeonato = Jogo.Mandante.Campeonato.VitoriasEmCasa / (double)Jogo.Mandante.Campeonato.Jogos * 100;
            var percentualVitoriasVisitanteNoCampeonato = Jogo.Visitante.Campeonato.VitoriasForaDeCasa / (double)Jogo.Visitante.Campeonato.Jogos * 100;
            var percentualEmpatesNoCampeonato = ((Jogo.Mandante.Campeonato.EmpatesEmCasa / (double)Jogo.Mandante.Campeonato.Jogos * 100) +
                                                 (Jogo.Visitante.Campeonato.EmpatesForaDeCasa / (double)Jogo.Visitante.Campeonato.Jogos * 100)) / 2d;


            // totalizando...
            var totalVitoriasMandante = percentualVitoriasMandanteNaHistoria /*+ percentualVitoriasMandanteNoBrasileirao*/ + (percentualVitoriasMandanteNoCampeonato * 2);
            var totalVitoriasVisitante = percentualVitoriasVisitanteNaHistoria /*+ percentualVitoriasVisitanteNoBrasileirao*/ + (percentualVitoriasVisitanteNoCampeonato * 2);
            var totalEmpates = percentualEmpatesNaHistoria /*+ percentualEmpatesNoBrasileirao*/ + (percentualEmpatesNoCampeonato * 2);
            var totalGeral = totalVitoriasMandante + totalVitoriasVisitante + totalEmpates;

            this.PercentualDeVitoriaMandante = totalVitoriasMandante / totalGeral;
            this.PercentualDeVitoriaVisitante = totalVitoriasVisitante / totalGeral;
            this.PercentualDeEmpate = totalEmpates / totalGeral;
        }
    }
}
