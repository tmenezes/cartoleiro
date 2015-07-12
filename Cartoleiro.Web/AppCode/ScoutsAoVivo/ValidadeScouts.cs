using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Cartoleiro.Web.AppCode.ScoutsAoVivo
{
    internal class ValidadeScouts
    {
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataProximaAtualizacao { get; set; }

        public bool DentroDaValidade { get { return DateTime.Now < DataAtualizacao.AddSeconds(60); } }
        public bool DeveAtualizar { get { return DateTime.Now > DataProximaAtualizacao; } }

        public ValidadeScouts()
        {
            DataAtualizacao = DateTime.MinValue;
            DataProximaAtualizacao = DateTime.MaxValue;
        }

        public void Atualizar(DateTime proximaAtualizacao)
        {
            DataAtualizacao = DateTime.Now;
            DataProximaAtualizacao = proximaAtualizacao;
        }
    }
}
