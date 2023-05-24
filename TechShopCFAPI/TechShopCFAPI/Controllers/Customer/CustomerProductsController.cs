using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;
namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/CustomerProducts")]
    public class CustomerProductsController : ApiController
    {
        ProductRepository productRepository = new ProductRepository();
        ReviewRepository reviewRepository = new ReviewRepository();
        RatingRepository ratingRepository = new RatingRepository();
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var product = productRepository.Get(id);
            if (product!=null)
            {
                return Ok(product);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("")]
        public IHttpActionResult Get()
        {
            var products = productRepository.GetAll().Where(x=>x.Status!="Out of Stock").ToList();
            if (products.Count==0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(products);
            }     
        }
        [Route("{customerId}/Product/{productId}", Name ="GetReviewsByProductId")]
        public IHttpActionResult GetByProductId(int customerId, int productId)
        {
            var reviewsList = reviewRepository.GetAll().Where(x => x.ProductId == productId).OrderByDescending(x=>x.DatePosted).ToList();
            if (reviewsList.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(reviewsList);
            }
        }
        [Route("{id}/Rating"), HttpGet]
        public IHttpActionResult GetRatingByProductId(int id)
        {
            double rate = 0;
            rate = (double)ratingRepository.GetAll().Where(x => x.ProductId == id).Select(x => x.RatingPoint).DefaultIfEmpty().Average();
            var productRating = new KeyValuePair<string, double>("ProductRate", rate);            
            return Ok(JsonConvert.SerializeObject(productRating));
        }
        [Route("GetAllProductsName"),HttpGet]
        public IHttpActionResult GetAllProductsName()
        {
            var productsName = productRepository.GetAll().Select(x => x.ProductName).ToList();
            if (productsName.Count==0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(productsName);
            }
        }
        [Route("{productName}/Product"), HttpGet]
        public IHttpActionResult GetProductId(string productName)
        {
            var id = 0;
            id = productRepository.GetAll().Where(x => x.ProductName.Equals(productName)).Select(x => x.Id).FirstOrDefault();
            var ProductId = new KeyValuePair<string, int>(productName, id);
            if (id!=0)
            {
                return Ok(JsonConvert.SerializeObject(ProductId));
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("GetAllCategories"), HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            var categories = productRepository.GetAll().Select(x => x.Category).Distinct().ToList();
            if (categories==null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(categories);
            }
        }
        [Route("GetProductsByCategory/{category}"), HttpGet]
        public IHttpActionResult GetProductsByCategory(string category)
        {
            var products = productRepository.GetAll().Where(x => x.Category == category).Distinct().OrderByDescending(x=>x.Id).ToList();
            if (products==null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(products);
            }
        }
        [Route("{id}"), HttpPut]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Update(product);
                return Ok(product);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
    }
}
