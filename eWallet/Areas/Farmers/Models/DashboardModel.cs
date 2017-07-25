using eWallet.Models;
using System.Collections.Generic;

namespace eWallet.Areas.Farmers.Models {
    public class DashboardModel
    {
        public int GrantCount { get; set; }
        public int PendingGrantCount { get; set; }

        public decimal GrantSpent { get; set; }
        public decimal GrantBalance { get; set; }

        public decimal GeneralBalance { get; set; }

        public int CompletedOrdersCount { get; set; }

        public List<FarmerGrant> ApprovedGrants { get; set; }
        public List<FarmerGrant> PendingGrants { get; internal set; }
    }
}