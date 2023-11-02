using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Admin.Models;
using PROG3050_HMJJ.Models.DataAccess;


namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public sealed class EventsController : Controller
    {
        private readonly GameStoreDbContext _context;


        public EventsController(GameStoreDbContext context)
        {
            _context = context; 
        }


        [HttpGet]
        public IActionResult Index()
        {
            var events = _context.Set<Events>().ToList();
            return View(events);
        }


        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Events model)
        {
            _context.Add<Events>(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            var model = _context.Find<Events>(id);
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(Events model)
        {
            _context.Update<Events>(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ViewResult Delete(int id)
        {
            var model = _context.Find<Events>(id);
            return View(model);
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteGame (int id)
        {
            var model = _context.Find<Events>(id);
            _context.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
