using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class AdminRepository : Repository<Admin>
    {
        TechShopDbContext context = new TechShopDbContext();

        public void Restrict(string email)
        {
            var admin = context.Admins.Where(x => x.Email == email).FirstOrDefault();
            admin.Status = 0;
            context.Entry(admin).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public List<Admin> GetActive()
        {
            List<Admin> admins = context.Admins.Where(x => x.Status == 1).ToList();
            return admins;
        }

        public List<Admin> GetByName(string Name)
        {
            List<Admin> admins = context.Admins.Where(p => p.FullName.Contains(Name) && p.Status == 1).ToList();
            return admins;
        }

        public Admin GetByEmail(string email)
        {
            Admin admin = context.Admins.Where(p => p.Email == email && p.Status == 1).FirstOrDefault();
            return admin;
        }

        public List<Admin> GetRestricted()
        {
            List<Admin> admins = context.Admins.Where(x => x.Status == 0).ToList();
            return admins;
        }
    }
}