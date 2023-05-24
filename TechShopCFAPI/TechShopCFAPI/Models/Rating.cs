using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class Rating
    {
        List<Link> links = new List<Link>();
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }


        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; } 
        public Product Product { get; set; }

        [Range(1,5), Required]
      
        public int RatingPoint { get; set; }
        [Required]
        public DateTime DateRated { get; set; }
        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}