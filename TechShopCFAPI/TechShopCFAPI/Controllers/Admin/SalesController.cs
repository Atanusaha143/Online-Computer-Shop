using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Controllers.Admin
{
    public class SalesController : ApiController
    {
        TechShopDbContext context = new TechShopDbContext();

        [Route("api/sales_log")]
        public IHttpActionResult Get(string startDate, string endDate)
        {
            if (startDate != null && endDate != null)
            {
                var sales = context.Sales_Log.Include("Product").ToList()
                    .Where(p => Convert.ToDateTime(p.DateSold) >= Convert.ToDateTime(startDate) && Convert.ToDateTime(p.DateSold) <= Convert.ToDateTime(endDate))
                    .GroupBy(p => p.Product.ProductName)
                    .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) });

                return Json(sales);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent); ;
            }
        }

        [Route("api/sales_log/getdatasalescategory")]
        public IHttpActionResult GetDataCategory()
        {
            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.Category)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).ToList();
            return Json(query);
        }

        [Route("api/sales_log/getdatasalesbrand")]
        public IHttpActionResult GetDataProductName()
        {
            var query = context.Sales_Log.Include("Product")
                   .GroupBy(p => p.Product.ProductName)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).ToList();
            return Json(query);
        }
    }
}
