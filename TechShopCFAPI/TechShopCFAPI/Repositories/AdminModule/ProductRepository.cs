using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class ProductRepository: Repository<Product>
    {
        TechShopDbContext context = new TechShopDbContext();

        public List<Product> GetByName(string name)
        {
            List<Product> products = context.Products.Where(p => p.ProductName.Contains(name)).ToList();
            return products;
        }

        public List<Product> GetByCategory(string category)
        {
            List<Product> products = context.Products.Where(p => p.Category == category).ToList();
            return products;
        }

        public List<Product> GetByBrand(string brand)
        {
            List<Product> products = context.Products.Where(p => p.Brand == brand).ToList();
            return products;
        }

    }
}