namespace eWallet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FuryTime : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("eWallet.Orders", "Product_Id", "eWallet.Products");
            DropIndex("eWallet.Orders", new[] { "Agent_Id" });
            DropIndex("eWallet.Orders", new[] { "Farmer_Id" });
            DropIndex("eWallet.Orders", new[] { "Product_Id" });
            DropColumn("eWallet.Orders", "AgentId");
            DropColumn("eWallet.Orders", "FarmerId");
            RenameColumn(table: "eWallet.Orders", name: "Agent_Id", newName: "AgentId");
            RenameColumn(table: "eWallet.Orders", name: "Farmer_Id", newName: "FarmerId");
            RenameColumn(table: "eWallet.Orders", name: "Product_Id", newName: "ProductId");
            AddColumn("eWallet.Orders", "Status", c => c.Int(nullable: false));
            AlterColumn("eWallet.Orders", "Quantity", c => c.Int(nullable: false));
            AlterColumn("eWallet.Orders", "AgentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("eWallet.Orders", "FarmerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("eWallet.Orders", "AgentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("eWallet.Orders", "FarmerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("eWallet.Orders", "ProductId", c => c.Int(nullable: false));
            CreateIndex("eWallet.Orders", "ProductId");
            CreateIndex("eWallet.Orders", "AgentId");
            CreateIndex("eWallet.Orders", "FarmerId");
            AddForeignKey("eWallet.Orders", "ProductId", "eWallet.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("eWallet.Orders", "ProductId", "eWallet.Products");
            DropIndex("eWallet.Orders", new[] { "FarmerId" });
            DropIndex("eWallet.Orders", new[] { "AgentId" });
            DropIndex("eWallet.Orders", new[] { "ProductId" });
            AlterColumn("eWallet.Orders", "ProductId", c => c.Int());
            AlterColumn("eWallet.Orders", "FarmerId", c => c.String(maxLength: 128));
            AlterColumn("eWallet.Orders", "AgentId", c => c.String(maxLength: 128));
            AlterColumn("eWallet.Orders", "FarmerId", c => c.Int(nullable: false));
            AlterColumn("eWallet.Orders", "AgentId", c => c.Int(nullable: false));
            AlterColumn("eWallet.Orders", "Quantity", c => c.Byte(nullable: false));
            DropColumn("eWallet.Orders", "Status");
            RenameColumn(table: "eWallet.Orders", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "eWallet.Orders", name: "FarmerId", newName: "Farmer_Id");
            RenameColumn(table: "eWallet.Orders", name: "AgentId", newName: "Agent_Id");
            AddColumn("eWallet.Orders", "FarmerId", c => c.Int(nullable: false));
            AddColumn("eWallet.Orders", "AgentId", c => c.Int(nullable: false));
            CreateIndex("eWallet.Orders", "Product_Id");
            CreateIndex("eWallet.Orders", "Farmer_Id");
            CreateIndex("eWallet.Orders", "Agent_Id");
            AddForeignKey("eWallet.Orders", "Product_Id", "eWallet.Products", "Id");
        }
    }
}
