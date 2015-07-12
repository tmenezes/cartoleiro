using System;
using System.Collections.Generic;
using System.Linq;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Escalador
{
    internal static class EscaladorHelper
    {
        private static readonly Dictionary<Posicao, double> _valoresMinimos = new Dictionary<Posicao, double>();

        public static double GetValorMinimo(Posicao posicao)
        {
            if (!_valoresMinimos.Any())
            {
                AnalisarValoresMinimos();
            }

            return _valoresMinimos[posicao];
        }

        private static void AnalisarValoresMinimos()
        {
            var posicoes = Enum.GetValues(typeof(Posicao)).OfType<Posicao>();
            var jogadores = EscaladorDeTime.CartolaDS.Jogadores;

            foreach (var posicao in posicoes)
            {
                var posicaoCopia = posicao;
                var precoMinimo = jogadores.Where(j => j.Posicao == posicaoCopia)
                                           .OrderBy(j => j.Preco.Atual)
                                           .Take(5)
                                           .Average(j => j.Preco.Atual);

                _valoresMinimos.Add(posicaoCopia, precoMinimo);
            }
        }
    }
}