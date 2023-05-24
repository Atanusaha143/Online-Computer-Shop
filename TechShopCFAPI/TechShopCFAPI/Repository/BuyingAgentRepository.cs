using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repository
{
    public class BuyingAgentRepository : Repository<BuyingAgent>
    {
        public BuyingAgent GetBuyingAgentByEmail(string email)
        {
            return GetAll().Where(x => x.Email.Equals(email)).FirstOrDefault();
        }
    }
}