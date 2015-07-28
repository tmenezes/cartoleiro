
using System;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Core.Data
{
    public class CartolaData
    {
        private static bool _iniciado = false;
        private static ICartolaDataSource _cartolaDataSource;

        public static ICartolaDataSource CartolaDataSource
        {
            get
            {
                if (!_iniciado)
                    throw new InvalidOperationException("CartolaData não foi inicializado.");

                return _cartolaDataSource;
            }
            private set { _cartolaDataSource = value; }
        }

        public static void Iniciar(ICartolaDataSource cartolaDataSource)
        {
            CartolaDataSource = cartolaDataSource;

            HistoricoDeJogos.Carregar(cartolaDataSource);

            _iniciado = true;
        }
    }
}
