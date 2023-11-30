using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PROG3050_HMJJ.Areas.Admin.Models;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;

namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Admin, Member")]
    public class WishListController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly GameStoreDbContext _context;
        private readonly HttpClient _client;

        public WishListController(UserManager<User> manager ,GameStoreDbContext context)
        {
            _context = context;
            _userManager = manager;
            _client = new HttpClient();
        }

        public async Task<IActionResult> WishList()
        {
            var user = await _userManager.GetUserAsync(User);

            WishLists wishList = await _context.WishLists.Include(w => w.WishListItems).Where(w => w.User.Id == user.Id).FirstOrDefaultAsync();

            if (wishList == null) 
            {
                wishList = new WishLists { User = user};
                _context.WishLists.Add(wishList);
                _context.SaveChanges();
            }

            if(wishList.WishListItems == null)
            {
                wishList.WishListItems = new List<WishListItems>();
            }

            if(wishList.WishListItems.Count > 0)
            {
                string url = "https://localhost:7108/api/game";

                HttpResponseMessage response = _client.GetAsync(url).Result;
                List<GamesViewModel> games;

                if (response.IsSuccessStatusCode)
                {
                    games = response.Content.ReadFromJsonAsync<List<GamesViewModel>>().Result;

                    foreach(var game in games)
                    {
                        WishListItems item = wishList.WishListItems.SingleOrDefault(wi => wi.GameID == game.ID);
                        if (item != null)
                        {
                            wishList.WishListItems.Remove(item);
                            item.Game = game;
                            wishList.WishListItems.Add(item);
                        }
                    }
                }

                else
                {
                    games = null;
                    wishList.WishListItems = new List<WishListItems>();
                }
            }

            return View(wishList);
        }

        public async Task<IActionResult> AddToWishList(int gameID)
        {
            var user = await _userManager.GetUserAsync(User);

            WishLists wishList = await _context.WishLists.Include(w => w.WishListItems).Where(w => w.User.Id == user.Id).FirstOrDefaultAsync();

            if (wishList == null)
            {
                wishList = new WishLists { User = user};
                _context.WishLists.Add(wishList);
                await _context.SaveChangesAsync();
            }

            WishListItems item = new WishListItems { GameID = gameID, WishLists = wishList};

            _context.WishListItems.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Home", new { area = "Member", id = gameID });
        }


        public async Task<IActionResult> RemoveFromWishList(int gameID)
        {
            var user = await _userManager.GetUserAsync(User);

            WishLists wishList = await _context.WishLists.Include(w => w.WishListItems).Where(w => w.User == user).FirstOrDefaultAsync();

            WishListItems item = wishList.WishListItems.SingleOrDefault(wi => wi.GameID == gameID);

            _context.WishListItems.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Details", "Home", new { area = "Member", id = gameID });
        }
    }
}
