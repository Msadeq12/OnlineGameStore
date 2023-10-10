using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Admin.Models;

namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Event>? events = new List<Event>();

            return View(events);
        }
    }
}
