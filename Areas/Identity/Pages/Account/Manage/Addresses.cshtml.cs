using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;
using System.Net;
using System.Text.RegularExpressions;

namespace PROG3050_HMJJ.Areas.Identity.Pages.Account.Manage
{
    public class AddressesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GameStoreDbContext _context;
        private readonly ILogger<PreferencesModel> _logger;

        public AddressesModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            GameStoreDbContext context,
            ILogger<PreferencesModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }


        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty(SupportsGet = true)]
        public Addresses Addresses { get; set; }


        [BindProperty(SupportsGet = true)]
        public MailingAddresses MailingAddresses { get; set; }


        [BindProperty(SupportsGet = true)]
        public ShippingAddresses ShippingAddresses { get; set; }


        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }


        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public int SelectedMailingCountryID { get; set; }


            public int? SelectedShippingCountryID { get; set; }
        }


        public SelectList MailingCountryList { get; set; }


        public SelectList MailingRegionList { get; set; }


        public SelectList ShippingCountryList { get; set; }


        public SelectList ShippingRegionList { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var address = await _context.Addresses.FirstOrDefaultAsync(p => p.User.Id == user.Id);

            // Add Addresses record to DB if it does not exist for this account
            if (address == null)
            {
                address = new Addresses();
                address.User = user;
                Addresses = address;
                _context.Add(address);
                await _context.SaveChangesAsync();
            }
            else
            {
                Addresses = address;
            }


            var regions = await _context.Regions.ToListAsync();
            var countries = await _context.Countries.ToListAsync();
            countries.Insert(0, new Countries() { ID = 0, Name = "" });
            var mailingAddress = await _context.MailingAddresses.FirstOrDefaultAsync(a => a.Addresses.ID == address.ID);
            var shippingAddress = await _context.ShippingAddresses.FirstOrDefaultAsync(a => a.Addresses.ID == address.ID);


            if (mailingAddress == null)
            {
                mailingAddress = new MailingAddresses();
                mailingAddress.Addresses = address;
                mailingAddress.RegionsID = 1;
                MailingAddresses = mailingAddress;
                _context.MailingAddresses.Add(mailingAddress);
                _context.SaveChanges();
            }
            else
            {
                MailingAddresses = mailingAddress;
            }

            if (shippingAddress == null)
            {
                shippingAddress = new ShippingAddresses();
                shippingAddress.Addresses = address;
                shippingAddress.RegionsID = 1;
                ShippingAddresses = shippingAddress;
                _context.ShippingAddresses.Add(shippingAddress);
                _context.SaveChanges();
            }
            else
            {
                ShippingAddresses = shippingAddress;
            }

            if (MailingAddresses.Line1 == null)
            {
                MailingAddresses.RegionsID = 0;
            }

            if (ShippingAddresses.Line1 == null)
            {
                ShippingAddresses.RegionsID = 0;
            }


            // ToDo: handle or fix null exception

            if (MailingAddresses.RegionsID != 0)
            {
                var mailingSelectedRegion = await _context.Regions.FirstOrDefaultAsync(r => r.ID == MailingAddresses.RegionsID);
                MailingCountryList = new SelectList(countries, "ID", "Name", mailingSelectedRegion.CountriesID);
                Input.SelectedMailingCountryID = mailingSelectedRegion.CountriesID;
            }
            else
            {
                MailingCountryList = new SelectList(countries, "ID", "Name", 0);
                Input.SelectedMailingCountryID = 0;
            }

            if (ShippingAddresses.RegionsID != 0)
            {
                var shippingSelectedRegion = await _context.Regions.FirstOrDefaultAsync(r => r.ID == ShippingAddresses.RegionsID);
                ShippingCountryList = new SelectList(countries, "ID", "Name", shippingSelectedRegion.CountriesID);
                Input.SelectedShippingCountryID = shippingSelectedRegion.CountriesID;
            }
            else
            {
                ShippingCountryList = new SelectList(countries, "ID", "Name", 0);
                Input.SelectedShippingCountryID = 0;
            }

            SelectRegions();
            SetRegionAndCodeLabels();

            if (MailingAddresses.RegionsID != 0){
                MailingRegionList.ElementAt(MailingAddresses.RegionsID).Selected = true;
            }

            if (ShippingAddresses.RegionsID != 0)
            {
                ShippingRegionList.ElementAt(ShippingAddresses.RegionsID).Selected = true;
            }

            return Page();
        }


        public void SelectRegions()
        {
            List<Regions> mailingRegions = _context.Regions.Where(r => r.CountriesID == Input.SelectedMailingCountryID).ToList();

            mailingRegions.Insert(0, new Regions { ID = 0, Name = "" });
            MailingRegionList = new SelectList(mailingRegions, "ID", "Name");

            MailingRegionList.ElementAt(0).Selected = true;

            List<Regions> shippingRegions = _context.Regions.Where(r => r.CountriesID == Input.SelectedShippingCountryID).ToList();

            shippingRegions.Insert(0, new Regions { ID = 0, Name = "" });
            ShippingRegionList = new SelectList(shippingRegions, "ID", "Name");

            ShippingRegionList.ElementAt(0).Selected = true;
        }

        public void SetRegionAndCodeLabels()
        {
            if (Input.SelectedMailingCountryID == 0)
            {
                ViewData["MailingRegion"] = "Region";
                ViewData["MailingCode"] = "Address Code";
            }
            else if (Input.SelectedMailingCountryID == 1)
            {
                ViewData["MailingRegion"] = "Province/Territory";
                ViewData["MailingCode"] = "Postal Code";
            }
            else if (Input.SelectedMailingCountryID == 2)
            {
                ViewData["MailingRegion"] = "State/Territory";
                ViewData["MailingCode"] = "Zip Code";
            }

            if (Input.SelectedShippingCountryID == 0)
            {
                ViewData["ShippingRegion"] = "Region";
                ViewData["ShippingCode"] = "Address Code";
            }
            else if (Input.SelectedShippingCountryID == 1)
            {
                ViewData["ShippingRegion"] = "Province/Territory";
                ViewData["ShippingCode"] = "Postal Code";
            }
            else if (Input.SelectedShippingCountryID == 2)
            {
                ViewData["ShippingRegion"] = "State/Territory";
                ViewData["ShippingCode"] = "Zip Code";
            }
        }

        public async Task<IActionResult> OnPostRegions(string addressType)
        {
            StatusMessage = null;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var countries = _context.Countries.ToList();
            countries.Insert(0, new Countries { ID = 0, Name = "" });
            MailingCountryList = new SelectList(countries, "ID", "Name", Input.SelectedMailingCountryID);
            ShippingCountryList = new SelectList(countries, "ID", "Name", Input.SelectedShippingCountryID);

            SetRegionAndCodeLabels();
            SelectRegions();

            if (addressType.Equals("shipping"))
            {
                if (MailingAddresses.RegionsID != 0)
                {
                    MailingRegionList.ElementAt(MailingAddresses.RegionsID).Selected = true;
                }
            }
            else if (addressType.Equals("mailing"))
            {
                if (ShippingAddresses.RegionsID != 0)
                {
                    ShippingRegionList.ElementAt(ShippingAddresses.RegionsID).Selected = true;
                }
            }

            return Page();
        }


        public async Task<IActionResult> OnPostSameAddress()
        {
            StatusMessage = "Make sure to save";
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var countries = _context.Countries.ToList();
            countries.Insert(0, new Countries { ID = 0, Name = "" });
            MailingCountryList = new SelectList(countries, "ID", "Name", Input.SelectedMailingCountryID);
            ShippingCountryList = new SelectList(countries, "ID", "Name", Input.SelectedShippingCountryID);

            SetRegionAndCodeLabels();
            SelectRegions();

            if (MailingAddresses.RegionsID != 0)
            {
                MailingRegionList.ElementAt(MailingAddresses.RegionsID).Selected = true;
            }
            
            if (ShippingAddresses.RegionsID != 0)
            {
                ShippingRegionList.ElementAt(ShippingAddresses.RegionsID).Selected = true;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAddresses()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ValidateMailingAddress();

            if (Addresses.SameAddress == false)
            {
                ValidateShippingAddress();
            }

            var countries = _context.Countries.ToList();
            countries.Insert(0, new Countries { ID = 0, Name = "" });
            MailingCountryList = new SelectList(countries, "ID", "Name", Input.SelectedMailingCountryID);
            ShippingCountryList = new SelectList(countries, "ID", "Name", Input.SelectedShippingCountryID);

            SetRegionAndCodeLabels();
            SelectRegions();

            if (MailingAddresses.RegionsID != 0)
            {
                MailingRegionList.ElementAt(MailingAddresses.RegionsID).Selected = true;
            }
            if (ShippingAddresses.RegionsID != 0)
            {
                ShippingRegionList.ElementAt(ShippingAddresses.RegionsID).Selected = true;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Addresses.User = user;

            MailingAddresses.Addresses = Addresses;
            MailingAddresses.Regions = _context.Regions.Where(r => r.ID == MailingAddresses.RegionsID).FirstOrDefault();
            _context.MailingAddresses.Update(MailingAddresses);
            _context.SaveChanges();


            ShippingAddresses.Addresses = Addresses;

            // Avoid adding selected platforms to database if there aren't any selected
            if (Addresses.SameAddress == true)
            {
                ShippingAddresses.Addresses = Addresses;
                ShippingAddresses.RegionsID = 1;
            }
            else
            {
                ShippingAddresses.Regions = _context.Regions.Where(r => r.ID == ShippingAddresses.RegionsID).FirstOrDefault();
            }

            _context.ShippingAddresses.Update(ShippingAddresses);
            _context.SaveChanges();
            _context.Addresses.Update(Addresses);
            _context.SaveChanges();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your address information have been updated";
            _logger.LogInformation("User changed their address information successfully.");
            return RedirectToPage();
        }

        
        public async void ValidateMailingAddress()
        {
            if (MailingAddresses.Line1 == null)
            {
                ModelState.AddModelError("MailingAddresses.Line1", "Please enter address line 1");
            }

            if (MailingAddresses.RegionsID == 0)
            {
                ModelState.AddModelError("MailingAddresses.RegionsID", "Please choose a region");
            }

            if (System.String.IsNullOrEmpty(MailingAddresses.City))
            {
                ModelState.AddModelError("MailingAddresses.City", "Please enter a city");
            }

            if (!System.String.IsNullOrWhiteSpace(MailingAddresses.PostalCode))
            {
                if (Input.SelectedMailingCountryID != 0)
                {
                    Regex regex;
                    if (Input.SelectedMailingCountryID == 1)
                    {
                        regex = new Regex("^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] [0-9][ABCEGHJKMNPRSTVWXYZ][0-9]$");
                        if (!regex.Match(MailingAddresses.PostalCode).Success)
                        {
                            ModelState.AddModelError("MailingAddresses.PostalCode", "Please enter a valid Postal Code");
                        }
                    }
                    else if (Input.SelectedMailingCountryID == 2)
                    {
                        regex = new Regex("^\\d{5}(-{0,1}\\d{4})?$");
                        if (!regex.Match(MailingAddresses.PostalCode).Success)
                        {
                            ModelState.AddModelError("MailingAddresses.PostalCode", "Please enter a valid Zip Code");
                        }
                    }
                }
            }
            else
            {
                if (Input.SelectedMailingCountryID == 1)
                {
                    ModelState.AddModelError("MailingAddresses.PostalCode", "Please enter a Postal Code");
                }
                else if (Input.SelectedMailingCountryID == 2)
                {
                    ModelState.AddModelError("MailingAddresses.PostalCode", "Please enter a Zip Code");
                }
            }
        }


        public async void ValidateShippingAddress()
        {
            if (ShippingAddresses.Line1 == null)
            {
                ModelState.AddModelError("ShippingAddresses.Line1", "Please enter address line 1");
            }

            if (ShippingAddresses.RegionsID == 0)
            {
                ModelState.AddModelError("ShippingAddresses.RegionsID", "Please choose a region");
            }

            if (System.String.IsNullOrEmpty(ShippingAddresses.City))
            {
                ModelState.AddModelError("ShippingAddresses.City", "Please enter a city");
            }

            if (!System.String.IsNullOrWhiteSpace(ShippingAddresses.PostalCode))
            {
                if (Input.SelectedShippingCountryID != 0)
                {
                    Regex regex;
                    if (Input.SelectedShippingCountryID == 1)
                    {
                        regex = new Regex("^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] [0-9][ABCEGHJKMNPRSTVWXYZ][0-9]$");
                        if (!regex.Match(ShippingAddresses.PostalCode).Success)
                        {
                            ModelState.AddModelError("ShippingAddresses.PostalCode", "Please enter a valid Postal Code");
                        }
                    }
                    else if (Input.SelectedShippingCountryID == 2)
                    {
                        regex = new Regex("^\\d{5}(-{0,1}\\d{4})?$");
                        if (!regex.Match(ShippingAddresses.PostalCode).Success)
                        {
                            ModelState.AddModelError("ShippingAddresses.PostalCode", "Please enter a valid Zip Code");
                        }
                    }
                }
            }
            else
            {
                if (Input.SelectedShippingCountryID == 1)
                {
                    ModelState.AddModelError("ShippingAddresses.PostalCode", "Please enter a Postal Code");
                }
                else if (Input.SelectedShippingCountryID == 2)
                {
                    ModelState.AddModelError("ShippingAddresses.PostalCode", "Please enter a Zip Code");
                }
            }
        }
    }
}
