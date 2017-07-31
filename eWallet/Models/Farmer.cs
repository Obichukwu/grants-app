using eWallet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
//using static eWallet.Models.User;

namespace eWallet.Models {
    public class Farmer : User
    {
        public string FarmName { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public FarmerType Type { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }

        public State State { get; set; }
        public Region Region { get; set; }

        public decimal GeneralWalletBalance { get; set; }

        public virtual ICollection<FarmerGrant> Grants { get; set; } =
            new HashSet<FarmerGrant>();

        public virtual ICollection<Order> Orders { get; set; } =
            new HashSet<Order>();

        public static void Configure(DbModelBuilder builder)
        {
            var farmer = builder.Entity<Farmer>();
            farmer.ToTable("Farmer");
            farmer.Property(pr => pr.FarmName).IsRequired().HasMaxLength(50);
            farmer.Property(pr => pr.FirstName).IsRequired().HasMaxLength(50);
            farmer.Property(pr => pr.OtherName).IsRequired().HasMaxLength(50);

            farmer.HasMany(el => el.Grants)
                .WithRequired(el => el.Farmer)
                .HasForeignKey(el => el.FarmerId);

            farmer.HasMany(el => el.Orders)
                .WithRequired(el => el.Farmer)
                .HasForeignKey(el => el.FarmerId);
        }
    }

    public enum FarmerType
    {
        Company, Coporative, Individual
    }
}