using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class SalesExecutiveRepository : Repository<SalesExecutive>
    {
        TechShopDbContext context = new TechShopDbContext();
        public void Restrict(string email)
        {
            var salesExe = context.SalesExecutives.Where(x => x.Email == email).FirstOrDefault();
            salesExe.Status = 0;
            context.SaveChanges();
        }

        public List<SalesExecutive> GetActive()
        {
            List<SalesExecutive> se = context.SalesExecutives.Where(x => x.Status == 1).ToList();
            return se;
        }

        public List<SalesExecutive> GetByName(string Name)
        {
            List<SalesExecutive> salesExecutives = context.SalesExecutives.Where(p => p.FullName.Contains(Name) && p.Status == 1).ToList();
            return salesExecutives;
        }

        public List<SalesExecutive> GetRestricted()
        {
            List<SalesExecutive> se = context.SalesExecutives.Where(x => x.Status == 0).ToList();
            return se;
        }
    }
}