using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Diagnostics;
using PROG3050_HMJJ.Areas.Member.Models;
using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;
using Microsoft.EntityFrameworkCore;


namespace PROG3050_HMJJ.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly UserManager<User> _userManager;
        private readonly GameStoreDbContext _context;


        public HomeController(GameStoreDbContext context)
        {
            _context = context;
            _client = new HttpClient();
        }


        /// <summary>
        /// Loads the member's home page
        /// On the home page member is presented with our entire game selection
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            string url = "https://localhost:7108/api/game";

            var response = _client.GetAsync(url).Result;
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


        /// <summary>
        /// Recieve a game matching a specific title from the game service
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Get games details from the game service by providing an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Details(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";

            HttpResponseMessage response = _client.GetAsync(url).Result;
            GamesViewModel? game;

            if (response.IsSuccessStatusCode)
            {
                game = response.Content.ReadFromJsonAsync<GamesViewModel>().Result;
                if (game != null)
                {
                    // Initialize the NewReview property with a new Reviews object
                    game.NewReview = new Reviews() { GameId = game.ID };
                    game.NewRating = new Ratings() { GameID = game.ID };
                    game.ApprovedReviews = _context.Reviews.Where(r => r.GameId == id && r.IsApproved == true)
                                             .ToList();
                    var currentUser = User?.Identity;
                    ViewBag.CurrentUsername = currentUser.Name ?? string.Empty;

                    //ToDo: get review for current user + extra???
                    /*var userRating = _context.Ratings
                      .Include(a => a.User)
                      .Where(a => a.User.UserName == currentUser.Name)
                      .Where(g => g.GameID == id)
                      .ToList();*/


                    game.Ratings = _context.Ratings
                      .Where(g => g.GameID == id)
                      .ToList();


                   /* var totalRating = _context.Ratings
                      .Where(g => g.GameID == id)
                      .Sum(r => r.Value);*/

                    /*ViewBag.TotalRating = totalRating;*/
                }
            }
            else
            {
                game = null;
            }

            return View(game);
        }


        /// <summary>
        /// Render the sites privacy policy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }


        /// <summary>
        /// Display website related errors to the member
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Areas.Member.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}