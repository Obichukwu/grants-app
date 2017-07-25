using eWallet.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace eWallet.Areas.Farmers.Models {
    public class BidApplyModel
    {
        public int GrantId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MonetaryValue { get; set; }
    }
}