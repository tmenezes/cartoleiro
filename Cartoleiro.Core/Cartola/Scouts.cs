namespace Cartoleiro.Core.Cartola
{
    public class Scouts
    {
        // positivas
        public int FaltasSofridas { get; set; }
        public int Assistencias { get; set; }
        public int FinalizacoesNaTrave { get; set; }
        public int FinalizacoesDefendidas { get; set; }
        public int FinalizacoesFora { get; set; }
        public int Gols { get; set; }
        public int RoubadasDeBola { get; set; }

        // negativas
        public int PassesErrados { get; set; }
        public int Impedimentos { get; set; }
        public int PenaltisPerdidos { get; set; }
        public int FaltasCometidas { get; set; }
        public int GolsContra { get; set; }
        public int CartoesAmarelo { get; set; }
        public int CartoesVermelho { get; set; }

        public int TotalDePositivos
        {
            get
            {
                return FaltasSofridas + Assistencias + FinalizacoesNaTrave +
                       FinalizacoesDefendidas + FinalizacoesFora + Gols + RoubadasDeBola;
            }
        }
        public int TotalDeNegativos
        {
            get
            {
                return PassesErrados + Impedimentos + PenaltisPerdidos +
                       FaltasCometidas + GolsContra + CartoesAmarelo + CartoesVermelho;
            }
        }
    }
}