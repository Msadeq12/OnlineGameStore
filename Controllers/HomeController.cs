using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Diagnostics;

namespace PROG3050_HMJJ.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
        }

      
        public IActionResult Index()
        {
            string url = "https://localhost:7108/api/game";

            HttpResponseMessage response = _client.GetAsync(url).Result;
            List<GamesViewModel> games;

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

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }

            string url = $"https://localhost:7108/api/game/search?search={searchString}";

            HttpResponseMessage searchResponse = _client.GetAsync(url).Result;
            Console.WriteLine("Search status code: " + searchResponse.StatusCode);
            Console.WriteLine("URL: " + url);
            List<GamesViewModel>? games;

            if (searchResponse.IsSuccessStatusCode)
            {
                games = searchResponse.Content.ReadFromJsonAsync<List<GamesViewModel>>().Result;
            }

            else
            {
                games = null;
            }

            return View(games);
        }


        [HttpGet]
        public ViewResult Details(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";

            HttpResponseMessage response = _client.GetAsync(url).Result;
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

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}