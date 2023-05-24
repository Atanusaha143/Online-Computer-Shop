using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class CustomerRepository : Repository<Customer>
    {
        TechShopDbContext context = new TechShopDbContext();
        public List<Customer> GetActiveCustomers()
        {
            List<Customer> customers = context.Customers.Where(x => x.Status == "Active").ToList();
            return customers;
        }
        
        public void BlockCustomer(string email)
        {
            var customer = context.Customers.Where(x => x.Email == email).FirstOrDefault();
            customer.Status = "Restricted";
            context.SaveChanges();
        }

        public List<Customer> GetActiveByName(string Name)
        {
            List<Customer> cust = context.Customers.Where(p => p.FullName.Contains(Name) && p.Status == "Active").ToList();
            return cust;
        }

        public List<Customer> GetRestrictedByName(string Name)
        {
            List<Customer> cust = context.Customers.Where(p => p.FullName.Contains(Name) && p.Status == "Restricted").ToList();
            return cust;
        }

        public List<Customer> GetRestrictedCustomers()
        {
            List<Customer> customers = context.Customers.Where(x => x.Status == "Restricted").ToList();
            return customers;
        }

        public void ReactivateCustomer(string email)
        {
            var customer = context.Customers.Where(x => x.Email == email).FirstOrDefault();
            customer.Status = "Active";
            context.SaveChanges();
        }

        public Customer GetCust(int customerId)
        {
            return context.Customers.Find(customerId);
        }

        public List<Sales_Log> History(int id)
        {
            return context.Sales_Log.Where(m => m.UserId == id).ToList();
        }

        public List<Review> CustomerReview(int id)
        {
            return context.Reviews.Where(p => p.CustomerId == id).ToList();
        }
    }
}