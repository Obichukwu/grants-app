using System.Data.Entity;

namespace eWallet.Models {
    public class Administrator : User
    {
        public static void Configure(DbModelBuilder builder)
        {
            var admin = builder.Entity<Administrator>();
            admin.ToTable("Administartors");
        }
    }
}