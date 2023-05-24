namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyingAgentMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyingAgents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 50),
                        ProfilePic = c.String(maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Address = c.String(nullable: false, maxLength: 150),
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BuyingAgents", new[] { "Email" });
            DropIndex("dbo.BuyingAgents", new[] { "UserName" });
            DropTable("dbo.BuyingAgents");
        }
    }
}
