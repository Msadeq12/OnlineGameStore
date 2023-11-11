using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Models.DataAccess;

namespace PROG3050_HMJJ.Services
{
    public class ReviewService
    {
        private readonly GameStoreDbContext _context;
        public bool LeaveComment(Reviews reviews)
        {
            _context.Add(reviews);
            return _context.SaveChanges() > 0;

        }
    }
}
