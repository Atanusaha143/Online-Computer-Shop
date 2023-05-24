namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.OldProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        ProductDescription = c.String(nullable: false),
                        Status = c.String(nullable: false, maxLength: 20),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(nullable: false, maxLength: 100),
                        Brand = c.String(nullable: false, maxLength: 50),
                        Features = c.String(),
                        Quantity = c.Int(nullable: false),
                        Images = c.String(nullable: false, maxLength: 250),
                        Discount = c.Int(),
                        Tax = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
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
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
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
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.UserId)
                .Index(t => t.UserId);
            
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
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "ProductId", "dbo.Products");
            DropForeignKey("dbo.WishLists", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ShippingDatas", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales_Log", "UserId", "dbo.Customers");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Reviews", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Ratings", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OldProducts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.WishLists", new[] { "ProductId" });
            DropIndex("dbo.WishLists", new[] { "CustomerId" });
            DropIndex("dbo.ShippingDatas", new[] { "CustomerId" });
            DropIndex("dbo.Sales_Log", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "CustomerId" });
            DropIndex("dbo.Ratings", new[] { "ProductId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.OldProducts", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "Email" });
            DropIndex("dbo.Customers", new[] { "UserName" });
            DropTable("dbo.WishLists");
            DropTable("dbo.ShippingDatas");
            DropTable("dbo.Sales_Log");
            DropTable("dbo.Reviews");
            DropTable("dbo.Products");
            DropTable("dbo.Ratings");
            DropTable("dbo.OldProducts");
            DropTable("dbo.Customers");
        }
    }
}
