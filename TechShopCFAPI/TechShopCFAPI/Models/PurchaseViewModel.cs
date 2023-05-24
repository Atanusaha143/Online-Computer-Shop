using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int Tax { get; set; }
        public int Discount { get; set; }
        public DateTime SoldDate { get; set; }
        public string Status { get; set; }
    }
}