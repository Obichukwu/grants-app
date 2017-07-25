using eWallet.Models;
using System.Data.Entity;

namespace eWallet.Models {
    public class AgentGrant
    {
        public string AgentId { get; set; }
        public int GrantId { get; set; }

        public AgentGrantStatus Status { get; set; }

        public decimal Balance { get; set; }
        public decimal PendingBalance { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Grant Grant { get; set; }

        public static void Configure(DbModelBuilder builder) {
            var agentGrant = builder.Entity<AgentGrant>();

            agentGrant.HasKey(el => new {el.AgentId, el.GrantId});
        }
    }

    public enum AgentGrantStatus
    {
        Active, Suspended, InProgress, Rejected
    }
}