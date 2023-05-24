using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class PurchaseLog
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
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        public string Features { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(250)]
        public string Images { get; set; }

        public Nullable<DateTime> PurchasedDate { get; set; }

        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }

        [NotMapped]
        public Nullable<System.DateTime> StartDate { get; set; }
        [NotMapped]
        public Nullable<System.DateTime> EndDate { get; set; }

    }
}