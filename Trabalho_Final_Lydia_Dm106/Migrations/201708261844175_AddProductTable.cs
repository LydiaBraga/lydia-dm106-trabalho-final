namespace Trabalho_Final_Lydia_Dm106.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                        color = c.String(),
                        model = c.String(nullable: false),
                        code = c.String(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        length = c.Decimal(nullable: false, precision: 18, scale: 2),
                        diameter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
