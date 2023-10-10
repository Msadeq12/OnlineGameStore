using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Models.ViewModel;

namespace PROG3050_HMJJ.Controllers.Account
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(LoginSignUpViewModel model)
        {
            return View();
        }
    }
}
