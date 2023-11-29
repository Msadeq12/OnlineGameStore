using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Admin.Models;

namespace PROG3050_HMJJ.Areas.Member.Components
{
    public class EventsList : ViewComponent
    {
        private readonly GameStoreDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly HttpClient _client;

        public EventsList(GameStoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            _client = new HttpClient();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string urlEvent = "https://localhost:7193/events";

            HttpResponseMessage response = _client.GetAsync(urlEvent).Result;
            List<Event>? events;

            if (response.IsSuccessStatusCode)
            {
                events = response.Content.ReadFromJsonAsync<List<Event>>().Result;
            }

            else
            {
                events = null;
            }

            return View(events);
            
        }

    }
}
