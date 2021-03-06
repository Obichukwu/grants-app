using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eWallet.Models
{
    public class Order //: BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public Decimal UnitPrice { get; set; }

        public string AgentId { get; set; }
        public virtual Agent Agent { get; set; }

        public string FarmerId { get; set; }
        public virtual Farmer Farmer { get; set; }

        public DateTimeOffset DateOrdered { get; set; }

        public OrderStatus Status { get; set; }

        public static void Configure(DbModelBuilder builder)
        {
            var order = builder.Entity<Order>();

            order.HasKey(pr => pr.Id);
            order.Property(pr => pr.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            order.Property(pr => pr.DateOrdered).IsRequired();
            //grant.Property(pr => pr.Description).IsRequired().HasMaxLength(50);

            //grant.HasMany(el => el.Products)
            //    .WithRequired(el => el.Grant)
            //    .HasForeignKey(el => el.GrantId);
            //grant.HasMany(el => el.Agents)
            //    .WithRequired(el => el.Grant)
            //    .HasForeignKey(el => el.GrantId);
            //grant.HasMany(el => el.Farmers)
            //    .WithRequired(el => el.Grant)
            //    .HasForeignKey(el => el.GrantId);
        }

    }
    public enum OrderStatus
    {
        Created,
        AwaitingAgentDisbursal,
        AwaitingFarmerConfirmation,
        Completed,
        Failed
    }
}