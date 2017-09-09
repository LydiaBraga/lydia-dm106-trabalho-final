namespace Trabalho_Final_Lydia_Dm106.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDeliveryDateToAcceptNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "deliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "deliveryDate", c => c.DateTime(nullable: false));
        }
    }
}
