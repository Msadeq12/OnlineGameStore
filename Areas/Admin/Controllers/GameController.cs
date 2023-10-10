using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Text;

namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private static HttpClient client;

        public GameController()
        {
             client = new HttpClient();
        }

        /// <summary>
        /// Calls GetAllGames() from the GameService API
        /// </summary>
        /// <returns>A List of Games from GameService DB</returns>
        [Route("[area]/[controller]")]
        public IActionResult Index()
        {

            string url = "https://localhost:7108/api/game";

            HttpResponseMessage response = client.GetAsync(url).Result;

            List<Game>? games;

            if (response.IsSuccessStatusCode)
            {
                games = response.Content.ReadFromJsonAsync<List<Game>>().Result;
            }

            else
            {
                games = null;
            }

            return View(games);
        }

        /// <summary>
        /// the Add method is also calling the API for getting all genres,
        /// similar to the Edit get method.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            string url = "https://localhost:7108/api/game/genres";
            HttpResponseMessage response = client.GetAsync(url).Result;
            List<Genre>? genres;

            if (response.IsSuccessStatusCode)
            {
                genres = response.Content.ReadFromJsonAsync<List<Genre>>().Result;
            }

            else
            {
                genres = null;
            }

            ViewBag.Genres = genres;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Game game)
        {
            string url = "https://localhost:7108/api/game";
            var jsonObject = JsonConvert.SerializeObject(game);

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }

        }

        /// <summary>
        /// The Edit controller method is calling the API twice; once for SelectGameById and
        /// the other for GetAllGenres.
        /// </summary>
        /// <param name="id">id of the specific game from the Index table</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";
            string urlGenre = "https://localhost:7108/api/game/genres";

            //this is a response for getting a specific game through API
            //passed through the View 
            HttpResponseMessage response = client.GetAsync(url).Result;
            Game? game;

            if (response.IsSuccessStatusCode)
            {
                game = response.Content.ReadFromJsonAsync<Game>().Result;
            }

            else
            {
                game = null;
            }

            // this is a response for getting all genres through API
            //response passed on through ViewBag
            HttpResponseMessage responseGenre = client.GetAsync(urlGenre).Result;
            List<Genre>? genres;

            if (response.IsSuccessStatusCode)
            {
                genres = responseGenre.Content.ReadFromJsonAsync<List<Genre>>().Result;
            }

            else
            {
                genres = null;
            }

            ViewBag.Genres = genres;


            return View(game);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            Game? game;

            if (response.IsSuccessStatusCode)
            {
                game = response.Content.ReadFromJsonAsync<Game>().Result;
            }

            else
            {
                game = null;
            }

            return View(game);
        }
    }
}
