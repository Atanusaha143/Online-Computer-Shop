using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models;

namespace TechShopCFAPI.Repositories.AdminModule
{
    public class ReviewRepository : Repository<Review>
    {
        TechShopDbContext context = new TechShopDbContext();

        public List<Review> GetReview(int id)
        {
            return context.Reviews.Where(r => r.ProductId == id).ToList();
        }

    }
}