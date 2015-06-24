namespace Cartoleiro.Core.Cartola
{
    public class Preco
    {
        public double Atual { get; set; }
        public double Variacao { get; set; }

        public Preco(double atual, double variacao)
        {
            Atual = atual;
            Variacao = variacao;
        }

        public override string ToString()
        {
            return string.Format("Atual: {0}, Var: {1}", Atual, Variacao);
        }
    }
}