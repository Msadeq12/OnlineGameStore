using Microsoft.AspNetCore.Mvc;

namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    [Area("Member")]
    public class MemberController : Controller
    {
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
