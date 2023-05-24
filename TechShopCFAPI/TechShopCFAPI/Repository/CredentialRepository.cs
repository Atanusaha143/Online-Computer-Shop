using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repository
{
    public class CredentialRepository : Repository<Credential>
    {
        TechShopDbContext context = new TechShopDbContext();

        public Credential Validation(string Email, string Password)
        {
            var c = context.Credentials.Where(x => x.Email == Email && x.Password == Password && x.Type == 3).FirstOrDefault();
            return c;
        }
        public Credential GetByEmail(string Email)
        {
            return context.Credentials.Where(x => x.Email == Email).FirstOrDefault();
        }
    }
}