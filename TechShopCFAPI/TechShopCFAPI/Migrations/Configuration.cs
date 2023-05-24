namespace TechShopCFAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TechShopCFAPI.Models.TechShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            ContextKey = "TechShopCFAPI.Models.TechShopDbContext";

        }

        protected override void Seed(TechShopCFAPI.Models.TechShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
