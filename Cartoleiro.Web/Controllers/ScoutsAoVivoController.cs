using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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

        public async Task<ActionResult> ScoutsPartida(string idPartida)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://scoutsaovivo.appspot.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("getdata.php?match=" + idPartida); //81_avai_gremio
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var scouts = JsonConvert.DeserializeObject<ScoutsData>(content);

                    return View(scouts);
                }
                
                return View();
            }
        }
    }
}