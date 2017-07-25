using System.Data.Entity;

namespace eWallet.Models {
    public class FarmerGrant {
        public string FarmerId { get; set; }
        public int GrantId { get; set; }

        public decimal ApprovedAmount { get; set; }
        public decimal AvailableAmount { get; set; }

        public FarmerGrantStatus Status { get; set; }
        public string Reason { get; set; }

        public virtual Farmer Farmer { get; set; }
        public virtual Grant Grant { get; set; }

        public static void Configure(DbModelBuilder builder)
        {
            var farmerGrant = builder.Entity<FarmerGrant>();

            farmerGrant.Property(pr => pr.Reason).IsOptional();

            farmerGrant.HasKey(el => new { el.FarmerId, el.GrantId });
        }
    }

    public enum FarmerGrantStatus
    {
        InProgress, Approved, Rejected, Revoked
    }
}