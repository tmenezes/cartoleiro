using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Cartoleiro.Core.Cartola;
using Cartoleiro.Core.Data;
using Cartoleiro.Core.Escalador;
using Cartoleiro.Core.Escalador.Analizador;
using Cartoleiro.DAO;
using Cartoleiro.Web.AppCode.Extensions;
using WebGrease.Activities;

namespace Cartoleiro.Web.AppCode
{
    public static class CartoleiroApp
    {
        private static Dictionary<Clube, string> _descricaoDosClubes;
        public static ICartolaDataSource CartolaDataSource { get; private set; }
        public static Time TimeDeMaiorMedia { get; private set; }

        public static void Iniciar()
        {
            CartolaDataSource = new CartolaJsonDataSource(HttpContext.Current.Server.MapPath("~/App_Data"));

            CarregarDescricaoDosClubes();

            EscalarMelhorTime();
        }

        public static void AtualizarDataSource(ICartolaDataSource cartolaDataSource)
        {
            lock (CartolaDataSource)
            {
                CartolaDataSource = cartolaDataSource;
            }
        }

        public static string GetDescricaoDoClube(Clube clube)
        {
            return _descricaoDosClubes.ContainsKey(clube)
                ? _descricaoDosClubes[clube]
                : string.Empty;
        }


        private static void CarregarDescricaoDosClubes()
        {
            var appData = HttpContext.Current.Server.MapPath("~/App_Data/Clubes");
            var descricoes = new Dictionary<Clube, string>();

            foreach (var clube in CartolaDataSource.Clubes)
            {
                var arquivo = Path.Combine(appData, string.Concat(clube.GetNomeNormalizado(), ".txt"));
                using (var reader = new StreamReader(arquivo, Encoding.Default))
                {
                    descricoes.Add(clube, reader.ReadToEnd());
                }
            }

            _descricaoDosClubes = descricoes;
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
