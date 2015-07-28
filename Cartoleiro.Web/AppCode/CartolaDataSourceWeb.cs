using System.Collections.Generic;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;

namespace Cartoleiro.Web.AppCode
{
    internal class CartolaDataSourceWeb : ICartolaDataSource
    {
        public IEnumerable<Clube> Clubes { get; private set; }
        public IEnumerable<Jogador> Jogadores { get; private set; }
        public IEnumerable<Rodada> Rodadas { get; private set; }
        public IEnumerable<Jogo> HistoricoDeJogos { get; private set; }

        public CartolaDataSourceWeb(ICartolaDataSource cartolaDataSourceAntigo, IEnumerable<Jogador> jogadores)
        {
            Clubes = cartolaDataSourceAntigo.Clubes;
            Jogadores = jogadores;
            Rodadas = cartolaDataSourceAntigo.Rodadas;
            HistoricoDeJogos = cartolaDataSourceAntigo.HistoricoDeJogos;

            CartolaData.Iniciar(this);
        }
    }
}