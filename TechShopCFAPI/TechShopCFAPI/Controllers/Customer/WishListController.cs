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
    [RoutePrefix("Api/WishList")]
    public class WishListController : ApiController
    {
        protected WishListRepository wishListRepository = new WishListRepository();
        protected ProductRepository productRepository = new ProductRepository();

        [Route("{id}", Name = "GetByWishListId"), HttpGet]
        public IHttpActionResult Get(int id)
        {
            var wishList = wishListRepository.Get(id);
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            wishList.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get a specific wish list data by id." });
            wishList.Links.Add(new Link() { Url = url.Substring(0, url.Length-3), Method = "POST", Relation = "Insert a wish list." });
            wishList.Links.Add(new Link() { Url = url+"/Customer/", Method="GET", Relation="Get wish list by customer id."});
            wishList.Links.Add(new Link() { Url = url + "/Customer/", Method = "GET", Relation = "Get wish list by customer id." });

            if (wishList == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                return Ok(wishList);
            }
        }

        [Route(""), HttpPost]
        public IHttpActionResult Post(WishList wishList)
        {
            if (ModelState.IsValid)
            {
                var wishedProduct = wishListRepository.GetAll().Where(x => x.CustomerId == wishList.CustomerId && x.ProductId == wishList.ProductId).FirstOrDefault();
                if (wishedProduct != null)
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
                wishListRepository.Insert(wishList);
                string url = Url.Link("GetByWishListId", new { id = wishList.Id });
                return Created(url, wishList);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [Route("{id}/Customer/{customerId}"), HttpGet]
        public IHttpActionResult GetWishListProductsByCustomerId(int id, int customerId)
        {
            var wishedList = wishListRepository.GetAll().Where(x => x.CustomerId == customerId).ToList();
            List<Product> wishedProductList = new List<Product>();

            foreach (var item in wishedList)
            {
                wishedProductList.Add(productRepository.Get(item.ProductId));
            }
            if (wishedProductList == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {

                return Ok(wishedProductList);
            }
        }
        [Route("{id}/Customer/{customerId}/Product/{productId}"),HttpDelete]
        public IHttpActionResult Delete(int id, int customerId, int productId)
        {
            var wishedProduct = wishListRepository.GetAll().Where(x => x.CustomerId == customerId && x.ProductId == productId).FirstOrDefault();
            if (wishedProduct==null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                wishListRepository.Delete(wishedProduct.Id);
                return StatusCode(HttpStatusCode.OK);
            }
        }
    }
}
