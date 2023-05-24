using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Repository;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/BuyProducts")]
    public class BuyProductController : ApiController
    {
        BuyProductRepository buyProductRepository = new BuyProductRepository();
        ProductRepository productRepository = new ProductRepository();
        CustomerRepository customerRepository = new CustomerRepository();

        [Route("{id}", Name ="GetBySalesId"), HttpGet]
        public IHttpActionResult Get(int id)
        {
            var sellData = buyProductRepository.GetAll().Where(x => x.Id == id).FirstOrDefault();
            if (sellData==null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                return Ok(sellData);
            }
        }
        [Route(""), HttpPost]
        public IHttpActionResult Post(Sales_Log sales_Log)
        {
            if (ModelState.IsValid)
            {
                buyProductRepository.Insert(sales_Log);
                string url = Url.Link("GetBySalesId", new { id=sales_Log.Id});
                return Created(url, sales_Log);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [Route("GetPurchasedDataByCustomerId/{id}"), HttpGet]
        public IHttpActionResult GetPurchasedDataByCustomerId(int id)
        {
            List<PurchaseViewModel> sellData = new List<PurchaseViewModel>();

            foreach (var sellsItem in buyProductRepository.GetAll().Where(x=>x.UserId==id).ToList())
            {
                foreach (var productsItem in productRepository.GetAll().Where(x => x.Id== sellsItem.ProductId).ToList())
                {
                        sellData.Add(new PurchaseViewModel
                        {
                            ProductName = productsItem.ProductName,
                            UnitPrice = productsItem.SellingPrice,
                            Id = sellsItem.Id,
                            Discount = (int)productsItem.Discount,
                            SoldDate = sellsItem.DateSold,
                            Status = sellsItem.Status,
                            Quantity = sellsItem.Quantity,
                            Tax = (int)productsItem.Tax
                        });
                } 
            }

            if (sellData.Count==0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(sellData);
            }
        }
        [Route("{id}/CancelOrder"), HttpGet]
        public IHttpActionResult CancelOrder([FromUri] int id)
        {
            var order = buyProductRepository.Get(id);
            if (order!=null)
            {
                if (order.Status=="Pending")
                {
                    order.Status = "Cancelled";
                    buyProductRepository.Update(order);
                    return Ok(order);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotAcceptable);
                }
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
    }
}
