using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.DataAccess;


// TODO: Remove Unused Views (i.e. Create, Details, Index, Delete) preferences should be tied to associated account
namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    [Area("Member")]
    public class PreferencesController : Controller
    {
        private readonly PreferencesContext _context;


        public PreferencesController(PreferencesContext context)
        {
            _context = context;
        }

        /*
        // GET: Member/Preferences
        public async Task<IActionResult> Preferences()
        {
            return _context.Preferences != null ?
                        View(await _context.Preferences.ToListAsync()) :
                        Problem("Entity set 'GameStoreDbContext.Preferences'  is null.");
        }
        

        // GET: Member/Preferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Preferences == null)
            {
                return NotFound();
            }

            var preferences = await _context.Preferences
                .FirstOrDefaultAsync(m => m.ID == id);
            if (preferences == null)
            {
                return NotFound();
            }

            return View(preferences);
        }


        // GET: Member/Preferences/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Member/Preferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountID,FavouritePlatformID,FavouriteGenreID,Language")] Preferences preferences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preferences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(preferences);
        }

        */
        // GET: Member/Preferences/Edit/5
        public async Task<IActionResult> Preferences(int? id)
        {
            if (id == null || _context.Preferences == null)
            {
                return NotFound();
            }

            var preferences = await _context.Preferences.FindAsync(id);
            if (preferences == null)
            {
                return NotFound();
            }

            var platforms = await _context.Platforms.ToListAsync();
            var genres = await _context.Genres.ToListAsync();

            ViewBag.Platforms = platforms;
            ViewBag.Genres = genres;

            return View(preferences);
        }


        // POST: Member/Preferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Preferences(int id, [Bind("ID,AccountID,FavouritePlatformID,FavouriteGenreID,Language")] Preferences preferences)
        {
            if (id != preferences.IdentityUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preferences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferencesExists(preferences.IdentityUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(preferences);
        }

        /*
        // GET: Member/Preferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Preferences == null)
            {
                return NotFound();
            }

            var preferences = await _context.Preferences
                .FirstOrDefaultAsync(m => m.ID == id);
            if (preferences == null)
            {
                return NotFound();
            }

            return View(preferences);
        }


        // POST: Member/Preferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Preferences == null)
            {
                return Problem("Entity set 'GameStoreDbContext.Preferences'  is null.");
            }
            var preferences = await _context.Preferences.FindAsync(id);
            if (preferences != null)
            {
                _context.Preferences.Remove(preferences);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        */
        private bool PreferencesExists(int id)
        {
            return (_context.Preferences?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
