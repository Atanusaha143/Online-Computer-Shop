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
    [RoutePrefix("Api/Rating")]
    public class RatingController : ApiController
    {
        protected RatingRepository ratingRepository = new RatingRepository();
        [Route("{id}", Name ="GetByRatingId")]
        public IHttpActionResult Get(int id)
        {
            var rating = ratingRepository.Get(id);
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            rating.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a new rating." });
            rating.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an specific rating data." });

            if (rating==null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                return Ok(rating);
            }
        }
        [Route("")]
        public IHttpActionResult Post(Rating rating)
        {
            var newRating = ratingRepository.GetAll().Where(x => x.CustomerId == rating.CustomerId && x.ProductId == rating.ProductId).FirstOrDefault();
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            newRating.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Post a new rating." });
            newRating.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an specific rating data." });
            if (newRating==null)
            {
                ratingRepository.Insert(rating);
                string URL = Url.Link("GetByRatingId", new { id = rating.RatingId });
                return Created(URL, rating);
            }
            else
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
        }
    }
}
