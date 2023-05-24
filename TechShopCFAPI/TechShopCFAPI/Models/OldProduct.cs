using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class OldProduct
    {
        List<Link> links = new List<Link>();

        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [Required]
        public decimal BuyingPrice { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        public string Features { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(250)]
        public string Images { get; set; }

        public Nullable<int> Discount { get; set; }

        public Nullable<int> Tax { get; set; }

        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}