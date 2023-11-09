// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.DataAccess;
using PROG3050_HMJJ.Models.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;

namespace PROG3050_HMJJ.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GameStoreDbContext _context;

        public ProfileModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            GameStoreDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty(SupportsGet = true)]
        public Profiles Profile { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.User.Id == user.Id);

            if(profile == null)
            {
                profile = new Profiles();
                profile.User = user;
                Profile = profile;
                _context.Add(profile);
                await _context.SaveChangesAsync();
            }

            Profile = profile;
            
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage();
        }

        

        public async Task<IActionResult> OnPostAddress()
        {
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Profile.User = user;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Profiles.Update(Profile);
            _context.SaveChanges();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
