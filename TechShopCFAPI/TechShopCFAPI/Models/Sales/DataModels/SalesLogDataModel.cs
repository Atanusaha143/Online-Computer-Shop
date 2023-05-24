using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models.Sales.DataModels
{
    public class SalesLogDataModel
    {
        TechShopDbContext data = new TechShopDbContext();

        public List<Sales_Log> GetAllSalesLog()
        {
            List<Sales_Log> SalesLog = data.Sales_Logs.GroupBy(x => x.DateSold).Select(grp => grp.FirstOrDefault()).ToList();
            return SalesLog;
        }

    }
}