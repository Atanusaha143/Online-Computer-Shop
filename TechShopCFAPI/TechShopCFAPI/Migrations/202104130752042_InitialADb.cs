namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialADb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50, unicode: false),
                        UserName = c.String(nullable: false, maxLength: 30, unicode: false),
                        ProfilePic = c.String(maxLength: 150, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Phone = c.String(nullable: false, maxLength: 12, unicode: false),
                        Status = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 150, unicode: false),
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 30, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ProductDescription = c.String(nullable: false, unicode: false, storeType: "text"),
                        Status = c.String(nullable: false, maxLength: 20, unicode: false),
                        BuyingPrice = c.Decimal(nullable: false, precision: 18, scale: 1),
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 1),
                        Category = c.String(nullable: false, maxLength: 20, unicode: false),
                        Brand = c.String(nullable: false, maxLength: 50, unicode: false),
                        Features = c.String(unicode: false, storeType: "text"),
                        Quantity = c.Int(nullable: false),
                        Images = c.String(nullable: false, maxLength: 250, unicode: false),
                        Discount = c.Int(),
                        Tax = c.Int(),
                        DateAdded = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Credentials");
            DropTable("dbo.Admins");
        }
    }
}
