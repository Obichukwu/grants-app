using System;
using System.Data.Entity.Migrations;

namespace eWallet.Migrations
{
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "eWallet.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserRole = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "eWallet.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("eWallet.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "eWallet.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("eWallet.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "eWallet.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("eWallet.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("eWallet.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "eWallet.AgentGrants",
                c => new
                    {
                        AgentId = c.String(nullable: false, maxLength: 128),
                        GrantId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PendingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.AgentId, t.GrantId })
                .ForeignKey("eWallet.Agents", t => t.AgentId)
                .ForeignKey("eWallet.Grants", t => t.GrantId, cascadeDelete: true)
                .Index(t => t.AgentId)
                .Index(t => t.GrantId);

            CreateTable(
                "eWallet.Grants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                        MonetaryWorth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "eWallet.FarmerGrants",
                c => new
                    {
                        FarmerId = c.String(nullable: false, maxLength: 128),
                        GrantId = c.Int(nullable: false),
                        ApprovedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvailableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => new { t.FarmerId, t.GrantId })
                .ForeignKey("eWallet.Farmer", t => t.FarmerId)
                .ForeignKey("eWallet.Grants", t => t.GrantId, cascadeDelete: true)
                .Index(t => t.FarmerId)
                .Index(t => t.GrantId);

            CreateTable(
                "eWallet.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 25),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        GrantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("eWallet.Grants", t => t.GrantId, cascadeDelete: true)
                .Index(t => t.GrantId);

            CreateTable(
                "eWallet.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Byte(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentId = c.Int(nullable: false),
                        FarmerId = c.Int(nullable: false),
                        DateOrdered = c.DateTimeOffset(nullable: false, precision: 7),
                        Agent_Id = c.String(maxLength: 128),
                        Farmer_Id = c.String(maxLength: 128),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("eWallet.Agents", t => t.Agent_Id)
                .ForeignKey("eWallet.Farmer", t => t.Farmer_Id)
                .ForeignKey("eWallet.Products", t => t.Product_Id)
                .Index(t => t.Agent_Id)
                .Index(t => t.Farmer_Id)
                .Index(t => t.Product_Id);

            CreateTable(
                "eWallet.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "eWallet.Administartors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("eWallet.AspNetUsers", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "eWallet.Agents",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        ContactName = c.String(nullable: false, maxLength: 50),
                        ContactPhone = c.String(nullable: false, maxLength: 50),
                        RegistrationDate = c.DateTimeOffset(nullable: false, precision: 7),
                        State = c.Int(nullable: false),
                        Region = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("eWallet.AspNetUsers", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "eWallet.Farmer",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FarmName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        OtherName = c.String(nullable: false, maxLength: 50),
                        Type = c.Int(nullable: false),
                        RegistrationDate = c.DateTimeOffset(nullable: false, precision: 7),
                        State = c.Int(nullable: false),
                        Region = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("eWallet.AspNetUsers", t => t.Id)
                .Index(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("eWallet.Farmer", "Id", "eWallet.AspNetUsers");
            DropForeignKey("eWallet.Agents", "Id", "eWallet.AspNetUsers");
            DropForeignKey("eWallet.Administartors", "Id", "eWallet.AspNetUsers");
            DropForeignKey("eWallet.AspNetUserRoles", "UserId", "eWallet.AspNetUsers");
            DropForeignKey("eWallet.AspNetUserLogins", "UserId", "eWallet.AspNetUsers");
            DropForeignKey("eWallet.AspNetUserClaims", "UserId", "eWallet.AspNetUsers");
            DropForeignKey("eWallet.AspNetUserRoles", "RoleId", "eWallet.AspNetRoles");
            DropForeignKey("eWallet.Orders", "Product_Id", "eWallet.Products");
            DropForeignKey("eWallet.Orders", "Farmer_Id", "eWallet.Farmer");
            DropForeignKey("eWallet.Orders", "Agent_Id", "eWallet.Agents");
            DropForeignKey("eWallet.Products", "GrantId", "eWallet.Grants");
            DropForeignKey("eWallet.FarmerGrants", "GrantId", "eWallet.Grants");
            DropForeignKey("eWallet.FarmerGrants", "FarmerId", "eWallet.Farmer");
            DropForeignKey("eWallet.AgentGrants", "GrantId", "eWallet.Grants");
            DropForeignKey("eWallet.AgentGrants", "AgentId", "eWallet.Agents");
            DropIndex("eWallet.Farmer", new[] { "Id" });
            DropIndex("eWallet.Agents", new[] { "Id" });
            DropIndex("eWallet.Administartors", new[] { "Id" });
            DropIndex("eWallet.AspNetRoles", "RoleNameIndex");
            DropIndex("eWallet.Orders", new[] { "Product_Id" });
            DropIndex("eWallet.Orders", new[] { "Farmer_Id" });
            DropIndex("eWallet.Orders", new[] { "Agent_Id" });
            DropIndex("eWallet.Products", new[] { "GrantId" });
            DropIndex("eWallet.FarmerGrants", new[] { "GrantId" });
            DropIndex("eWallet.FarmerGrants", new[] { "FarmerId" });
            DropIndex("eWallet.AgentGrants", new[] { "GrantId" });
            DropIndex("eWallet.AgentGrants", new[] { "AgentId" });
            DropIndex("eWallet.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("eWallet.AspNetUserRoles", new[] { "UserId" });
            DropIndex("eWallet.AspNetUserLogins", new[] { "UserId" });
            DropIndex("eWallet.AspNetUserClaims", new[] { "UserId" });
            DropIndex("eWallet.AspNetUsers", "UserNameIndex");
            DropTable("eWallet.Farmer");
            DropTable("eWallet.Agents");
            DropTable("eWallet.Administartors");
            DropTable("eWallet.AspNetRoles");
            DropTable("eWallet.Orders");
            DropTable("eWallet.Products");
            DropTable("eWallet.FarmerGrants");
            DropTable("eWallet.Grants");
            DropTable("eWallet.AgentGrants");
            DropTable("eWallet.AspNetUserRoles");
            DropTable("eWallet.AspNetUserLogins");
            DropTable("eWallet.AspNetUserClaims");
            DropTable("eWallet.AspNetUsers");
        }
    }
}
