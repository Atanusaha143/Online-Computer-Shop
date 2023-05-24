namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullabledBuyingAgentLastUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BuyingAgents", "LastUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BuyingAgents", "LastUpdated", c => c.DateTime(nullable: false));
        }
    }
}
