using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechShopCFAPI.Models.Sales.ViewModels;

namespace TechShopCFAPI.Models.Sales.DataModels
{
    public class CartDataModel
    {
        public List<CartViewModel> GetNewCart()
        {
            return new List<CartViewModel>();
        }
        public CartViewModel ConvertToCartViewModel(Product product)
        {
            CartViewModel cart = new CartViewModel();
            cart.ProductId = product.Id;
            cart.ProductImage = product.Images;
            cart.ProductName = product.ProductName;
            cart.Quantity = product.Quantity;
            cart.UnitPrice = product.SellingPrice;
            cart.Discount = product.Discount;
            cart.Tax = product.Tax;
            cart.Brand = product.Brand;
            cart.TotalPrice = product.Quantity * (product.SellingPrice - ((product.SellingPrice * product.Discount) / 100) + ((product.SellingPrice * product.Tax) / 100));
            return cart;
        }
    }
}