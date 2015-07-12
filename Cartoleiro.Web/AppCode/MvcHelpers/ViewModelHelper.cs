using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cartoleiro.Core.Cartola;

namespace Cartoleiro.Web.AppCode.MvcHelpers
{
    public static class ViewModelHelper
    {
        public static int MaiorMedia { get; private set; }
        public static int MaiorQtdeJogos { get; private set; }

        static ViewModelHelper()
        {
            MaiorMedia = CalcularMaiorMedia();
            MaiorQtdeJogos = CalcularMaiorQuantidadeDeJogos();
        }


        public static IEnumerable<SelectListItem> EsquemasTaticos
        {
            get
            {
                return new List<SelectListItem>()
                       {
                            new SelectListItem { Value = ((int)EsquemaTatico._343).ToString(), Text = "3-4-3" },
                            new SelectListItem { Value = ((int)EsquemaTatico._352).ToString(), Text = "3-5-2" },
                            new SelectListItem { Value = ((int)EsquemaTatico._433).ToString(), Text = "4-3-3" },
                            new SelectListItem { Value = ((int)EsquemaTatico._442).ToString(), Text = "4-4-2" },
                            new SelectListItem { Value = ((int)EsquemaTatico._451).ToString(), Text = "4-5-1" },
                            new SelectListItem { Value = ((int)EsquemaTatico._532).ToString(), Text = "5-3-2" },
                            new SelectListItem { Value = ((int)EsquemaTatico._541).ToString(), Text = "5-4-1" },
                       };
            }
        }

        public static IEnumerable<SelectListItem> Posicoes
        {
            get
            {
                var posicoes = Enum.GetValues(typeof(Posicao))
                                   .OfType<Posicao>()
                                   .Select(i => new SelectListItem()
                                                {
                                                    Value = i.ToString(),
                                                    Text = i.ToString()
                                                });
                return posicoes;
            }
        }

        public static IEnumerable<SelectListItem> LimitesDeMedia
        {
            get
            {
                var medias = Enumerable.Range(1, MaiorMedia)
                                       .Select(i => new SelectListItem()
                                       {
                                           Value = i.ToString(),
                                           Text = i.ToString()
                                       });
                return medias;
            }
        }

        public static IEnumerable<SelectListItem> LimitesDeJogos
        {
            get
            {
                var medias = Enumerable.Range(1, MaiorQtdeJogos)
                                       .Select(i => new SelectListItem()
                                       {
                                           Value = i.ToString(),
                                           Text = i.ToString()
                                       });
                return medias;
            }
        }


        private static int CalcularMaiorQuantidadeDeJogos()
        {
            return CalcularMaiorIndicador(j => j.Jogos);
        }

        private static int CalcularMaiorMedia()
        {
            return CalcularMaiorIndicador(j => j.Pontuacao.Media);
        }

        private static int CalcularMaiorIndicador(Func<Jogador, double> funcaoDeAnalise)
        {
            var posicoes = Enum.GetValues(typeof(Posicao)).OfType<Posicao>();
            var mediasPorPosicoes = posicoes.Select(p => new
            {
                Posicao = p,
                Indicador = CartoleiroApp.CartolaDataSource.Jogadores.Where(j => j.Posicao == p)
                                                                     .Max(funcaoDeAnalise)
            });

            return (int)Math.Floor(mediasPorPosicoes.Min(i => i.Indicador));
        }
    }
}
