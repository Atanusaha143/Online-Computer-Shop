using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class OldProductRepository : Repository<OldProduct>
    {
        TechShopDbContext context = new TechShopDbContext();

        public List<OldProduct> GetInStock()
        {
            List<OldProduct> products = context.OldProducts.Where(x => x.Status == "In Stock").ToList();
            return products;
        }

        public List<OldProduct> GetInStockByCategory(string category)
        {
            List<OldProduct> products = context.OldProducts.Where(x => x.Category == category && x.Status == "In Stock").ToList();
            return products;
        }

    }
}