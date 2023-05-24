namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        ProductDescription = c.String(nullable: false),
                        Status = c.String(nullable: false, maxLength: 20),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(nullable: false, maxLength: 20),
                        Brand = c.String(nullable: false, maxLength: 50),
                        Features = c.String(),
                        Quantity = c.Int(nullable: false),
                        Images = c.String(nullable: false, maxLength: 250),
                        Discount = c.Int(),
                        Tax = c.Int(),
                        DateAdded = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales_Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        CustomerName = c.String(nullable: false, maxLength: 20),
                        CustomerAddress = c.String(nullable: false, maxLength: 20),
                        CustomerPhoneNo = c.String(nullable: false, maxLength: 20),
                        ProductId = c.Int(nullable: false),
                        SalesExecutiveId = c.Int(),
                        DateSold = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        Tax = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(nullable: false, maxLength: 20),
                        Profits = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sales_Log");
            DropTable("dbo.Products");
            DropTable("dbo.Credentials");
        }
    }
}
