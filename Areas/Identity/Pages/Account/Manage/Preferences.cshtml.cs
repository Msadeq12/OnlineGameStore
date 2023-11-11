using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.DataAccess;
using System.ComponentModel.DataAnnotations;
using PROG3050_HMJJ.Models.Account;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace PROG3050_HMJJ.Areas.Identity.Pages.Account.Manage
{
    public class PreferencesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GameStoreDbContext _context;
        private readonly ILogger<PreferencesModel> _logger;

        public PreferencesModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            GameStoreDbContext context,
            ILogger<PreferencesModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public List<int>? SelectedPlatformsIDList { get; set; }


            public List<int>? SelectedGenresIDList { get; set; }


            [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Preferred language must be specified")]
            public int LanguagesID { get; set; }
        }

        public MultiSelectList PlatformList { get; set; }


        public MultiSelectList GenreList { get; set; }


        public SelectList LanguageList { get; set; }


        public Preferences Preferences { get; set; }


        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var preferences = await _context.Preferences.FirstOrDefaultAsync(p => p.User.Id == user.Id);

            // Add Preferences record to DB if it does not exist for this account
            if (preferences == null)
            {
                preferences = new Preferences();
                preferences.User = user;
                Preferences = preferences;
                _context.Add(preferences);
                await _context.SaveChangesAsync();
            }

            var platforms = await _context.Platforms.ToListAsync();
            var selectedPlatforms = await _context.SelectedPlatforms.Where(p => p.Preferences.ID == preferences.ID).Select(s => s.Platforms.ID).ToListAsync();
            var genres = await _context.Genres.ToListAsync();
            var selectedGenres = await _context.SelectedGenres.Where(g => g.Preferences.ID == preferences.ID).Select(s => s.Genres.ID).ToListAsync();
            var languages = await _context.Languages.ToListAsync();

            PlatformList = new MultiSelectList(platforms, "ID", "Name", selectedPlatforms);
            GenreList = new MultiSelectList(genres, "ID", "Name", selectedGenres);
            
            if(preferences.Languages != null)
            {
                LanguageList = new SelectList(languages, "ID", "Name", preferences.Languages.ID);
            }
            else
            {
                languages.Add(new Languages { ID = 0, Name = ""});
                LanguageList = new SelectList(languages, "ID", "Name", 0);
            }
            

            Preferences = preferences;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var preferences = _context.Preferences.FirstOrDefault(p => p.User.Id == user.Id);
            var previouslySelectedPlatforms = _context.SelectedPlatforms.Where(p => p.Preferences.ID == preferences.ID);
            var previouslySelectedGenres = _context.SelectedGenres.Where(p => p.Preferences.ID == preferences.ID);


            // For both SelectedPlatforms and SelectedGenres I am mimicking an upsert (i.e. delete if exists, then insert)
            // Remove all Previously selected platforms
            foreach (var selectedPlatform in previouslySelectedPlatforms)
            {
                _context.SelectedPlatforms.Remove(selectedPlatform);
                await _context.SaveChangesAsync();
            }

            // Avoid adding selected platforms to database if there aren't any selected
            if (Input.SelectedPlatformsIDList != null)
            {
                // Add newly selected platforms
                foreach (var platformID in Input.SelectedPlatformsIDList)
                {
                    var selectedPlatform = new SelectedPlatforms();
                    selectedPlatform.Preferences = preferences;
                    var platform = _context.Platforms.FirstOrDefault(p => p.ID == platformID);
                    selectedPlatform.Platforms = platform;
                    _context.Add(selectedPlatform);
                    await _context.SaveChangesAsync();
                }
            }

            // Remove all Previously selected genres
            foreach (var selectedGenre in previouslySelectedGenres)
            {
                _context.SelectedGenres.Remove(selectedGenre);
                await _context.SaveChangesAsync();
            }

            // Avoid adding selected genres to database if there aren't any selected
            if (Input.SelectedGenresIDList != null)
            {
                // Add newly selected genres
                foreach (var genreID in Input.SelectedGenresIDList)
                {
                    var selectedGenre = new SelectedGenres();
                    selectedGenre.Preferences = preferences;
                    var genre = _context.Genres.FirstOrDefault(p => p.ID == genreID);
                    selectedGenre.Genres = genre;
                    _context.Add(selectedGenre);
                    await _context.SaveChangesAsync();
                }
            }

            preferences.Languages = await _context.Languages.FirstOrDefaultAsync(l => l.ID == Input.LanguagesID);

            _context.Preferences.Update(preferences);
            _context.SaveChanges();
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your preferences have been updated";
            _logger.LogInformation("User changed their preferences successfully.");
            return RedirectToPage();
        }

        private bool PreferencesExists(string id)
        {
            return (_context.Preferences?.Any(e => e.User.Id == id)).GetValueOrDefault();
        }
    }
}
