using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        public static bool CustomerLoginValidation(string user, string password)
        {
            using (TechShopDbContext context = new TechShopDbContext())
            {
                return context.Customers.Any(x => x.UserName.Equals(user, StringComparison.OrdinalIgnoreCase) || x.Email.Equals(user, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
            }
        }
        public Customer GetCustomerByEmailOrUserName(string user)
        {
            return GetAll().Where(x => x.Email.Equals(user, StringComparison.OrdinalIgnoreCase) || x.UserName.Equals(user, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}