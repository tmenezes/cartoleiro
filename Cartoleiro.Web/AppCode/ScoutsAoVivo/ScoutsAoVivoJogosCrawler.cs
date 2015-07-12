using System;
using System.Collections.Generic;
using Cartoleiro.Web.AppCode.Utils;
using Cartoleiro.Web.Models.ScoutsAoVivoModels;
using Newtonsoft.Json;

namespace Cartoleiro.Web.AppCode.ScoutsAoVivo
{
    public class ScoutsAoVivoJogosCrawler
    {
        public IEnumerable<string> CarregarDosJogos()
        {
            var idsJogos = new List<string>();

            var url = "http://scoutsaovivo.appspot.com/index.php";
            var html = HttpClientHelper.Get(url);

            var posicaoInicial = html.IndexOf(";confronto=\"", StringComparison.Ordinal) + 12;
            var posicaoFinal = html.Substring(posicaoInicial).IndexOf("\";", StringComparison.Ordinal);
            var confronto = html.Substring(posicaoInicial, posicaoFinal);

            var dataUrl = string.Format("http://scoutsaovivo.appspot.com/getdata.php?match={0}", confronto);
            var jsonPartidas = HttpClientHelper.Get(dataUrl);
            var scouts = JsonConvert.DeserializeObject<ScoutsData>(jsonPartidas);

            foreach (var match in scouts.FixtureMatches.Matches)
            {
                var idJogo = string.Format("{0}_{1}_{2}", match.Id, match.Home.Url, match.Away.Url);

                idsJogos.Add(idJogo);
            }

            return idsJogos;
        }
    }
}
