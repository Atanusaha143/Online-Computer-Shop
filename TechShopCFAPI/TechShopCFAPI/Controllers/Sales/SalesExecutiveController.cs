using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Attributes;
using TechShopCFAPI.Models;
using TechShopCFAPI.Models.Sales.DataModels;
using TechShopCFAPI.Models.Sales.ViewModels;

namespace TechShopCFAPI.Controllers
{
    [RoutePrefix("api/SalesExecutive")]
    public class SalesExecutiveController : ApiController
    {
        List<CartViewModel> salesCart = new List<CartViewModel>();
        TechShopDbContext context = new TechShopDbContext();
        ProductsDataModel productsData = new ProductsDataModel();
        CartDataModel cartData = new CartDataModel();
        //System.Web.HttpCookie sc = new HttpCookie("sc1");

        [Route("Profile", Name = "SalesExecutiveProfile"),BasicAuthintcationAttribute]
        public IHttpActionResult Get()
        {
          // SalesExecutiveDataModel d = new SalesExecutiveDataModel();
           // var dd = d.GetValidSalesExecutive(1);
            return Ok(context.SalesExecutives);
        }
        [Route("Products", Name = "SalesExecutiveProducts"), BasicAuthintcationAttribute]
        public IHttpActionResult GetProducts()
        {
            var FullUrl = HttpContext.Current.Request.Url.AbsoluteUri;
           // List<Models.Product> prod = new Product();
           var prod = productsData.GetAllProducts();
            foreach (var c in prod) 
            {
                c.Links.Add(new Link() { Url = FullUrl, Method = "GET", Relation = "Get All Products" });
                c.Links.Add(new Link() { Url = FullUrl+ "/AvailableProducts", Method = "GET", Relation = "Get All Available Products" });
                c.Links.Add(new Link() { Url = FullUrl + "/UpcomingProducts", Method = "GET", Relation = "Get All Up Coming Products" });
                c.Links.Add(new Link() { Url = FullUrl + "/DiscountProducts", Method = "GET", Relation = "Get All Discount Products" });
            }
           
            return Ok(prod);
            
        }
        [Route("AvailableProducts", Name = "SalesExecutiveAvailableProducts")]
        public IHttpActionResult GetAvailableProducts()
        {
            return Ok(productsData.GetAvailableProducts());
        }
        [Route("UpcomingProducts", Name = "SalesExecutiveUpcomingProducts")]
        public IHttpActionResult GetUpcomingProducts()
        {
            return Ok(productsData.GetUpComingProducts());
        }
        [Route("DiscountProducts", Name = "SalesExecutiveDiscountProducts")]
        public IHttpActionResult GetDiscountProducts()
        {
            return Ok(productsData.GetAllDiscountProducts());
        }

       [Route("AddProductToCart/{id}/{q}", Name = "cart"), HttpPost]
        public IHttpActionResult AddProductToCart([FromUri] int id, int q)
        {

          Models.Product  data = context.Products.Find(id);
            Models.Cart cart = new Cart();

            if (q <=0) { return Ok(new { msg = "In valid Quantity !" }); }
            if (q > data.Quantity) { return Ok(new { msg = "Not enough stock !" }); }
            cart.ProductId = id;
            cart.ProductName = data.ProductName;
            cart.Quantity = q;
            cart.Category = data.Category;
            cart.TotalPrice = (int)data.SellingPrice * q;
            context.Carts.Add(cart);
            context.SaveChanges();

            return Ok(new { msg = "Product Added to the Cart!" });
            
        }

        [Route("Cart", Name = "cartview"), BasicAuthintcationAttribute]
        public IHttpActionResult GetCart()
        {
            return Ok(context.Carts);
        }

        [Route("sell", Name = "sells")]
        public IHttpActionResult GetSell()
        {
            return Ok(context.Carts);
        }

        [Route("Sold"), HttpPost]
        public IHttpActionResult Solddd( info info)
        {
            //string cname, string address, string phone
            ProductsDataModel data = new ProductsDataModel();
            Models.Product SellingProduct = new Product();

       
            var CurrentQuantity = 0;
            var UpdatedQuantity = 0;
            var id = 0;
     
            foreach (var c in context.Carts)
            {

                id = c.ProductId;

                SellingProduct = data.GetProductById(id);

                CurrentQuantity = SellingProduct.Quantity;
                UpdatedQuantity = CurrentQuantity - c.Quantity;
                if (UpdatedQuantity == 0) { SellingProduct.Status = "Stock Out"; }
                SellingProduct.Quantity = UpdatedQuantity;
                data.UpdateProduct(SellingProduct);
                var sl = new Sales_Log()
                {
                    UserId = 2,
                    CustomerName = info.fullName,
                    CustomerAddress= info.address,
                    CustomerPhoneNo = info.phone,
                    ProductId = c.ProductId,
                    SalesExecutiveId = 1,
                    DateSold = DateTime.Today,
                    Quantity = c.Quantity,
                    Discount = SellingProduct.Discount,
                    Tax = SellingProduct.Tax,
                    TotalPrice = (decimal)c.TotalPrice,
                    Status = "Sold",
                    Profits = ((SellingProduct.SellingPrice) * c.Quantity) - ((SellingProduct.BuyingPrice) * c.Quantity)
                };

                data.insertSales(sl, c.Id);
               
            }
            return Ok();
        }
        [Route("LoadChart", Name = "LoadChart"), BasicAuthintcationAttribute]
        public IHttpActionResult GetChart()
        {
            SalesLogDataModel log = new SalesLogDataModel();

            // var categoryNameQuantity = new List<KeyValuePair<string, int>>();
            var DateAndQuantity = new List<KeyValuePair<string, int>>();
            var alllog = log.GetAllSalesLog().ToList();

            Dictionary<DateTime, bool> check = new Dictionary<DateTime, bool>();
            foreach (var item in alllog)
            {
                check.Add((item.DateSold), true);
            }
            foreach (KeyValuePair<DateTime, bool> item in check)
            {
                DateTime currDate = item.Key;

                int totalQuantity = context.Sales_Logs.Where(x => x.DateSold == currDate).Count();
                DateAndQuantity.Add(new KeyValuePair<string, int>(currDate.ToString(), totalQuantity));
            }
            return Json(DateAndQuantity);



            //return Ok(context.Carts);
        }

      /*  [Route("ClearCart", Name = "clearcart"),HttpDelete]
        public IHttpActionResult DeleteCart()
        {
            return Ok(context.Carts);
        }*/



    }
}
