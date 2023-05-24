using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Repository;
using TechShopCFAPI.Models;
using System.Web;

namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/SellProducts")]
    public class SellProductController : ApiController
    {
        OldProductsRepository oldProductRepo = new OldProductsRepository();

        [Route("GetAllPendingOldProductsByCustomerId/{id}"), HttpGet]
        public IHttpActionResult Get(int id)
        {
            var allOldProducts = oldProductRepo.GetAll().Where(x=>x.CustomerId==id && x.Status=="Pending").OrderByDescending(x=>x.Id).ToList();
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            var ID = url.Substring(url.Length - 1);

            foreach (var item in allOldProducts)
            {
                item.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a sell post for old product." });
                item.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Delete a sell post for old product by post id." });
                item.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit a sell post for old product by post id." });
                item.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get a specific post by post id." });
                item.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3) + "/GetMostSoldCategoriesByCustomerId/" + ID, Method = "GET", Relation = "Get most sold old products categories by customer ID." });
            }
            if (allOldProducts.Count!=0)
            {
                return Ok(allOldProducts);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
        [Route("GetAllSoldOldProductByCustomerId/{id}"), HttpGet]
        public IHttpActionResult GetAllSoldProductByCustomerId(int id)
        {
            var soldProducts = oldProductRepo.GetAll().Where(x => x.Status != "Pending" && x.CustomerId == id).OrderByDescending(x => x.Id).ToList();
            if (soldProducts.Count!=0)
            {
                return Ok(soldProducts);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
        [Route("{id}", Name = "GetByOldProductId"), HttpGet]
        public IHttpActionResult GetByOldProductId(int id)
        {
            var oldProduct = oldProductRepo.Get(id);

            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            var ID = url.Substring(url.Length-1);

            oldProduct.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a sell post for old product." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Delete a sell post for old product by post id." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit a sell post for old product by post id." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get a specific post by post id." });
            oldProduct.Links.Add(new Link() { Url = url.Substring(0, url.Length-3)+ "/GetMostSoldCategoriesByCustomerId/"+ID, Method = "GET", Relation = "Get most sold old products categories by customer ID." });

            if (oldProduct!=null)
            {
                return Ok(oldProduct);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route(""), HttpPost]
        public IHttpActionResult Post(OldProduct oldProduct)
        {

            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            var ID = url.Substring(url.Length - 1);

            oldProduct.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a sell post for old product." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Delete a sell post for old product by post id." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit a sell post for old product by post id." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get a specific post by post id." });
            oldProduct.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3) + "/GetMostSoldCategoriesByCustomerId/" + ID, Method = "GET", Relation = "Get most sold old products categories by customer ID." });

            if (ModelState.IsValid)
            {
                oldProductRepo.Insert(oldProduct);
                string URL = Url.Link("GetByOldProductId", new { id = oldProduct.Id});
                return Created(URL, oldProduct);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [Route("{id}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            oldProductRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("{id}"), HttpPut]
        public IHttpActionResult Put([FromUri]int id, [FromBody]OldProduct oldProduct)
        {
            oldProduct.Id = id;

            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            var ID = url.Substring(url.Length - 1);

            oldProduct.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a sell post for old product." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Delete a sell post for old product by post id." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit a sell post for old product by post id." });
            oldProduct.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get a specific post by post id." });
            oldProduct.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3) + "/GetMostSoldCategoriesByCustomerId/" + ID, Method = "GET", Relation = "Get most sold old products categories by customer ID." });
            if (ModelState.IsValid)
            {
                oldProductRepo.Update(oldProduct);
                return Ok(oldProduct);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }
        [Route("GetMostSoldCategoriesByCustomerId/{id}"), HttpGet]
        public IHttpActionResult GetMostSoldCategoriesByCustomerId(int id)
        {
            List<KeyValuePair<string, int>> mostSoldCategoreiesList = new List<KeyValuePair<string, int>>();
            var list = oldProductRepo.GetAll().Where(x=>x.CustomerId==id && x.Status!="Pending").GroupBy(x => x.Category);
            foreach (var group in list)
            {
                mostSoldCategoreiesList.Add(new KeyValuePair<string, int>(group.Key, group.Count()));
            }
            if (mostSoldCategoreiesList.Count!=0)
            {
                return Ok(mostSoldCategoreiesList);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
