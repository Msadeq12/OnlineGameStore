using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Models.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Loader;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROG3050_HMJJ.Areas.Identity.Pages.Account.Manage
{
    public class AddressModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GameStoreDbContext _context;

        public AddressModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            GameStoreDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty(SupportsGet = true)]
        public Address MailingAddress { get; set; }


        public SelectList MailingCountryList { get; set; }


        public SelectList MailingRegionList { get; set; }


        [Display(Name = "Country")]
        [BindProperty(SupportsGet = true)]
        public int MailingCountryID { get; set; }


        [BindProperty(SupportsGet = true)]
        public Address ShippingAddress { get; set; }


        public SelectList ShippingCountryList { get; set; }


        public SelectList ShippingRegionList { get; set; }


        [Display(Name = "Country")]
        [BindProperty(SupportsGet = true)]
        public int ShippingCountryID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var mailingAddress = await _context.Address.Where(a => a.AddressType == "mailing").FirstOrDefaultAsync(a => a.User.Id == user.Id);
            Address shippingAddress;
            Regions mailingRegion = new Regions();
            Regions shippingRegion = new Regions();

            if (mailingAddress == null)
            {
                mailingAddress = new Address();
                mailingAddress.User = user;
                mailingAddress.RegionID = 0;
            }
            else
            {
                mailingRegion = _context.Regions.Find(mailingAddress.RegionID);
            }

            if (mailingAddress.SameAddress == false)
            {
                shippingAddress = await _context.Address.Where(a => a.AddressType == "shipping").FirstOrDefaultAsync(a => a.User.Id == user.Id);

                if(shippingAddress == null)
                {
                    shippingAddress = new Address();
                    shippingAddress.User = user;
                    shippingAddress.RegionID = 0;
                }
                else
                {
                    shippingRegion = _context.Regions.Find(shippingAddress.RegionID);
                }
            }
            else
            {
                shippingAddress = new Address();
                shippingAddress.User = user;
            }

            MailingAddress = mailingAddress;
            ShippingAddress = shippingAddress;

            var countries = await _context.Countries.ToListAsync();

            countries.Insert(0, new Countries { ID = 0, Name = "" });


            if (MailingAddress.RegionID != 0)
            {
                MailingCountryID = mailingRegion.CountryID;
            }
            else
            {
                MailingCountryID = 0;
            }

            if(ShippingAddress.RegionID != 0)
            {
                ShippingCountryID = shippingRegion.CountryID;
            }
            else
            {
                ShippingCountryID = 0;
            }

            MailingCountryList = new SelectList(countries, "ID", "Name", MailingCountryID);
            ShippingCountryList = new SelectList(countries, "ID", "Name", ShippingCountryID);

            SetRegionAndCodeLabels();
            SelectRegions();

            if (MailingAddress.RegionID != 0)
            {
                MailingRegionList.ElementAt(MailingAddress.RegionID).Selected = true;
            }

            if(ShippingAddress.RegionID != 0)
            {
                ShippingRegionList.ElementAt(ShippingAddress.RegionID).Selected = true;
            }

            return Page();
        }

        public void SelectRegions()
        {
            List<Regions> mailingRegions = _context.Regions.Where(r => r.CountryID == MailingCountryID).ToList();

            mailingRegions.Insert(0, new Regions { ID = 0, Name = "" });
            MailingRegionList = new SelectList(mailingRegions, "ID", "Name");

            MailingRegionList.ElementAt(0).Selected = true;

            List<Regions> shippingRegions = _context.Regions.Where(r => r.CountryID == ShippingCountryID).ToList();

            shippingRegions.Insert(0, new Regions { ID = 0, Name = "" });
            ShippingRegionList = new SelectList(shippingRegions, "ID", "Name");

            ShippingRegionList.ElementAt(0).Selected = true;
        }

        public void SetRegionAndCodeLabels()
        {
            if (MailingCountryID == 0)
            {
                ViewData["MailingRegion"] = "Region";
                ViewData["MailingCode"] = "Address Code";
            }
            else if (MailingCountryID == 1)
            {
                ViewData["MailingRegion"] = "Province/Territory";
                ViewData["MailingCode"] = "Postal Code";
            }
            else if (MailingCountryID == 2)
            {
                ViewData["MailingRegion"] = "State/Territory";
                ViewData["MailingCode"] = "Zip Code";
            }

            if (ShippingCountryID == 0)
            {
                ViewData["ShippingRegion"] = "Region";
                ViewData["ShippingCode"] = "Address Code";
            }
            else if (ShippingCountryID == 1)
            {
                ViewData["ShippingRegion"] = "Province/Territory";
                ViewData["ShippingCode"] = "Postal Code";
            }
            else if (ShippingCountryID == 2)
            {
                ViewData["ShippingRegion"] = "State/Territory";
                ViewData["ShippingCode"] = "Zip Code";
            }
        }

        public async Task<IActionResult> OnPostRegions(string addressType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (addressType.Equals("shipping"))
            {
                var mailingAddress = await _context.Address.Where(a => a.AddressType == "mailing").FirstOrDefaultAsync(a => a.User.Id == user.Id);
                Regions mailingRegion = new Regions();

                if (mailingAddress == null)
                {
                    mailingAddress = new Address();
                    mailingAddress.User = user;
                    mailingAddress.RegionID = 0;
                }
                else
                {
                    mailingRegion = _context.Regions.Find(mailingAddress.RegionID);
                }

                MailingAddress = mailingAddress;

                if (MailingAddress.RegionID != 0)
                {
                    MailingCountryID = mailingRegion.CountryID;
                }
                else
                {
                    MailingCountryID = 0;
                }
            }
            else if (addressType.Equals("mailing"))
            {
                Address shippingAddress = await _context.Address.Where(a => a.AddressType == "shipping").FirstOrDefaultAsync(a => a.User.Id == user.Id);
                Regions shippingRegion = new Regions();

                if (shippingAddress == null)
                {
                    shippingAddress = new Address();
                    shippingAddress.User = user;
                    shippingAddress.RegionID = 0;
                }
                else
                {
                    shippingRegion = _context.Regions.Find(shippingAddress.RegionID);
                }

                ShippingAddress = shippingAddress;

                if (ShippingAddress.RegionID != 0)
                {
                    ShippingCountryID = shippingRegion.CountryID;
                }
                else
                {
                    ShippingCountryID = 0;
                }
            }

            var countries = _context.Countries.ToList();
            countries.Insert(0, new Countries { ID = 0, Name = "" });
            MailingCountryList = new SelectList(countries, "ID", "Name", MailingCountryID);
            ShippingCountryList = new SelectList(countries, "ID", "Name", ShippingCountryID);

            SetRegionAndCodeLabels();
            SelectRegions();

            if (addressType.Equals("shipping"))
            {
                if (MailingAddress.RegionID != 0)
                {
                    MailingRegionList.ElementAt(MailingAddress.RegionID).Selected = true;
                }
            }
            else if (addressType.Equals("mailing"))
            {
                if (ShippingAddress.RegionID != 0)
                {
                    ShippingRegionList.ElementAt(ShippingAddress.RegionID).Selected = true;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostMailing()
        {
            if(MailingAddress.Line1 == null)
            {
                ModelState.AddModelError("MailingAddress.Line1", "Please enter address line 1");
            }

            if (MailingAddress.RegionID == 0)
            {
                ModelState.AddModelError("MailingAddress.RegionID", "Please choose a region");
            }

            if (System.String.IsNullOrEmpty(MailingAddress.City))
            {
                ModelState.AddModelError("MailingAddress.City", "Please enter a city");
            }

            if (!System.String.IsNullOrWhiteSpace(MailingAddress.PostalCode))
            {
                if (MailingCountryID != 0)
                {
                    Regex regex;
                    if (MailingCountryID == 1)
                    {
                        regex = new Regex("^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] [0-9][ABCEGHJKMNPRSTVWXYZ][0-9]$");
                        if (!regex.Match(MailingAddress.PostalCode).Success)
                        {
                            ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a valid Postal Code");
                        }
                    }
                    else if (MailingCountryID == 2)
                    {
                        regex = new Regex("^\\d{5}(-{0,1}\\d{4})?$");
                        if (!regex.Match(MailingAddress.PostalCode).Success)
                        {
                            ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a valid Zip Code");
                        }
                    }
                }
            }
            else
            {
                if(MailingCountryID == 1)
                {
                    ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a Postal Code");
                }
                else if (MailingCountryID == 2)
                {
                    ModelState.AddModelError("MailingAddress.PostalCode", "Please enter a Zip Code");
                }
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            MailingAddress.User = user;

            Address shippingAddress = await _context.Address.AsNoTracking().Where(a => a.AddressType == "shipping").FirstOrDefaultAsync(a => a.User.Id == user.Id);
            Regions shippingRegion = new Regions();

            if (shippingAddress == null)
            {
                shippingAddress = new Address();
                shippingAddress.User = user;
                shippingAddress.RegionID = 0;
            }
            else
            {
                shippingRegion = _context.Regions.Find(shippingAddress.RegionID);
            }

            ShippingAddress = shippingAddress;

            if (ShippingAddress.RegionID != 0)
            {
                ShippingCountryID = shippingRegion.CountryID;
            }
            else
            {
                ShippingCountryID = 0;
            }

            var countries = _context.Countries.ToList();
            countries.Insert(0, new Countries { ID = 0, Name = "" });
            MailingCountryList = new SelectList(countries, "ID", "Name", MailingCountryID);
            ShippingCountryList = new SelectList(countries, "ID", "Name", ShippingCountryID);

            SetRegionAndCodeLabels();
            SelectRegions();

            if (ShippingAddress.RegionID != 0)
            {
                ShippingRegionList.ElementAt(ShippingAddress.RegionID).Selected = true;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }


            MailingAddress.AddressType = "mailing";

            if (MailingAddress.SameAddress)
            {
                shippingAddress = await _context.Address.Where(a => a.AddressType == "shipping").FirstOrDefaultAsync(a => a.User.Id == user.Id);

                if (shippingAddress != null)
                {
                    _context.Address.Remove(shippingAddress);
                    _context.SaveChanges();
                }
            }

            Address address = await _context.Address.AsNoTracking().Where(a => a.AddressType == "mailing").FirstOrDefaultAsync(a => a.User.Id == user.Id);

            if(address == null)
            {
                _context.Address.Add(MailingAddress);
                _context.SaveChanges();
            }
            else
            {
                _context.Address.Update(MailingAddress);
                _context.SaveChanges();
            }

            StatusMessage = "Your mailing address has been updated";

            return Page();
        }

        public async Task<IActionResult> OnPostShipping()
        {
            if (ShippingAddress.Line1 == null)
            {
                ModelState.AddModelError("ShippingAddress.Line1", "Please enter address line 1");
            }

            if (ShippingAddress.RegionID == 0)
            {
                ModelState.AddModelError("ShippingAddress.RegionID", "Please choose a region");
            }

            if (System.String.IsNullOrEmpty(ShippingAddress.City))
            {
                ModelState.AddModelError("ShippingAddress.City", "Please enter a city");
            }

            if (!System.String.IsNullOrWhiteSpace(ShippingAddress.PostalCode))
            {
                if (ShippingCountryID != 0)
                {
                    Regex regex;
                    if (ShippingCountryID == 1)
                    {
                        regex = new Regex("^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] [0-9][ABCEGHJKMNPRSTVWXYZ][0-9]$");
                        if (!regex.Match(ShippingAddress.PostalCode).Success)
                        {
                            ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a valid Postal Code");
                        }
                    }
                    else if (ShippingCountryID == 2)
                    {
                        regex = new Regex("^\\d{5}(-{0,1}\\d{4})?$");
                        if (!regex.Match(ShippingAddress.PostalCode).Success)
                        {
                            ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a valid Zip Code");
                        }
                    }
                }
            }
            else
            {
                if (ShippingCountryID == 1)
                {
                    ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a Postal Code");
                }
                else if (ShippingCountryID == 2)
                {
                    ModelState.AddModelError("ShippingAddress.PostalCode", "Please enter a Zip Code");
                }
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ShippingAddress.User = user;

            var mailingAddress = await _context.Address.AsNoTracking().Where(a => a.AddressType == "mailing").FirstOrDefaultAsync(a => a.User.Id == user.Id);
            Regions mailingRegion = new Regions();

            if (mailingAddress == null)
            {
                mailingAddress = new Address();
                mailingAddress.User = user;
                mailingAddress.RegionID = 0;
            }
            else
            {
                mailingRegion = _context.Regions.Find(mailingAddress.RegionID);
            }

            MailingAddress = mailingAddress;

            if (MailingAddress.RegionID != 0)
            {
                MailingCountryID = mailingRegion.CountryID;
            }
            else
            {
                MailingCountryID = 0;
            }

            var countries = _context.Countries.ToList();
            countries.Insert(0, new Countries { ID = 0, Name = "" });
            MailingCountryList = new SelectList(countries, "ID", "Name", MailingCountryID);
            ShippingCountryList = new SelectList(countries, "ID", "Name", ShippingCountryID);

            SetRegionAndCodeLabels();
            SelectRegions();

            if (MailingAddress.RegionID != 0)
            {
                MailingRegionList.ElementAt(MailingAddress.RegionID).Selected = true;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Address address = new Address();

            if (ShippingAddress.SameAddress)
            {
                ShippingAddress.AddressType = "mailing";

                MailingAddress = ShippingAddress;
                MailingCountryID = ShippingCountryID;

                address = _context.Address.AsNoTracking().Where(a => a.AddressType == "mailing").FirstOrDefault(a => a.User.Id == user.Id);

                if(address != null)
                {
                    ShippingAddress.ID = address.ID;
                }

                Address shippingAddress = _context.Address.Where(a => a.AddressType == "shipping").FirstOrDefault(a => a.User.Id == user.Id);

                if(shippingAddress != null)
                {
                    _context.Address.Remove(shippingAddress);
                    _context.SaveChanges();
                }
            }
            else
            {
                ShippingAddress.AddressType = "shipping";

                address = _context.Address.AsNoTracking().Where(a => a.AddressType == "shipping").FirstOrDefault(a => a.User.Id == user.Id);
            }

            if (address == null)
            {
                _context.Address.Add(ShippingAddress);
                _context.SaveChanges();
            }
            else
            {
                _context.Address.Update(ShippingAddress);
                _context.SaveChanges();
            }

            if (ShippingAddress.SameAddress)
            {
                StatusMessage = "Your mailing address has been updated";
            }
            else
            {
                StatusMessage = "Your shipping address has been updated";
            }

            return Page();
        }
    }
}
