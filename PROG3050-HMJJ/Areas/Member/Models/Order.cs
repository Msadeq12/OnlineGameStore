using System.ComponentModel.DataAnnotations.Schema;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Order
    {

        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }
        public bool IsPurchased { get; set; }
        public string Status { get; set; } // "Processed", "Unprocessed"
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; } // Foreign key to Invoice
        public Invoice Invoice { get; set; }

    }
}
