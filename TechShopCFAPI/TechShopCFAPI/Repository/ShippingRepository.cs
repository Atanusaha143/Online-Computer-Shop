using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repository
{
    public class ShippingRepository: Repository<ShippingData>
    {
        public Models.ShippingData GetShippingDataByCustomerId(int id)
        {
            return GetAll().Where(x => x.CustomerId == id).FirstOrDefault();
            
        }
    }
}