using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Text;

namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private static HttpClient client;
        GenrePlatformViewModel? genresPlatforms;

        public GameController()
        {
            string urlGenre = "https://localhost:7108/api/game/GenresPlatforms";
            client = new HttpClient();

            HttpResponseMessage responseGenre = client.GetAsync(urlGenre).Result;

            if (responseGenre.IsSuccessStatusCode)
            {
                genresPlatforms = responseGenre.Content.ReadFromJsonAsync<GenrePlatformViewModel>().Result;
            }

            else
            {
                genresPlatforms = null;
            }

        }

        /// <summary>
        /// Calls GetAllGames() from the GameService API
        /// </summary>
        /// <returns>A List of Games from GameService DB</returns>
        
        public IActionResult Index()
        {

            string url = "https://localhost:7108/api/game/";

            HttpResponseMessage response = client.GetAsync(url).Result;

            List<GamesViewModel>? games;

            if (response.IsSuccessStatusCode)
            {
                games = response.Content.ReadFromJsonAsync<List<GamesViewModel>>().Result;
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
        public ViewResult Add()
        {
            ViewBag.Genres = genresPlatforms.GenreList;
            ViewBag.Platforms = genresPlatforms.PlatformList;

            return View();
        }

        [HttpPost]
        public IActionResult Add(GamesViewModel game)
        {

            string url = "https://localhost:7108/api/game";

            HttpResponseMessage response = client.PostAsJsonAsync(url, game).Result;

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
        public ViewResult Edit(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";
            

            //this is a response for getting a specific game through API
            //passed through the View 
            HttpResponseMessage response = client.GetAsync(url).Result;
            GamesViewModel? game;

            if (response.IsSuccessStatusCode)
            {
                game = response.Content.ReadFromJsonAsync<GamesViewModel>().Result;
            }

            else
            {
                game = null;
            }

            // this is a response for getting all genres through API
            //response passed on through ViewBag
            ViewBag.Genres = genresPlatforms.GenreList;
            ViewBag.Platforms = genresPlatforms.PlatformList;


            return View(game);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, GamesViewModel game)
        {

            string url = $"https://localhost:7108/api/game/{id}";
            HttpResponseMessage response = client.PutAsJsonAsync(url, game).Result;
            Console.WriteLine("Edit status code: " + response.StatusCode);
            Console.WriteLine("Game content: " + JsonConvert.SerializeObject(game));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.Genres = genresPlatforms.GenreList;
                ViewBag.Platforms = genresPlatforms.PlatformList;
                return View(game);
            }


        }

        
        [HttpGet]
        public ViewResult Delete(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            GamesViewModel? game;

            if (response.IsSuccessStatusCode)
            {
                game = response.Content.ReadFromJsonAsync<GamesViewModel>().Result;
            }

            else
            {
                game = null;
            }

            return View(game);
        }

        
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteGame(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";

            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }
        }
    }
}
