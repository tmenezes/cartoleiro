using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.DAO;

namespace Cartoleiro.Web.AppCode
{
    public static class CartoleiroApp
    {
        public static ICartolaDataSource CartolaDataSource { get; private set; }
        public static Time TimeDeMaiorMedia { get; private set; }

        public static void Iniciar()
        {
            CartolaDataSource = new CartolaJsonDataSource(HttpContext.Current.Server.MapPath("~/App_Data"));

            EscalarMelhorTime();
        }

        public static void AtualizarDataSource(ICartolaDataSource cartolaDataSource)
        {
            lock (CartolaDataSource)
            {
                CartolaDataSource = cartolaDataSource;
            }
        }
        

        private static void EscalarMelhorTime()
        {
            var escalador = new EscaladorDeTime(CartolaDataSource)
                .ComPatrimonio(double.MaxValue)
                .ComAnalisadores(new AnalisadorBuilder().PontuacaoMedia().Analisadores);

            TimeDeMaiorMedia = escalador.MontarTime();
        }
    }
}
