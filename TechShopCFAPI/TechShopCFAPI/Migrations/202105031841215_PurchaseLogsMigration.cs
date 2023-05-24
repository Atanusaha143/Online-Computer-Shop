namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseLogsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        ProductDescription = c.String(nullable: false),
                        Status = c.String(nullable: false, maxLength: 20),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(nullable: false, maxLength: 100),
                        Brand = c.String(nullable: false, maxLength: 50),
                        Features = c.String(),
                        Quantity = c.Int(nullable: false),
                        Images = c.String(maxLength: 250),
                        PurchasedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PurchaseLogs");
        }
    }
}
