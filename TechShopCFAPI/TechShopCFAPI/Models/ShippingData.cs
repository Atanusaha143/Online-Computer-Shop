using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechShopCFAPI.Models
{
    public class ShippingData
    {
        List<Link> links = new List<Link>();
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [MinLength(3), MaxLength(12)]
        public string CardType { get; set; }
        [MinLength(10), MaxLength(20)]
        public string CardNumber { get; set; }

        [Range(20, 25)]
        public string ExpirationYear { get; set; }
        [Range(1, 12)]
        public string ExpirationMonth { get; set; }
        [MaxLength(15), MinLength(3)]
        public string ShippingMethod { get; set; }
        [MaxLength(50), MinLength(3)]
        public string ShippingAddress { get; set; }
        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}