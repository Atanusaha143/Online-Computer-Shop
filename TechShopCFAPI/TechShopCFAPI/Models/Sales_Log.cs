using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace TechShopCFAPI.Models
{
    public class Sales_Log
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerPhoneNo { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Nullable<int> SalesExecutiveId { get; set; }

        [Required]
        public System.DateTime DateSold { get; set; }

        [Required]
        public int Quantity { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Tax { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        public Nullable<decimal> Profits { get; set; }
      
        [NotMapped]
        public DateTime StartDate { get; set; }
        [NotMapped]
        public DateTime EndDate { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Product Product { get; set; }
    }
}