using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class CredentialRepository : Repository<Credential>
    {

        TechShopDbContext context = new TechShopDbContext();

        public Credential Validation(string email, string password)
        {
            var c = context.Credentials.Where(x => x.Email == email && x.Password == password && x.Status == 1).FirstOrDefault();
            return c;
        }

        public void Restrict(string email)
        {
            var cred = context.Credentials.Where(x => x.Email == email).FirstOrDefault();
            cred.Status = 0;
            context.SaveChanges();
        }

        public void Reactive(string email)
        {
            var cred = context.Credentials.Where(x => x.Email == email).FirstOrDefault();
            cred.Status = 1;
            context.SaveChanges();
        }

        public Credential GetByEmail(string email)
        {
            Credential crd = context.Credentials.Where(p => p.Email == email && p.Status == 1).FirstOrDefault();
            return crd;
        }

    }
}