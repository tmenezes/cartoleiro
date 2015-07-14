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

        public int SemGolSofrido { get; set; }
        public int DefesasDificeis { get; set; }
        public int DefesasDePenaltis { get; set; }
        public int GolsSofridos { get; set; }

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

        public void SetScout(string nome, int quantidade)
        {
            switch (nome)
            {
                case "FS": FaltasSofridas = quantidade; break;
                case "A": Assistencias = quantidade; break;
                case "FT": FinalizacoesNaTrave = quantidade; break;
                case "FD": FinalizacoesDefendidas = quantidade; break;
                case "FF": FinalizacoesFora = quantidade; break;
                case "G": Gols = quantidade; break;
                case "RB": RoubadasDeBola = quantidade; break;

                case "PE": PassesErrados = quantidade; break;
                case "I": Impedimentos = quantidade; break;
                case "PP": PenaltisPerdidos = quantidade; break;
                case "FC": FaltasCometidas = quantidade; break;
                case "GC": GolsContra = quantidade; break;
                case "CA": CartoesAmarelo = quantidade; break;
                case "CV": CartoesVermelho = quantidade; break;

                case "SG": SemGolSofrido = quantidade; break;
                case "DD": DefesasDificeis = quantidade; break;
                case "DP": DefesasDePenaltis = quantidade; break;
                case "GS": GolsSofridos = quantidade; break;
            }
        }
    }
}