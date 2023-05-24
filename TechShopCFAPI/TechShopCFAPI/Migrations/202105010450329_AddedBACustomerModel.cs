namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBACustomerModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BACustomers",
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BACustomers", new[] { "Email" });
            DropIndex("dbo.BACustomers", new[] { "UserName" });
            DropTable("dbo.BACustomers");
        }
    }
}
