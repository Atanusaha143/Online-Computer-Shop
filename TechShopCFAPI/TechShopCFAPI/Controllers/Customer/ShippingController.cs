using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/Shipping")]
    public class ShippingController : ApiController
    {
        protected ShippingRepository shippingRepository = new ShippingRepository();
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var shippingData=shippingRepository.Get(id);

            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            shippingData.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get a specific shpping data by id." });
            shippingData.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit a specific shpping data by id." });

            if (shippingData!=null)
            {
                return Ok(shippingData);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("{id}"), HttpPut]
        public IHttpActionResult Edit([FromBody]Models.ShippingData shippingData, [FromUri]int id)
        {
            shippingData.Id= id;
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            shippingData.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get a specific shpping data by id." });
            shippingData.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit a specific shpping data by id." });

            if (ModelState.IsValid)
            {
                shippingRepository.Update(shippingData);
                return Ok(shippingRepository);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }
    }
}
