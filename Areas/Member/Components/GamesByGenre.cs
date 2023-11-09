using Microsoft.AspNetCore;
using PROG3050_HMJJ.Areas.Member.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;

namespace PROG3050_HMJJ.Areas.Member.Components
{
    public class GamesByGenre : ViewComponent
    {
        private GameStoreDbContext _context;
        private UserManager<User> _userManager;

        public GamesByGenre(GameStoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IViewComponentResult Invoke()
        {
            string baseUrl = "$https://localhost:7108/api/game/GameByGenre?";

            return View();
        }  
    }
}
