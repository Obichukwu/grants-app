using eWallet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace eWallet.Models {
    public class Agent: User
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public State State { get; set; }
        public Region Region { get; set; }

        public virtual ICollection<AgentGrant> Grants { get; set; } =
            new HashSet<AgentGrant>();

        public static void Configure(DbModelBuilder builder)
        {
            var agent = builder.Entity<Agent>();
            agent.ToTable("Agents");
            agent.Property(pr => pr.Name).IsRequired().HasMaxLength(50);
            agent.Property(pr => pr.Description).IsOptional().IsMaxLength();
            agent.Property(pr => pr.ContactName).IsRequired().HasMaxLength(50);
            agent.Property(pr => pr.ContactPhone).IsRequired().HasMaxLength(50);

            agent.HasMany(el => el.Grants)
                .WithRequired(el => el.Agent)
                .HasForeignKey(el => el.AgentId);
        }
    }
}