using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Attributes;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    public class PurchaseListController : ApiController
    {
        TechShopDbContext context = new TechShopDbContext();
        PurchaseRepository purRepo = new PurchaseRepository();

        [Route("api/purchaselogs"), BasicAuthentication]
        public IHttpActionResult Get()
        {
            return Ok(purRepo.GetAll());
        }

        [Route("api/purchaselogs/name"), BasicAuthentication]
        public IHttpActionResult Get(string productName)
        {
            return Ok(purRepo.GetByName(productName));
        }
        /*[Route("api/purchaselogs")]
        public IHttpActionResult Get(DateTime startDate, DateTime endDate)
        {
            if (startDate == null && endDate == null)
            {
                return Ok(purRepo.GetAll());
            }
            else if (startDate != null && endDate != null)
            {
                return Ok(purRepo.Get(startDate, endDate));
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }*/
    }
}
