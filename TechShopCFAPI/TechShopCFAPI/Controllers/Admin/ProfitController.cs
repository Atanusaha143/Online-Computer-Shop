using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Controllers.Admin
{
    public class ProfitController : ApiController
    {
        TechShopDbContext context = new TechShopDbContext();

        [Route("api/sales_log/getdataprofitscategory")]
        public IHttpActionResult GetDataCategory()
        {
            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.Category)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Profits) }).ToList();
            return Json(query);
        }

        [Route("api/sales_log/getdataprofitsproductname")]
        public IHttpActionResult GetDataProductName()
        {
            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.ProductName)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Profits) }).ToList();
            return Json(query);
        }
    }
}
