using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Admin.Models;

namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly HttpClient client;

        public EventController()
        {
            client = new HttpClient();
            
        }

        [HttpGet]
        public ViewResult Index()
        {
            //List<Event>? events = new List<Event>();
            string url = "https://localhost:7193/events";

            HttpResponseMessage response = client.GetAsync(url).Result;
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

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Event eves)
        {
            string url = "https://localhost:7193/events";

            HttpResponseMessage response = client.PostAsJsonAsync(url, eves).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            string url = $"https://localhost:7193/events/{id}";

            HttpResponseMessage response = client.GetAsync(url).Result;
            Event? eves;

            if (response.IsSuccessStatusCode)
            {
                eves = response.Content.ReadFromJsonAsync<Event>().Result;
            }

            else
            {
                eves = null;
            }

            return View(eves);
        }

        [HttpPost]
        public IActionResult Edit(int id, Event eves)
        {
            string url = $"https://localhost:7193/events/{id}";

            HttpResponseMessage response = client.PutAsJsonAsync(url, eves).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View(eves);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            string url = $"https://localhost:7193/events/{id}";

            HttpResponseMessage response = client.GetAsync(url).Result;
            Event? eves;

            if (response.IsSuccessStatusCode)
            {
                eves = response.Content.ReadFromJsonAsync<Event>().Result;
            }

            else
            {
                eves = null;
            }

            return View(eves);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteEvent (int id)
        {
            string url = $"https://localhost:7193/events/{id}";

            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }
        }
    }
}
