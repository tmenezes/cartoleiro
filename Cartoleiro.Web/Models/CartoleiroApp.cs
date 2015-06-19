using System.Web;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.DAO;

namespace Cartoleiro.Web.Models
{
    public static class CartoleiroApp
    {
        public static ICartolaDataSource CartolaDataSource { get; private set; }
        public static Time TimeDeMaiorMedia { get; private set; }

        public static void Iniciar()
        {
            //CartolaDataSource = new CartolaDataSource();
            CartolaDataSource = new CartolaJsonDataSource(HttpContext.Current.Server.MapPath("~/App_Data"));

            EscalarMelhorTime();
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
