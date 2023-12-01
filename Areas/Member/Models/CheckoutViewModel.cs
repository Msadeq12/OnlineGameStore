namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem>? CartItems { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
