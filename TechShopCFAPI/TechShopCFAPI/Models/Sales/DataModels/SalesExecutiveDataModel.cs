using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models.Sales.DataModels
{
    public class SalesExecutiveDataModel
    {
        TechShopDbContext data = new TechShopDbContext();

        public SalesExecutive GetValidSalesExecutive(int id)
        {
            return data.SalesExecutives.Where(x => x.Id == id).FirstOrDefault();
        }
        public SalesExecutive GetValidSalesExecutive2(string email, string password)
        {
            return data.SalesExecutives.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    }
}