using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models.Sales.ViewModels
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int? Tax { get; set; }
        public int? Discount { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}