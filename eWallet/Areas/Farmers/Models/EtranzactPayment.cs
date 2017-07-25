namespace eWallet.Areas.Farmers.Models
{
    public class EtranzactPayment
    {
        public string CheckSum { get; set; }
        public string ResponseUrl { get; set; }
        public string SecretKey { get; set; }
        public string TerminalId { get; set; }
        public string Url { get; set; }

        public string InvoiceTitle { get; set; }
        public string TransactionId { get; set; }
        public decimal TotalAmount { get; set; }

        public string Email { get; set; }
    }
}