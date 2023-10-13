using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.DataAccess;


namespace PROG3050_HMJJ.Areas.Identity.Pages.Account.Manage
{
    public class PreferencesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly GameStoreDbContext _context;

        public PreferencesModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            GameStoreDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

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
            public int PlatformsID { get; set; }


            public int GenresID { get; set; }


            public int LanguagesID { get; set; }
        }

        public List<Platforms> PlatformList { get; set; }

        public List<Genres> GenreList { get; set; }

        public List<Languages> LanguageList { get; set; }

        public Preferences Preferences { get; set; }


        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var preferences = await _context.Preferences.FirstOrDefaultAsync(p => p.IdentityUser.Id == user.Id);

            // Add Preferences record to DB if it does not exist for this account
            if (preferences == null)
            {
                preferences.IdentityUser = user;
                _context.Add(preferences);
                await _context.SaveChangesAsync();
            }

            var platforms = await _context.Platforms.ToListAsync();
            var genres = await _context.Genres.ToListAsync();
            var languages = await _context.Languages.ToListAsync();

            Preferences = preferences;

            PlatformList = platforms;
            GenreList = genres;
            LanguageList = languages;

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

            var preferences = _context.Preferences.FirstOrDefault(p => p.IdentityUser.Id == user.Id);

            preferences.Platforms = await _context.Platforms.FirstOrDefaultAsync(p => p.ID == Input.PlatformsID);
            preferences.Genres = await _context.Genres.FirstOrDefaultAsync(g => g.ID == Input.GenresID);
            preferences.Languages = await _context.Languages.FirstOrDefaultAsync(l => l.ID == Input.LanguagesID);

            _context.Preferences.Update(preferences);
            _context.SaveChanges();
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your preferences have been updated";
            //_logger.LogInformation("User changed their preferences successfully.");
            return RedirectToPage();
        }

        private bool PreferencesExists(string id)
        {
            return (_context.Preferences?.Any(e => e.IdentityUser.Id == id)).GetValueOrDefault();
        }
    }
}
