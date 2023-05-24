using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class PurchaseRepository : Repository<PurchaseLog>
    {
        TechShopDbContext context = new TechShopDbContext();

        public List<PurchaseLog> Get(string startDate, string endDate)
        {
            List<PurchaseLog> products = context.PurchaseLogs.Where(x => Convert.ToDateTime(x.PurchasedDate) >= Convert.ToDateTime(startDate) && Convert.ToDateTime(x.PurchasedDate) <= Convert.ToDateTime(endDate)).ToList();
            return products;
        }

        public List<PurchaseLog> GetByName(string Name)
        {
            List<PurchaseLog> purs = context.PurchaseLogs.Where(p => p.ProductName.Contains(Name)).ToList();
            return purs;
        }
    }
}