
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Diagnostics;
using PROG3050_HMJJ.Models.DataAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Models.Account;

namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Admin, Member")]
    public class HomeController : Controller
    {
        private HttpClient _client;
        private readonly UserManager<User> _userManager;


        public HomeController(GameStoreDbContext context)
        {
            _context = context;
            _client = new HttpClient();
        }

        private readonly GameStoreDbContext _context;

        [HttpGet]
        public ViewResult Index()
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
                if (game != null)
                {
                    // Initialize the NewReview property with a new Reviews object
                    game.NewReview = new Reviews() { GameId = game.ID };
                    game.ApprovedReviews = _context.Reviews.Where(r => r.GameId == id && r.IsApproved==true)
                                             .ToList();
                    ViewBag.CurrentUsername = User?.Identity.Name ?? string.Empty;
                }
            }

            else
            {
                game = null;
            }

            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(GamesViewModel model)
        {
            foreach (var key in ModelState.Keys)
            {
                var value = ModelState[key];
                Console.WriteLine($"Key: {key}, Errors: {value.Errors.Count}, Value: {value.AttemptedValue}");
            }
            // Disable validation for unrelated fields
            var unrelatedFields = new[] { "Title", "GameGenre", "Publisher", "Description", "ReleaseYear", "GamePlatform", "CommentId", "UserId", "ApprovedReviews" };
            foreach (var field in unrelatedFields)
            {
                ModelState.Remove(field);
            }
            if (ModelState.IsValid)
            {
                var review = new Reviews
                {
                    Timestamp = DateTime.Now,
                    IsApproved = null,
                    UserId = model.NewReview.UserId,
                    CommentText = model.NewReview.CommentText,
                    GameId = model.NewReview.GameId,
                    CommentId = model.NewReview.CommentId
                };
                // Add the review to the database context
                _context.Reviews.Add(review);

                // Save changes asynchronously
                await _context.SaveChangesAsync();
                TempData["ReviewMessage"] = "Review submitted successfully, will be shown once approved by an admin.";
                // Redirect to the game's details page after submission
                return RedirectToAction("Details", "Home", new { area = "Member", id = review.GameId });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Log the error message
                    Console.WriteLine(error.ErrorMessage);
                }

                // If the model state is not valid, you might want to return to the form with validation messages
                // For simplicity, redirecting back to the same game's details page
                return RedirectToAction("Details", "Home", new { area = "Member", id = model.NewReview.GameId });
            }

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