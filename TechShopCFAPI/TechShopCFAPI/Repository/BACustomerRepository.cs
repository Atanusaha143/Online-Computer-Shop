using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repository
{
    public class BACustomerRepository : Repository<BACustomer>
    {

        public List<PurchaseLog> GetPurchaseHistory(int id)
        {
            PruchaseLogRepository pruchaseLogRepository = new PruchaseLogRepository();
            var allPurchase = pruchaseLogRepository.GetAll();
            List<PurchaseLog> history = new List<PurchaseLog>();
            foreach(var item in allPurchase)
            {
                if(item.CustomerId == id)
                {
                    history.Add(item);
                }
            }
            return history;
        }
    }
}