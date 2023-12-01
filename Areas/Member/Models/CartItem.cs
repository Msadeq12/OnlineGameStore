using PROG3050_HMJJ.Areas.Admin.Models;

namespace PROG3050_HMJJ.Areas.Member.Models
{

    public class CartItem
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string OrderType { get; set; }  // Added this to indicate the type of order ("Physical" or "Digital")
        public bool IsPurchased { get; set; }
        public int TotalPrice { get { return Price * Quantity; } }

    }
}
