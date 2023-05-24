namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REDM : DbMigration
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
                        Status = c.Int(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 30),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        ProfilePic = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 11),
                        Address = c.String(maxLength: 100),
                        Status = c.String(nullable: false, maxLength: 20),
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        RatingPoint = c.Int(nullable: false),
                        DateRated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 100),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ShippingDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CardType = c.String(maxLength: 12),
                        CardNumber = c.String(maxLength: 20),
                        ExpirationYear = c.String(),
                        ExpirationMonth = c.String(),
                        ShippingMethod = c.String(maxLength: 15),
                        ShippingAddress = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        DateWished = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
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
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PromoCode = c.String(nullable: false, maxLength: 20),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Validity = c.DateTime(nullable: false),
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
            
            CreateTable(
                "dbo.SalesExecutives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 20),
                        UserName = c.String(nullable: false, maxLength: 20),
                        ProfilePic = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 20),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ShippingDatas", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Reviews", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.WishLists", new[] { "CustomerId" });
            DropIndex("dbo.ShippingDatas", new[] { "CustomerId" });
            DropIndex("dbo.Reviews", new[] { "CustomerId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "Email" });
            DropIndex("dbo.Customers", new[] { "UserName" });
            DropIndex("dbo.BuyingAgents", new[] { "Email" });
            DropIndex("dbo.BuyingAgents", new[] { "UserName" });
            DropTable("dbo.SalesExecutives");
            DropTable("dbo.Sales_Log");
            DropTable("dbo.PurchaseLogs");
            DropTable("dbo.Promotions");
            DropTable("dbo.OldProducts");
            DropTable("dbo.WishLists");
            DropTable("dbo.ShippingDatas");
            DropTable("dbo.Reviews");
            DropTable("dbo.Ratings");
            DropTable("dbo.Customers");
            DropTable("dbo.BuyingAgents");
        }
    }
}
