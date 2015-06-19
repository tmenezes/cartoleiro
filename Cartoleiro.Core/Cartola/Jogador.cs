namespace Cartoleiro.Core.Cartola
{
    public class Jogador
    {
        public string Nome { get; set; }
        public Clube Clube { get; set; }
        public Posicao Posicao { get; set; }
        public Preco Preco { get; set; }
        public Pontuacao Pontuacao { get; set; }

        public Jogador(string nome, Clube clube, Posicao posicao)
        {
            Nome = nome;
            Clube = clube;
            Posicao = posicao;
        }

        public override string ToString()
        {
            return string.Format("Nome: {0}, Pontuacao: {1}, Preco: {2}", Nome, Pontuacao, Preco);
        }
    }
}
