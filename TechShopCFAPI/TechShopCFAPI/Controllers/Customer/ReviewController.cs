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
    [RoutePrefix("Api/Reviews")]
    public class ReviewController : ApiController
    {
        protected ReviewRepository reviewRepository = new ReviewRepository();
        [Route("{id}", Name = "GetByReviewId")]
        public IHttpActionResult Get(int id)
        {
            var review = reviewRepository.Get(id);
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            review.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a new review." });
            review.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an specific review data." });

            if (review!=null)
            {
                return Ok(review);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        
        [Route("")]
        public IHttpActionResult Post(Review review)
        {
            if (ModelState.IsValid)
            {
                var newReview = reviewRepository.GetAll().Where(x => x.CustomerId == review.CustomerId && x.ProductId == review.ProductId).FirstOrDefault();
                var url = HttpContext.Current.Request.Url.AbsoluteUri;
                review.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a new review." });
                review.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an specific review data." });

                if (newReview==null)
                {
                    reviewRepository.Insert(review);
                    string URL = Url.Link("GetByReviewId", new { id = review.ReviewId });
                    return Created(URL, review);
                }
                else
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
    }
}
