namespace eWallet.Areas.Farmers.Models
{
    public class ProductBuyModel
    {
        public string AgentId { get; set; }

        public decimal GrantBalance { get; set; }
        public decimal MultiPurposeWalletBalance { get; set; }

        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
    }
}