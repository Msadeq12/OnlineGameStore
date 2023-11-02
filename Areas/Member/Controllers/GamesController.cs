using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;


namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    [Area("Member")]
    //[Authorize(Roles = "Member")]
    public sealed class GamesController : Controller
    {
        private readonly GameStoreDbContext _context;
        private readonly UserManager<User> _userManager;

        public GamesController(GameStoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Recommends games to the Member, does not if there are no preferences specified
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var preferences = await _context.Preferences.FirstOrDefaultAsync(p => p.User.Id == user.Id);

            var models = _context.Set<Games>().ToList();

            // If preferences do not exist for user do not filter
            if (preferences == null)
            {
                return View(models);
            }
            else
            {

                var recommendedGames = new List<Games>();
                List<Games>? recommendedGamesByGenre = new List<Games>();
                List<Games>? recommendedGamesByPlatform = new List<Games>();

                // Grab data for user
                var platforms = await _context.Platforms.ToListAsync();
                var selectedPlatforms = await _context.SelectedPlatforms.Where(p => p.Preferences.ID == preferences.ID).Select(s => s.Platforms).ToListAsync();
                var genres = await _context.Genres.ToListAsync();
                var selectedGenres = await _context.SelectedGenres.Where(g => g.Preferences.ID == preferences.ID).Select(s => s.Genres).ToListAsync();
                var languages = await _context.Languages.ToListAsync();
                
                foreach (var genre in selectedGenres)
                {
                    List<Games>? games = _context.Set<Games>().Where(g => g.GenresID == genre.ID).ToList();
                    foreach (var game in games)
                    {
                        recommendedGamesByGenre.Add(game);
                    }
                }

               foreach (var platform in selectedPlatforms)
                {
                    List<Games>? games = _context.Set<Games>().Where(g => g.PlatformsID == platform.ID).ToList();
                    foreach (var game in games)
                    {
                        recommendedGamesByPlatform.Add(game);
                    }
                }

                foreach (var model in models)
                {
                    if (recommendedGamesByGenre.Contains(model) && recommendedGamesByPlatform.Contains(model))
                    {
                        recommendedGames.Add(model);
                    }
                }

                return View(recommendedGames);
            }
        }
    }
}
