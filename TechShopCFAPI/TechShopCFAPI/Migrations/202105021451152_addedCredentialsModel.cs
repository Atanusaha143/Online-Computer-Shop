namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCredentialsModel : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Credentials");
        }
    }
}
