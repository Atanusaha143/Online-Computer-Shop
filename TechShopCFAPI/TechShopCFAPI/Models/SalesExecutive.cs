using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class Review
    {
        List<Link> links = new List<Link>();
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        [Required, MaxLength(100), MinLength(3)]
        public string Body { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
      
        [Required]
        public DateTime DatePosted { get; set; }
        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}