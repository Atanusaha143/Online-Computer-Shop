using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.BuyingAgent
{
    [RoutePrefix("api/ba_customer")]
    public class BACustomerController : ApiController
    {
        BACustomerRepository bACustomerRepository = new BACustomerRepository();

        [Route("{id}", Name = "GetBACustomerById")]
        public IHttpActionResult Get([FromUri] int id)
        {
            var bACustomer = bACustomerRepository.Get(id);

            if (bACustomer == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var insertUrl = mainUrl.Remove(mainUrl.Length - 2, 2);
                bACustomer.Links.Add(new Link() { Url = mainUrl, Method = "GET", Relation = "Get an existing Buying Agent Customer" });
                bACustomer.Links.Add(new Link() { Url = insertUrl, Method = "POST", Relation = "Create a new Buying Agent Customer" });
                bACustomer.Links.Add(new Link() { Url = insertUrl+ "/purchase_history/"+ bACustomer.CustomerId, Method = "GET", Relation = "Get purchase history of a Buying Agent Customer" });
                return Ok(bACustomer);
            }
        }

        [Route("")]
        public IHttpActionResult Post(Models.BACustomer bACustomer)
        {
            if (ModelState.IsValid)
            {
                bACustomerRepository.Insert(bACustomer);
                string url = Url.Link("GetBACustomerById", new { id = bACustomer.CustomerId });
                return Created(url, bACustomer);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }

        [Route("purchase_history/{id}"), HttpGet]
        public IHttpActionResult PurchaseHistory([FromUri] int id)
        {
            var purchaseHistory = bACustomerRepository.GetPurchaseHistory(id);

            if (purchaseHistory == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(purchaseHistory);
            }
        }
    }
}
