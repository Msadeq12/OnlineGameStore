namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Adjust the type according to your user identifier
        public DateTime InvoiceDate { get; set; }
        public decimal Bill { get; set; }
        public string PaymentMethod { get; set; }

    }
}
