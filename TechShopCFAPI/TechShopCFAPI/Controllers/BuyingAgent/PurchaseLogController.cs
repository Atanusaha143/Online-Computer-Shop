using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Attributes;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.BuyingAgent
{
    [RoutePrefix("api/purchase_log")]
    public class PurchaseLogController : ApiController
    {
        PruchaseLogRepository pruchaseLogRepository = new PruchaseLogRepository();

        [Route("{id}", Name ="GetPurchaseLogById"), BasicAuthentication]
        public IHttpActionResult Get([FromUri] int id)
        {
            var purchaseLog = pruchaseLogRepository.Get(id);

            if (purchaseLog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var insertUrl = mainUrl.Remove(mainUrl.Length - 3, 3);
                purchaseLog.Links.Add(new Link() { Url = mainUrl, Method = "GET", Relation = "Get an existing Purchase Log" });
                purchaseLog.Links.Add(new Link() { Url = mainUrl, Method = "PUT", Relation = "Update an existing Purchase Log" });
                purchaseLog.Links.Add(new Link() { Url = mainUrl, Method = "DELETE", Relation = "Delete an existing Purchase Log" });
                purchaseLog.Links.Add(new Link() { Url = insertUrl + "sortBy/Price", Method = "GET", Relation = "Sort all existing Purchase Log By Price" });
                purchaseLog.Links.Add(new Link() { Url = insertUrl + "sortBy/Quantity", Method = "GET", Relation = "Sort all existing Purchase Log By Quantity" });
                return Ok(purchaseLog);
            }
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Get()
        {
            var purchaseLogs = pruchaseLogRepository.GetAll().ToList();

            if (purchaseLogs.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var insertUrl = mainUrl.Remove(mainUrl.Length - 3, 3);
                foreach (var purchaseLog in purchaseLogs)
                {
                    purchaseLog.Links.Add(new Link() { Url = mainUrl + "/" + purchaseLog.Id, Method = "GET", Relation = "Get an existing Purchase Log" });
                    purchaseLog.Links.Add(new Link() { Url = mainUrl + "/" + purchaseLog.Id, Method = "PUT", Relation = "Update an existing Purchase Log" });
                    purchaseLog.Links.Add(new Link() { Url = mainUrl + "/" + purchaseLog.Id, Method = "DELETE", Relation = "Delete an existing Purchase Log" });
                    purchaseLog.Links.Add(new Link() { Url = insertUrl + "sortBy/Price", Method = "GET", Relation = "Sort all existing Purchase Log By Price" });
                    purchaseLog.Links.Add(new Link() { Url = insertUrl + "sortBy/Quantity", Method = "GET", Relation = "Sort all existing Purchase Log By Quantity" });
                }
                return Ok(purchaseLogs);
            }
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Post(Models.PurchaseLog purchaseLog)
        {
            if(ModelState.IsValid)
            {
                pruchaseLogRepository.Insert(purchaseLog);
                string url = Url.Link("GetPurchaseLogById", new { id = purchaseLog.Id });
                return Created(url, purchaseLog);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Put([FromBody] Models.PurchaseLog purchaseLog, [FromUri] int id)
        {
            var updatedPurchaseLog = pruchaseLogRepository.Get(id);
            if (updatedPurchaseLog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                if(ModelState.IsValid)
                {
                    updatedPurchaseLog.CustomerId = purchaseLog.CustomerId;
                    updatedPurchaseLog.ProductName = purchaseLog.ProductName;
                    updatedPurchaseLog.ProductDescription = purchaseLog.ProductDescription;
                    updatedPurchaseLog.Status = purchaseLog.Status;
                    updatedPurchaseLog.BuyingPrice = purchaseLog.BuyingPrice;
                    updatedPurchaseLog.Category = purchaseLog.Category;
                    updatedPurchaseLog.Brand = purchaseLog.Brand;
                    updatedPurchaseLog.Features = purchaseLog.Features;
                    updatedPurchaseLog.Quantity = purchaseLog.Quantity;
                    updatedPurchaseLog.Images = purchaseLog.Images;
                    updatedPurchaseLog.PurchasedDate = purchaseLog.PurchasedDate;
                    pruchaseLogRepository.Update(updatedPurchaseLog);
                    return Ok(updatedPurchaseLog);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotModified);
                }
            }
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var purchaseLog = pruchaseLogRepository.Get(id);

            if (purchaseLog == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                pruchaseLogRepository.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("pie_chart"), HttpGet, BasicAuthentication]
        public IHttpActionResult PieChart()
        {
            var allPurchasedata = pruchaseLogRepository.GetAll();
            var category = allPurchasedata.Select(x => x.Category).Distinct();
            List<int> value = new List<int>();

            foreach (var item in category)
            {
                value.Add(allPurchasedata.Count(x => x.Category == item));
            }
            return Ok(new { category, value });

        }

        [Route("bar_chart"), HttpGet, BasicAuthentication]
        public IHttpActionResult BarChart()
        {
            var allPurchasedata = pruchaseLogRepository.GetAll();
            var category = allPurchasedata.Select(x => x.Category).Distinct();
            List<decimal> value = new List<decimal>();

            foreach (var item in category)
            {
                decimal currPurchase = 0;
                foreach(var i in allPurchasedata)
                {
                    if(item == i.Category)
                    {
                        currPurchase += i.BuyingPrice;
                    }
                }
                value.Add(currPurchase);
            }
            return Ok(new { category, value });

        }

        [Route("sortBy/{sortBy}"), HttpGet, BasicAuthentication]
        public IHttpActionResult SortPurchaseLog([FromUri]string sortBy)
        {
            var allPurchaseData = pruchaseLogRepository.GetAll().OrderByDescending(x => x.Quantity).ToList();
            if(sortBy == "Price")
            {
                allPurchaseData = pruchaseLogRepository.GetAll().OrderByDescending(x => x.BuyingPrice).ToList();
            }

            return Ok(allPurchaseData);

        }
    }
}
