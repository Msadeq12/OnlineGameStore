using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.DataAccess;


namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class GamesController : Controller
    {
        private readonly GameStoreDbContext _context;


        public GamesController(GameStoreDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Calls GetAllGames() from the GameService API
        /// </summary>
        /// <returns>A List of Games from GameService DB</returns>
        public IActionResult Index()
        {
            var models = _context.Set<Games>().ToList();
            return View(models);
        }


        /// <summary>
        /// the Add method is also calling the API for getting all genres,
        /// similar to the Edit get method.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Add()
        {
            var genres = _context.Set<Genres>().ToList();
            var platforms = _context.Set<Platforms>().ToList();
            ViewBag.Genres = genres;
            ViewBag.Platforms = platforms;
            return View();
        }


        [HttpPost]
        public IActionResult Add(Games model)
        {
            _context.Add<Games>(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
            var model = _context.Find<Games>(id);
            var genres = _context.Set<Genres>().ToList();
            var platforms = _context.Set<Platforms>().ToList();
            ViewBag.Genres = new SelectList(genres, "ID", "Name", model.GenresID);
            ViewBag.Platforms = new SelectList(platforms, "ID", "Name", model.PlatformsID);
            return View(model);
        }
        

        [HttpPost]
        public IActionResult Edit(Games model)
        {
            _context.Update<Games>(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var model = _context.Find<Games>(id);
            var genre = _context.Find<Genres>(id);
            var platform = _context.Find<Platforms>(id);
            ViewData["genre"] = genre.Name;
            ViewData["platform"] = platform.Name;
            return View(model);
        }

        
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteGame(int id)
        {
            var model = _context.Find<Games>(id);
            _context.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
