using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Cartoleiro.Web.Models.ScoutsAoVivo;
using Newtonsoft.Json;

namespace Cartoleiro.Web.Controllers
{
    public class ScoutsAoVivoController : Controller
    {
        // GET: ScoutsAoVivo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ScoutsPartida(string idPartida)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://scoutsaovivo.appspot.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("getdata.php?match=" + idPartida).Result; //81_avai_gremio
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var scouts = JsonConvert.DeserializeObject<ScoutsData>(content);

                    return PartialView("_ScoutsPartida", scouts.ScoutsMatch);
                }

                return PartialView("_ScoutsPartida", null);
            }
        }
    }
}