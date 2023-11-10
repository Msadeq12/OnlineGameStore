using Microsoft.AspNetCore;
using PROG3050_HMJJ.Areas.Member.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Admin.Models;

namespace PROG3050_HMJJ.Areas.Member.Components
{
    public class GamesByGenre : ViewComponent
    {
        private readonly GameStoreDbContext _context;
        private readonly UserManager<User> _userManager;
        private HttpClient _client;

        public GamesByGenre(GameStoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            _client = new HttpClient();
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
            string endpoint = "model";

            List<GamesViewModel>? gameByGenre;

            if (user == null)
            {
                Console.WriteLine("User not found.");
            }

            var preferences = _context.Preferences.FirstOrDefault(p => p.User.Id == user.Id);

            if (preferences == null)
            {
                Console.WriteLine("Preferences not found.");
                gameByGenre = new List<GamesViewModel>();
            }

            else
            {
                var platforms = await _context.Platforms.ToListAsync();
                var selectedGenres = await _context.SelectedGenres.Where(g => g.Preferences.ID == preferences.ID).Select(s => s.Genres.Name).ToListAsync();

                string baseUrl = "https://localhost:7108/api/game/GameByGenre?";

                string updatedUrl = HelperClass.QueryUrlParser(selectedGenres, baseUrl, endpoint);

                HttpResponseMessage response = await _client.GetAsync(updatedUrl);


                await Console.Out.WriteLineAsync("GameGenre View status: " + response.StatusCode);
                await Console.Out.WriteLineAsync("updated url from VC: " + updatedUrl);

                if (response.IsSuccessStatusCode)
                {
                    gameByGenre = response.Content.ReadFromJsonAsync<List<GamesViewModel>>().Result;
                }

                else
                {
                    gameByGenre = null;
                }

            }

            

            return View(gameByGenre);
        }
    }
}
