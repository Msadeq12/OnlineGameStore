using Microsoft.AspNetCore.Mvc;

namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    public class PreferencesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
