namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyingAgents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        ProfilePic = c.String(maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 20, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Phone = c.String(nullable: false, maxLength: 11, unicode: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 1),
                        Address = c.String(nullable: false, maxLength: 150, unicode: false),
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.OldProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ProductDescription = c.String(nullable: false, unicode: false, storeType: "text"),
                        Status = c.String(nullable: false, maxLength: 20, unicode: false),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 1),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 1),
                        Category = c.String(nullable: false, maxLength: 100, unicode: false),
                        Brand = c.String(nullable: false, maxLength: 50, unicode: false),
                        Features = c.String(unicode: false, storeType: "text"),
                        Quantity = c.Int(nullable: false),
                        Images = c.String(nullable: false, maxLength: 250, unicode: false),
                        Discount = c.Int(),
                        Tax = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ProductDescription = c.String(nullable: false, unicode: false, storeType: "text"),
                        Status = c.String(nullable: false, maxLength: 20, unicode: false),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 1),
                        Category = c.String(nullable: false, maxLength: 100, unicode: false),
                        Brand = c.String(nullable: false, maxLength: 50, unicode: false),
                        Features = c.String(unicode: false, storeType: "text"),
                        Quantity = c.Int(nullable: false),
                        Images = c.String(maxLength: 250, unicode: false),
                        PurchasedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BuyingAgents", new[] { "Email" });
            DropIndex("dbo.BuyingAgents", new[] { "UserName" });
            DropTable("dbo.PurchaseLogs");
            DropTable("dbo.OldProducts");
            DropTable("dbo.BuyingAgents");
        }
    }
}
