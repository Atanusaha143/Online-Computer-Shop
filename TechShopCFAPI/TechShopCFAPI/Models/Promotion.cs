using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string PromoCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public System.DateTime Validity { get; set; }
    }
}