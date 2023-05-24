using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Controllers.Admin
{
    public class AdminDashboardController : ApiController
    {
        [Route("api/sales_log/getdatacategory")]
        public IHttpActionResult GetDataCategory()
        {
            TechShopDbContext context = new TechShopDbContext();

            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.Category)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).OrderByDescending(x => x.count).ToList().Take(7);
            return Ok(query);
        }

        [Route("api/sales_log/getdatabrand")]
        public IHttpActionResult GetDataBrand()
        {
            TechShopDbContext context = new TechShopDbContext();

            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.Brand)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).OrderByDescending(x => x.count).ToList().Take(7);
            return Ok(query);
        }
    }
}
