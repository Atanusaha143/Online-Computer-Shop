namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
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
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SalesExecutives");
            DropTable("dbo.Promotions");
        }
    }
}
