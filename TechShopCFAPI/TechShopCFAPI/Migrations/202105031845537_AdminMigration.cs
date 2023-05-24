namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 30),
                        ProfilePic = c.String(maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 12),
                        Status = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 150),
                        JoiningDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Admins");
        }
    }
}