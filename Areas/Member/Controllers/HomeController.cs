﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Diagnostics;

namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Admin, Member")]
    public class HomeController : Controller
    {
        private HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
        }


        [HttpGet]
        public ViewResult Index()
        {
            string url = "https://localhost:7108/api/game";

            HttpResponseMessage response = _client.GetAsync(url).Result;
            List<GamesViewModel> games;

            if (response.IsSuccessStatusCode)
            {
                games = response.Content.ReadFromJsonAsync<List<GamesViewModel>>().Result;
            }

            else
            {
                games = null;
            }

            return View(games);
        }

   

        [HttpGet]
        public ViewResult Details(int id)
        {
            string url = $"https://localhost:7108/api/game/{id}";

            HttpResponseMessage response = _client.GetAsync(url).Result;
            GamesViewModel? game;

            if (response.IsSuccessStatusCode)
            {
                game = response.Content.ReadFromJsonAsync<GamesViewModel>().Result;
            }

            else
            {
                game = null;
            }

            return View(game);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}