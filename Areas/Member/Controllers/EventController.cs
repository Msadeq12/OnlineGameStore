using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Diagnostics;
using PROG3050_HMJJ.Models.DataAccess;
using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Models.Account;

namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    public class EventController : Controller
    {
        private readonly GameStoreDbContext _context;
        private readonly UserManager<User> _userManager;

        public EventController(GameStoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
