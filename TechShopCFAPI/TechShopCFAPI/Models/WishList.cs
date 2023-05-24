using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class WishList
    {
        List<Link> links = new List<Link>();
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public DateTime DateWished { get; set; }
        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}