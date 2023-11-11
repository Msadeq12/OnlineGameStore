using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;


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


        [BindProperty]
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
            var mailingAddress = await _context.MailingAddresses.FirstOrDefaultAsync(a => a.Addresses.ID == address.ID);
            var shippingAddress = await _context.ShippingAddresses.FirstOrDefaultAsync(a => a.Addresses.ID == address.ID);


            if (mailingAddress == null)
            {
                mailingAddress = new MailingAddresses();
                mailingAddress.Addresses = address;
                mailingAddress.RegionsID = 1;
                MailingAddresses = mailingAddress;
                _context.Add(mailingAddress);
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
                _context.Add(shippingAddress);
                _context.SaveChanges();
            }
            else
            {
                ShippingAddresses = shippingAddress;
            }


            if (address.SameAddress)
            {
                ShippingAddresses.AddressesID = Addresses.ID;
                ShippingAddresses.RegionsID = MailingAddresses.RegionsID;
                ShippingAddresses.Addresses = Addresses;
                ShippingAddresses.Regions = MailingAddresses.Regions;
                ShippingAddresses.Line1 = MailingAddresses.Line1;
                ShippingAddresses.Line2 = MailingAddresses.Line2;
                ShippingAddresses.City = MailingAddresses.City;
                ShippingAddresses.PostalCode = MailingAddresses.PostalCode;
            }


            // ToDo: handle or fix null exception

            if (mailingAddress != null)
            {
                var mailingSelectedRegion = await _context.Regions.FirstOrDefaultAsync(r => r.ID == mailingAddress.RegionsID);
                MailingCountryList = new SelectList(countries, "ID", "Name", mailingSelectedRegion.CountriesID);
                MailingRegionList = new SelectList(regions, "ID", "Name", mailingSelectedRegion.ID);
            }
            else
            {
                MailingCountryList = new SelectList(countries, "ID", "Name", 1);
                MailingRegionList = new SelectList(regions, "ID", "Name", 1);
            }

            if (shippingAddress != null)
            {
                var shippingSelectedRegion = await _context.Regions.FirstOrDefaultAsync(r => r.ID == shippingAddress.RegionsID);
                ShippingCountryList = new SelectList(countries, "ID", "Name", shippingSelectedRegion.CountriesID);
                ShippingRegionList = new SelectList(regions, "ID", "Name", shippingSelectedRegion.ID);
            }
            else
            {
                ShippingCountryList = new SelectList(countries, "ID", "Name", 1);
                ShippingRegionList = new SelectList(regions, "ID", "Name", 1);
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Addresses.User = user;

            MailingAddresses.Addresses = Addresses;
            MailingAddresses.Regions = _context.Regions.Where(r => r.ID == MailingAddresses.RegionsID).FirstOrDefault();
            _context.MailingAddresses.Update(MailingAddresses);
            _context.SaveChanges();


            ShippingAddresses.Addresses = Addresses;

            // Avoid adding selected platforms to database if there aren't any selected
            if (Addresses.SameAddress != null && Addresses.SameAddress == true)
            {
                ShippingAddresses.AddressesID = Addresses.ID;
                ShippingAddresses.RegionsID = MailingAddresses.RegionsID;
                ShippingAddresses.Addresses = Addresses;
                ShippingAddresses.Regions = MailingAddresses.Regions;
                ShippingAddresses.Line1 = MailingAddresses.Line1;
                ShippingAddresses.Line2 = MailingAddresses.Line2;
                ShippingAddresses.City = MailingAddresses.City;
                ShippingAddresses.PostalCode = MailingAddresses.PostalCode;
            }
            else
            {
                ShippingAddresses.Regions = _context.Regions.Where(r => r.ID == ShippingAddresses.RegionsID).FirstOrDefault();
            }

            _context.Update(ShippingAddresses);
            _context.SaveChanges();
            _context.Addresses.Update(Addresses);
            _context.SaveChanges();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your address information have been updated";
            _logger.LogInformation("User changed their address information successfully.");
            return RedirectToPage();
        }
    }
}

        
        /*public async Task<bool> ValidMailingAddress()
        {
            bool valid = true;
            if (MailingAddresses.Line1 == null)
            {
                ModelState.AddModelError("MailingAddress.Line1", "Please enter address line 1");
                valid = false;
            }

            if (MailingAddresses.RegionsID == 0)
            {
                ModelState.AddModelError("MailingAddress.RegionID", "Please choose a region");
                valid = false;
            }

            if (System.String.IsNullOrEmpty(MailingAddresses.City))
            {
                ModelState.AddModelError("MailingAddress.City", "Please enter a city");
                valid = false;
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
                            ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a valid Postal Code");
                            valid = false;
                        }
                    }
                    else if (Input.SelectedMailingCountryID == 2)
                    {
                        regex = new Regex("^\\d{5}(-{0,1}\\d{4})?$");
                        if (!regex.Match(MailingAddresses.PostalCode).Success)
                        {
                            ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a valid Zip Code");
                            valid = false;
                        }
                    }
                }
            }
            else
            {
                if (Input.SelectedMailingCountryID == 1)
                {
                    ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a Postal Code");
                    valid = false;
                }
                else if (Input.SelectedMailingCountryID == 2)
                {
                    ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a Zip Code");
                    valid = false;
                }
            }
            return valid;
        }


        public async Task<bool> ValidShippingAddress()
        {
            bool valid = true;
            if (ShippingAddresses.Line1 == null)
            {
                ModelState.AddModelError("ShippingAddress.Line1", "Please enter address line 1");
                valid = false;
            }

            if (ShippingAddresses.RegionsID == 0)
            {
                ModelState.AddModelError("ShippingAddress.RegionID", "Please choose a region");
                valid = false;
            }

            if (System.String.IsNullOrEmpty(ShippingAddresses.City))
            {
                ModelState.AddModelError("ShippingAddress.City", "Please enter a city");
                valid = false;
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
                            ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a valid Postal Code");
                            valid = false;
                        }
                    }
                    else if (Input.SelectedShippingCountryID == 2)
                    {
                        regex = new Regex("^\\d{5}(-{0,1}\\d{4})?$");
                        if (!regex.Match(ShippingAddresses.PostalCode).Success)
                        {
                            ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a valid Zip Code");
                            valid = false;
                        }
                    }
                }
            }
            else
            {
                if (Input.SelectedShippingCountryID == 1)
                {
                    ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a Postal Code");
                    valid = false;
                }
                else if (Input.SelectedShippingCountryID == 2)
                {
                    ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a Zip Code");
                    valid = false;
                }
            }
            return valid;
        }
    }
}
*/