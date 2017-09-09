namespace Trabalho_Final_Lydia_Dm106.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderAndItemTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userEmail = c.String(),
                        date = c.DateTime(nullable: false),
                        deliveryDate = c.DateTime(nullable: false),
                        status = c.String(),
                        totalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        freightPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        amount = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        orderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.orderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "orderId", "dbo.Orders");
            DropForeignKey("dbo.Items", "productId", "dbo.Products");
            DropIndex("dbo.Items", new[] { "orderId" });
            DropIndex("dbo.Items", new[] { "productId" });
            DropTable("dbo.Items");
            DropTable("dbo.Orders");
        }
    }
}
