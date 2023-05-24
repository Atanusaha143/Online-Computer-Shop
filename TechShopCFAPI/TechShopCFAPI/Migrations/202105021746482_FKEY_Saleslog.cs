namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKEY_Saleslog : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Sales_Log", "ProductId");
            AddForeignKey("dbo.Sales_Log", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales_Log", "ProductId", "dbo.Products");
            DropIndex("dbo.Sales_Log", new[] { "ProductId" });
        }
    }
}
