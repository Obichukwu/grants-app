using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eWallet.Areas.Farmers.Models
{
    public class MyWalletIndexModel
    {
        public string Title { get; set; }
        public decimal AvailableAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
    }
}