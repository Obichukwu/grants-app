using eWallet.Models;
using System.Collections.Generic;

namespace eWallet.Areas.Admin.Models {
    public class DashboardModel
    {
        public int GrantCount { get; set; }
        public int TerminatedGrantCount { get; set; }

        public int AgentCount { get; set; }
        public int PendingAgentCount { get; set; }

        public int FarmerCount { get; set; }
        public int PendingFarmerCount { get; set; }

        public List<Grant> ApprovedGrants { get; set; }

        public List<Grant> TerminatedGrants { get; set; }

    }
}