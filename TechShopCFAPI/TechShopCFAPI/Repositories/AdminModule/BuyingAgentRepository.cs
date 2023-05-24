using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class BuyingAgentRepository : Repository<BuyingAgent>
    {
        TechShopDbContext context = new TechShopDbContext();


        public void Restrict(string email)
        {
            var by = context.BuyingAgents.Where(x => x.Email == email).FirstOrDefault();
            by.Status = 0;
            context.SaveChanges();
        }

        public List<BuyingAgent> GetActive()
        {
            List<BuyingAgent> by = context.BuyingAgents.Where(x => x.Status == 1).ToList();
            return by;
        }

        public List<BuyingAgent> GetByName(string Name)
        {
            List<BuyingAgent> bys = context.BuyingAgents.Where(p => p.FullName.Contains(Name) && p.Status == 1).ToList();
            return bys;
        }

        public List<BuyingAgent> GetRestricted()
        {
            List<BuyingAgent> by = context.BuyingAgents.Where(x => x.Status == 0).ToList();
            return by;
        }

    }
}