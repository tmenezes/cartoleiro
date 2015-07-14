namespace Cartoleiro.Core.Cartola
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Clube Clube { get; set; }
        public Posicao Posicao { get; set; }
        public Preco Preco { get; set; }
        public Pontuacao Pontuacao { get; set; }
        public int Jogos { get; set; }
        public Status Status { get; set; }
        public Scouts Scouts { get; set; }

        public Jogador(string nome, Clube clube, Posicao posicao)
        {
            Nome = nome;
            Clube = clube;
            Posicao = posicao;

            Status = Status.Provavel;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}/{4}] - Pts: {2}, C$: {3}", Nome, Clube.Nome, Pontuacao, Preco.Atual, Status);
        }
    }
}
