using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Extensions;

namespace Cartoleiro.Core.Confronto.Probabilidade
{
    public class ProbabilidadeDeResultado
    {
        public Jogo Jogo { get; private set; }

        public double ProbabilidadeDeVitoriaMandante { get; private set; }
        public double ProbabilidadeDeEmpate { get; private set; }
        public double ProbabilidadeDeVitoriaVisitante { get; private set; }

        public int TotalDeVitoriasClubeMandante { get; set; }
        public int TotalDeVitoriasClubeVisitante { get; set; }
        public int TotalDeEmpates { get; set; }

        public int TotalDeVitoriasClubeMandanteComoMandante { get; set; }
        public int TotalDeVitoriasClubeVisitanteComoMandante { get; set; }
        public int TotalDeEmpatesComoMandante { get; set; }

        public int TotalDeVitoriasClubeMandanteComoVisitante { get; set; }
        public int TotalDeVitoriasClubeVisitanteComoVisitante { get; set; }
        public int TotalDeEmpatesComoVisitante { get; set; }



        public ProbabilidadeDeResultado(Jogo jogo)
        {
            Jogo = jogo;

            CalcularDados();
        }


        private void CalcularDados()
        {
            // confrontos gerais
            var confrontos = HistoricoDeJogos.GetHistoricoDeConfrontos(Jogo.Mandante, Jogo.Visitante).ToList();

            AtribuirVitorias(confrontos);
            CalcularProbablidade(confrontos);
        }

        private void AtribuirVitorias(List<Jogo> confrontos)
        {
            // confrontos gerais
            TotalDeVitoriasClubeMandante = confrontos.Count(j => j.Vencedor() == Jogo.Mandante);
            TotalDeVitoriasClubeVisitante = confrontos.Count(j => j.Vencedor() == Jogo.Visitante);
            TotalDeEmpates = confrontos.Count(j => j.Empate());


            // jogos do mandante em casa
            var confrontosMandanteEmCasa = confrontos.ComoMandante(Jogo.Mandante).ToList();

            TotalDeVitoriasClubeMandanteComoMandante = confrontosMandanteEmCasa.Count(j => j.Vencedor() == Jogo.Mandante);
            TotalDeVitoriasClubeVisitanteComoVisitante = confrontosMandanteEmCasa.Count(j => j.Vencedor() == Jogo.Visitante);
            TotalDeEmpatesComoMandante = confrontosMandanteEmCasa.Count(j => j.Empate());


            // jogos do visitante em casa
            var confrontosVisitanteEmCasa = confrontos.ComoMandante(Jogo.Visitante).ToList();

            TotalDeVitoriasClubeMandanteComoVisitante = confrontosVisitanteEmCasa.Count(j => j.Vencedor() == Jogo.Mandante);
            TotalDeVitoriasClubeVisitanteComoMandante = confrontosVisitanteEmCasa.Count(j => j.Vencedor() == Jogo.Visitante);
            TotalDeEmpatesComoVisitante = confrontosVisitanteEmCasa.Count(j => j.Empate());
        }

        private void CalcularProbablidade(List<Jogo> confrontos)
        {
            // confrontos gerais
            var percentualVitoriasGeraisMandante = confrontos.PercentualDeVitorias(Jogo.Mandante);
            var percentualVitoriasGeraisVisitante = confrontos.PercentualDeVitorias(Jogo.Visitante);
            var percentualGeraisEmpates = confrontos.PercentualDeEmpates();


            // na historia (obedecendo mando de campo)
            var confrontosPorMando = confrontos.ComoMandante(Jogo.Mandante).ToList();

            var percentualVitoriasPorMandoDoMandante = confrontosPorMando.PercentualDeVitorias(Jogo.Mandante);
            var percentualVitoriasPorMandoDoVisitante = confrontosPorMando.PercentualDeVitorias(Jogo.Visitante);
            var percentualEmpatesPorMando = confrontosPorMando.PercentualDeEmpates();


            // no campeonato atual
            var percentualVitoriasMandanteNoCampeonato = Jogo.Mandante.Campeonato.VitoriasEmCasa / (double)Jogo.Mandante.Campeonato.Jogos * 100;
            var percentualVitoriasVisitanteNoCampeonato = Jogo.Visitante.Campeonato.VitoriasForaDeCasa / (double)Jogo.Visitante.Campeonato.Jogos * 100;
            var percentualEmpatesNoCampeonato = ((Jogo.Mandante.Campeonato.EmpatesEmCasa / (double)Jogo.Mandante.Campeonato.Jogos * 100) +
                                                 (Jogo.Visitante.Campeonato.EmpatesForaDeCasa / (double)Jogo.Visitante.Campeonato.Jogos * 100)) / 2d;


            // totalizando...
            var totalVitoriasMandante = percentualVitoriasGeraisMandante + (percentualVitoriasPorMandoDoMandante * 2) + (percentualVitoriasMandanteNoCampeonato * 3);
            var totalVitoriasVisitante = percentualVitoriasGeraisVisitante + (percentualVitoriasPorMandoDoVisitante * 2) + (percentualVitoriasVisitanteNoCampeonato * 3);
            var totalEmpates = percentualGeraisEmpates + (percentualEmpatesPorMando * 2) + (percentualEmpatesNoCampeonato * 3);
            var totalGeral = totalVitoriasMandante + totalVitoriasVisitante + totalEmpates;

            this.ProbabilidadeDeVitoriaMandante = totalVitoriasMandante / totalGeral;
            this.ProbabilidadeDeVitoriaVisitante = totalVitoriasVisitante / totalGeral;
            this.ProbabilidadeDeEmpate = totalEmpates / totalGeral;
        }
    }
}
