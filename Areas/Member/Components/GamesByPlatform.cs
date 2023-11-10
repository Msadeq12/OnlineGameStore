

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Admin.Models;
using PROG3050_HMJJ.Areas.Member.Helper;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;

namespace PROG3050_HMJJ.Areas.Member.Components
{
    public class GamesByPlatform : ViewComponent
    {
        private readonly GameStoreDbContext _context;
        private readonly UserManager<User> _userManager;
        private HttpClient _client;

        public GamesByPlatform(GameStoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            _client = new HttpClient();
        }

        public async  Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
            string endpoint = "model";
            List<GamesViewModel>? gameByPlatform;

            if (user == null)
            {
                Console.WriteLine("User not found.");
            }

            var preferences = _context.Preferences.FirstOrDefault(p => p.User.Id == user.Id);

            if (preferences == null)
            {
                Console.WriteLine("Preferences not found.");
                gameByPlatform = new List<GamesViewModel>();
            }

            else
            {
                var platforms = await _context.Platforms.ToListAsync();
                var selectedPlatforms = await _context.SelectedPlatforms.Where(g => g.Preferences.ID == preferences.ID).Select(s => s.Platforms.Name).ToListAsync();

                string baseUrl = "https://localhost:7108/api/game/GameByPlatform?";

                string updatedUrl = HelperClass.QueryUrlParser(selectedPlatforms, baseUrl, endpoint);

                HttpResponseMessage response = await _client.GetAsync(updatedUrl);


                await Console.Out.WriteLineAsync("GameGenre View status: " + response.StatusCode);
                await Console.Out.WriteLineAsync("updated url from VC: " + updatedUrl);

                if (response.IsSuccessStatusCode)
                {
                    gameByPlatform = response.Content.ReadFromJsonAsync<List<GamesViewModel>>().Result;
                }

                else
                {
                    gameByPlatform = null;
                }

            }

            

            return View(gameByPlatform);
        }
    }
    
}
