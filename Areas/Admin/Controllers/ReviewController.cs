using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models.DataAccess;

namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReviewController : Controller
    {
        private readonly GameStoreDbContext _context;

        public ReviewController(GameStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Reviews()
        {
            var reviews = await _context.Reviews
                                        .Where(r => r.IsApproved==null)
                                        .ToListAsync();
            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReview(string reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review != null)
            {
                review.IsApproved = true;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Review approved successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "No Reviews were found to approve.";
            }
            return RedirectToAction("Reviews");
        }

        [HttpPost]
        public async Task<IActionResult> DisapproveReview(string reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review != null)
            {
                review.IsApproved = false;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Review disapproved successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "No Reviews were found to disapprove.";
            }
            return RedirectToAction("Reviews");
        }
    }
}
