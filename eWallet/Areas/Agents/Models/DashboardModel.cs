using eWallet.Models;
using System.Collections.Generic;

namespace eWallet.Areas.Agents.Models {
    public class DashboardModel
    {
        public int GrantCount { get; set; }
        public int PendingGrantCount { get; set; }

        public decimal GrantSold { get; set; }
        public decimal PendingWithdrawal { get; set; }

        public int CompletedOrdersCount { get; set; }

        public List<AgentGrant> ApprovedGrants { get; set; }
        public List<AgentGrant> PendingGrants { get; internal set; }
    }
}