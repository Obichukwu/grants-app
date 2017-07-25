using System.Data.Entity;
using eWallet.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eWallet.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class eWalletContext : IdentityDbContext<User>
    {
        public eWalletContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentGrant> AgentGrants { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<FarmerGrant> FarmerGrants { get; set; }
        public DbSet<Grant> Grants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet <Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<eWalletContext, Configuration>());

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.HasDefaultSchema("eWallet");

            Administrator.Configure(builder);
            Agent.Configure(builder);
            Farmer.Configure(builder);
            Grant.Configure(builder);
            AgentGrant.Configure(builder);
            FarmerGrant.Configure(builder);
            Product.Configure(builder);
            Order.Configure(builder);
        }

        public static eWalletContext Create()
        {
            return new eWalletContext();
        }
    }
}
