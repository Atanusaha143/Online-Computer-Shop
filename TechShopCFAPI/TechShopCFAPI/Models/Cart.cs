using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }


        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(20)]
        public string Category { get; set; }

        [Required]
        public int Quantity { get; set; }


        [Required]
        public int TotalPrice { get; set; }

    }
}