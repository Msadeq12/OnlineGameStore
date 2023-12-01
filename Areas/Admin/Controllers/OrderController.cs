using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models.DataAccess;

namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly GameStoreDbContext _context;
        public OrderController(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OrderDetail()
        {
            
            var userOrders = await _context.Orders
                                           .Include(o => o.Invoice) 
                                           .Where(o => o.Status=="Now Processing")
                                           .ToListAsync();

            return View(userOrders);
        }
        public async Task<IActionResult> UpdateOrderStatus(int gameID)
        {
            var order = await _context.Orders.FindAsync(gameID);
            if (order != null && order.OrderType == "Physical")
            {
                order.Status = "Processed";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("OrderDetail"); // Redirect to the list of orders
        }

    }
}
