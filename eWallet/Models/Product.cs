using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace eWallet.Models {
    public class Product {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Code { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int GrantId { get; set; }
        public virtual Grant Grant { get; set; }

        public virtual ICollection<Order> Orders { get; set; } =
            new HashSet<Order>();

        public static void Configure(DbModelBuilder builder)
        {
            var product = builder.Entity<Product>();

            product.HasKey(pr => pr.Id);
            product.Property(pr => pr.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            product.Property(pr => pr.Title).IsRequired().HasMaxLength(50);
            product.Property(pr => pr.Code).IsRequired().HasMaxLength(25);

            product.HasMany(el => el.Orders)
                .WithRequired(el => el.Product)
                .HasForeignKey(el => el.ProductId);
        }
    }
}