
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Diagnostics;
using PROG3050_HMJJ.Models.DataAccess;
using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Models.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using PROG3050_HMJJ.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace PROG3050_HMJJ.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Admin, Member")]
    public sealed class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly UserManager<User> _userManager;
        private readonly GameStoreDbContext _context;


        public HomeController(GameStoreDbContext context, UserManager<User> manger)
        {
            _context = context;
            _client = new HttpClient();
            _userManager = manger;
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
                if(games != null)
                {
                    foreach(var game in games)
                    {
                        game.AverageRating = _context.Ratings.Where(r => r.GameID == game.ID)
                                             .Average(r => r.Value);
                    }
                }
            }

            else
            {
                games = null;
            }

            return View(games);
        }


        [HttpGet]
        public IActionResult Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index");
            }

            string url = $"https://localhost:7108/api/game/search?search={searchString}";

            HttpResponseMessage searchResponse = _client.GetAsync(url).Result;
            Console.WriteLine("Search status code: " + searchResponse.StatusCode);
            Console.WriteLine("URL: " + url);
            List<GamesViewModel>? games;

            if (searchResponse.IsSuccessStatusCode)
            {
                games = searchResponse.Content.ReadFromJsonAsync<List<GamesViewModel>>().Result;
                if (games != null)
                {
                    foreach (var game in games)
                    {
                        game.AverageRating = _context.Ratings.Where(r => r.GameID == game.ID)
                                             .Average(r => r.Value);
                    }
                }
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
                if (game != null)
                {
                    // Initialize the NewReview property with a new Reviews object
                    game.NewReview = new Reviews() { GameId = id };
                    game.ApprovedReviews = _context.Reviews.Where(r => r.GameId == id && r.IsApproved == true)
                                             .ToList();
                    game.NewRating = new Ratings() { GameID = id };
                    game.Ratings = _context.Ratings.Where(r => r.GameID == id)
                                             .ToList();
                    game.AverageRating = _context.Ratings.Where(r => r.GameID == id)
                                             .Average(r => r.Value);
                    ViewBag.CurrentUsername = User?.Identity.Name ?? string.Empty;

                    bool inWishList = IsInWishList(id).Result;

                    ViewBag.InWishList = inWishList;
                }
            }
            else
            {
                game = null;
            }

            return View(game);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitReview(GamesViewModel model)
        {
            foreach (var key in ModelState.Keys)
            {
                var value = ModelState[key];
                
                Console.WriteLine($"Key: {key}, Errors: {value.Errors.Count}, Value: {value.AttemptedValue}");
            }
            // Disable validation for unrelated fields
            var unrelatedFields = new[] { "Title", "GameGenre", "Publisher", "Description", "ReleaseYear", "OrderType", "GamePlatform", "CommentId", "UserId", "ApprovedReviews", "NewRating", "Ratings" };
            foreach (var field in unrelatedFields)
            {
                ModelState.Remove(field);
            }
            //string fieldName = "Title";
            //ModelState.ClearValidationState(fieldName);

            //// Set the model value for the field to its current value (or updated one if needed)
            //var currentValue = ModelState[fieldName]?.RawValue;
            //var attemptedValue = ModelState[fieldName]?.AttemptedValue;
            //ModelState.SetModelValue(fieldName, new ValueProviderResult(attemptedValue, CultureInfo.CurrentCulture));
            if (ModelState.IsValid)
            {
                var review = new Reviews
                {
                    Timestamp = DateTime.Now,
                    IsApproved = null,
                    UserId = model.NewReview.UserId,
                    GameName = model.NewReview.GameName,
                    CommentText = model.NewReview.CommentText,
                    GameId = model.NewReview.GameId,
                    CommentId = model.NewReview.CommentId
                };
                // Add the review to the database context
                _context.Reviews.Add(review);

                // Save changes asynchronously
                await _context.SaveChangesAsync();
                TempData["ReviewMessage"] = "Review submitted successfully, will be shown once approved by an admin.";
                // Redirect to the game's details page after submission
                return RedirectToAction("Details", "Home", new { area = "Member", id = review.GameId });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Log the error message
                    Console.WriteLine(error.ErrorMessage);
                }

                // If the model state is not valid, you might want to return to the form with validation messages
                // For simplicity, redirecting back to the same game's details page
                return RedirectToAction("Details", "Home", new { area = "Member", id = model.NewReview.GameId });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SubmitRating(GamesViewModel model)
        {
            foreach (var key in ModelState.Keys)
            {
                var value = ModelState[key];

                Console.WriteLine($"Key: {key}, Errors: {value.Errors.Count}, Value: {value.AttemptedValue}");
            }
            // Disable validation for unrelated fields
            var unrelatedFields = new[] { "Title", "Ratings", "GameGenre", "NewReview", "Publisher", "OrderType", "Description", "ReleaseYear", "GamePlatform", "ApprovedReviews" };
            foreach (var field in unrelatedFields)
            {
                ModelState.Remove(field);
            }
                
            //// Set the model value for the field to its current value (or updated one if needed)
            if (ModelState.IsValid)
            {
                // Check if rating exist for user and game currently
                // (i.e. Determine if we should add or update the rating)
                Ratings? rating = _context.Ratings
                                        .Where(r => r.UserName == model.NewRating.UserName)
                                        .Where(r => r.GameID == model.NewRating.GameID)
                                        .FirstOrDefault();

                if (rating == null)
                {
                    // Add new rating
                    rating = new Ratings
                    {
                        UserName = model.NewRating.UserName,
                        GameID = model.NewRating.GameID,
                        Value = model.NewRating.Value
                    };

                    // Add the rating to the database context
                    _context.Ratings.Add(rating);              
                }
                else
                {
                    // Update existing rating
                    rating.Value = model.NewRating.Value;
                    _context.Ratings.Update(rating);
                }
                // Save changes asynchronously
                await _context.SaveChangesAsync();
                TempData["RatingsMessage"] = "Rating submitted successfully.";

                // Redirect to the game's details page after submission
                return RedirectToAction("Details", "Home", new { area = "Member", id = rating.GameID });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Log the error message
                    Console.WriteLine(error.ErrorMessage);
                }

                // If the model state is not valid, you might want to return to the form with validation messages
                // For simplicity, redirecting back to the same game's details page
                return RedirectToAction("Details", "Home", new { area = "Member", id = model.NewRating.GameID });
            }
        }


        public async Task<bool> IsInWishList(int gameID)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                WishLists wishList = await _context.WishLists.Include(w => w.WishListItems).Where(w => w.User.Id == user.Id).FirstOrDefaultAsync();

                if (wishList != null)
                {
                    WishListItems item = wishList.WishListItems.SingleOrDefault(wi => wi.GameID == gameID);
                    if (item != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Gets Details of the event you're registered for</returns>
        [HttpGet]
        public ViewResult GetEvents()
        {
            var user = User.Identity.Name;
            var registerations = _context.EventRegistration.Where(r => r.UserId == user).ToList();
            var eventIDs = registerations.Select(r => r.eventID).ToList();

            List<Event> myEvents = new List<Event>();

            foreach(var id in eventIDs)
            {
                string url = $"https://localhost:7193/events/{id}";

                HttpResponseMessage response = _client.GetAsync(url).Result;
                Event? events;

                if (response.IsSuccessStatusCode)
                {
                    events = response.Content.ReadFromJsonAsync<Event>().Result;
                }
                else
                {
                    events = new Event();
                }

                myEvents.Add(events);
            }
            
            return View(myEvents);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>Adds the Event to EventRegisteration table under the username</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterEvent(int id)
        {
            //var user = await _userManager.GetUserAsync(User);
            var myRegistrations = _context.EventRegistration.Where(r => r.UserId == User.Identity.Name).ToList();
            
            if(myRegistrations.Any(r => r.eventID == id))
            {
                TempData["EventAlready"] = "Already registered to that event.";
                return RedirectToAction("Index");
            }

            EventRegister newRegistration = new()
            {
                eventID = id,
                UserId = User.Identity.Name
            };

            await _context.EventRegistration.AddAsync(newRegistration);
            await _context.SaveChangesAsync();

            TempData["EventSaved"] = "Your Registration is complete!";

            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>Removes event reference from the EventRegistration table</returns>
        [HttpPost]
        public IActionResult DeleteEvent(int id)
        {
            var myRegistrations = _context.EventRegistration.Where(r => r.UserId == User.Identity.Name).ToList();

            var registeration = myRegistrations.Where(r => r.eventID == id).FirstOrDefault();
                       
            if (registeration == null)
            {
                TempData["Cancelled"] = "Event not found";
                return RedirectToAction("GetEvents");
            }

            _context.EventRegistration.RemoveRange(registeration);
            _context.SaveChanges();
            TempData["Cancelled"] = "Registration has been cancelled";

            return RedirectToAction("GetEvents");
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

        //[HttpGet]
        //public IActionResult AddToCart()
        //{
        //    // Redirect to the Index action of the current controller
        //    return RedirectToAction("Index");
        //}
        //
        public ActionResult AddToCart(int id, int qty)
        {
            string url = $"https://localhost:7108/api/game/{id}";
            HttpResponseMessage response = _client.GetAsync(url).Result;
            GamesViewModel game;

            if (response.IsSuccessStatusCode)
            {
                game = response.Content.ReadFromJsonAsync<GamesViewModel>().Result;
            }
            else
            {
                // Handle the error appropriately
                return View("Error");
            }
            qty = 1;
            CartItem cartItem = new CartItem
            {
                GameId = game.ID,
                GameName = game.Title,
                Price = game.Price,
                Quantity = qty,
                OrderType = game.OrderType, // Set the OrderType from the game details
                IsPurchased = false // Initially, the game is not purchased
            };

            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var existingItem = cart.Find(x => x.GameId == id);

            if (existingItem != null)
            {
                existingItem.Quantity += qty;
            }
            else
            {
                cart.Add(cartItem);
            }

            HttpContext.Session.SetObjectAsJson("cart", cart);

            return RedirectToAction("Index", "Home", new { area = "Member" });
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var grandTotal = cartItems.Sum(item => item.TotalPrice);

            var viewModel = new CheckoutViewModel
            {
                CartItems = cartItems,
                PaymentInfo = new PaymentInfo(), // Initialize empty payment info
                GrandTotal = grandTotal
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
                if (cartItems != null && cartItems.Any())
                {
                    // Create and save the invoice
                    var invoice = new Invoice
                    {
                        UserId = User?.Identity.Name ?? string.Empty, // Or however you get the user ID
                        InvoiceDate = DateTime.Now,
                        Bill = cartItems.Sum(item => item.TotalPrice),
                        PaymentMethod = "Card"
                    };
                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync();

                    // Create and save each order
                    foreach (var item in cartItems)
                    {
                        
                        var order = new Order
                        {
                            GameId = item.GameId,
                            GameName = item.GameName,
                            Quantity = item.Quantity,
                            UnitPrice = item.Price,
                            TotalPrice = item.TotalPrice,
                            OrderDate = DateTime.Now,
                            OrderType = item.OrderType,
                            Status = item.OrderType == "Physical" ? "Now Processing" : "Processed",
                            IsPurchased = true,
                            InvoiceId = invoice.Id // Use the Invoice Id
                        };

                        _context.Orders.Add(order);
                    }
                    await _context.SaveChangesAsync();

                    // Clear the session cart
                    HttpContext.Session.Remove("cart");

                    var confirmationViewModel = new OrderConfirmationViewModel
                    {
                        InvoiceDetails = invoice,
                        OrderItems = await _context.Orders
                                       .Where(o => o.InvoiceId == invoice.Id)
                                       .ToListAsync()
                    };

                    // Use TempData or a similar mechanism to pass data to the redirection target
                    TempData["OrderConfirmation"] = JsonConvert.SerializeObject(confirmationViewModel);


                    // Redirect to an order confirmation page
                    return RedirectToAction("OrderConfirmation");
                }
            }
            else
            {
                viewModel.CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
                viewModel.GrandTotal = viewModel.CartItems.Sum(item => item.TotalPrice);
            }

            // If model state is not valid, or cart is empty, return to the same view
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult OrderConfirmation()
        {
            if (TempData["OrderConfirmation"] is string serializedConfirmationViewModel)
            {
                var viewModel = JsonConvert.DeserializeObject<OrderConfirmationViewModel>(serializedConfirmationViewModel);
                return View(viewModel);
            }

            // Redirect to a different page or show an error if the order details are not available
            return RedirectToAction("Index"); // or return View("Error");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int gameId)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            var itemToUpdate = cartItems.FirstOrDefault(item => item.GameId == gameId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity -= 1;
                if (itemToUpdate.Quantity <= 0)
                {
                    cartItems.Remove(itemToUpdate);
                }

                HttpContext.Session.SetObjectAsJson("cart", cartItems);
            }

            return RedirectToAction("Checkout");
        }

        public async Task<IActionResult> OrderDetail()
        {
            var userId = User?.Identity.Name ?? string.Empty;

            var userOrders = await _context.Orders
                                           .Include(o => o.Invoice) // Include the Invoice in the query
                                           .Where(o => o.Invoice.UserId == userId)
                                           .ToListAsync();

            return View(userOrders);
        }

        public async Task<IActionResult> libraries()
        {
            var userId = User?.Identity.Name ?? string.Empty;

            var userOrders = await _context.Orders
                                           .Include(o => o.Invoice) // Include the Invoice in the query
                                           .Where(o => o.Invoice.UserId == userId)
                                           .Where(o => o.OrderType== "Digital")
                                           .ToListAsync();

            return View(userOrders);
        }

        public async Task<IActionResult> DownloadGame(int gameID)
        {
            // Fetch the game details based on gameId
            var game = await _context.Orders.FindAsync(gameID);
            if (game == null)
            {
                return NotFound(); // Or handle the case where the game isn't found
            }

            // Generate the content for the text file
            string fileContent = $"Game Name: {game.GameName}\nFormat: {game.OrderType}\nPrice: {game.TotalPrice}";

            // Convert the string content to a byte array
            var byteArray = Encoding.UTF8.GetBytes(fileContent);

            // Set the file name dynamically based on the game name
            var fileName = $"{game.GameName}.txt";

            return File(byteArray, "text/plain", fileName);
        }
    }
}