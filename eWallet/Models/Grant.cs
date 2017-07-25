using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace eWallet.Models {
    public class Grant {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MonetaryWorth { get; set; }
        public GrantStatus Status { get; set; }
        public virtual ICollection<Product> Products { get; set; }=
            new HashSet<Product>();

        public virtual ICollection<FarmerGrant> Farmers { get; set; } =
            new HashSet<FarmerGrant>();

        public virtual ICollection<AgentGrant> Agents { get; set; } =
            new HashSet<AgentGrant>();

        public static void Configure(DbModelBuilder builder)
        {
            var grant = builder.Entity<Grant>();

            grant.HasKey(pr => pr.Id);
            grant.Property(pr => pr.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            grant.Property(pr => pr.Title).IsRequired().HasMaxLength(50);
            grant.Property(pr => pr.Description).IsRequired().HasMaxLength(50);

            grant.HasMany(el => el.Products)
                .WithRequired(el => el.Grant)
                .HasForeignKey(el => el.GrantId);
            grant.HasMany(el => el.Agents)
                .WithRequired(el => el.Grant)
                .HasForeignKey(el => el.GrantId);
            grant.HasMany(el => el.Farmers)
                .WithRequired(el => el.Grant)
                .HasForeignKey(el => el.GrantId);
        }
    }
    public enum GrantStatus
    {
        Published, Terminated, Completed
    }
}