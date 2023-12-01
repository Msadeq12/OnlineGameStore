namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class OrderConfirmationViewModel
    {
        public Invoice InvoiceDetails { get; set; }
        public List<Order> OrderItems { get; set; }
    }
}
